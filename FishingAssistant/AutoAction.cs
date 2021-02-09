using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Objects;
using System.Collections.Generic;
using System.Linq;
using Object = StardewValley.Object;

namespace FishingAssistant
{
    partial class ModEntry : Mod
    {
        private int autoCastDelay = 60;
        private int autoClosePopupDelay = 30;
        private int autoLootDelay = 30;

        private float treasureCatchStep = 0;
        private bool catchingTreasure;

        private enum ModState { Disable, Pause, Idle, Fishing, Loot }

        private ModState modState;

        /// <summary>Auto cast fishing rod if posible by last player facing direction.</summary>
        private void AutoCastFishingRod()
        {
            if (modEnable && CanCastFishingRod() && !Game1.isFestival())
            {
                modState = ModState.Idle;
                if (autoCastDelay-- > 0)
                    return;

                autoCastDelay = 60;
                modState = ModState.Fishing;

                //prevent player from exhausted
                if (Game1.player.stamina <= (8.0f + (Game1.player.fishingLevel * 0.1f)))
                {
                    modEnable = false;
                    modState = ModState.Disable;
                    Game1.playSound("coin");
                    AddHUDMessage(3, KeyHelper.hud_message_low_stamina);
                    return;
                }

                if (Game1.player.isInventoryFull())
                {
                    modEnable = false;
                    modState = ModState.Disable;
                    Game1.playSound("coin");
                    AddHUDMessage(3, KeyHelper.hud_message_inventory_full);
                    return;
                }

                Game1.player.faceDirection(playerFacingDirection);
                fishingRod.beginUsing(Game1.currentLocation, playerStandingX, playerStandingY, Game1.player);
                modState = ModState.Idle;
            }
        }

        /// <summary>Auto hook fish when fish bite.</summary>
        private void AutoHook()
        {
            //apply auto hook if mod enable
            if (modEnable && IsRodCasting)
                autoHook = true;

            // auto hook fish when fish bite
            if (autoHook && IsRodCanHook() && !Game1.isFestival())
            {
                autoHook = false;
                fishingRod.DoFunction(Game1.player.currentLocation, 1, 1, 1, Game1.player);
            }
        }

        /// <summary>Auto play fishing minigame.</summary>
        private void AutoPlayMiniGame()
        {
            if (!modEnable)
                return;

            modState = ModState.Fishing;

            float fishPos = BarBobberPosition;
            float barPosMax = 568 - (BarHeight / 2);
            float barPosMin = (BarHeight / 2);

            if (BarDistanceFromCatching >= 1.0f)
            {
                EndMiniGame();
            }
            else if (autoCatchTreasure && BarHasTreasure && !BarTreasureCaught && (BarDistanceFromCatching > 0.85 || catchingTreasure))
            {
                catchingTreasure = true;
                fishPos = BarTreasurePosition;

                if (!IsTreasureInBar())
                    HandleTreasureCatchError();
            }
            else if (catchingTreasure && BarDistanceFromCatching < 0.15)
            {
                catchingTreasure = false;
                fishPos = BarBobberPosition;
            }

            if (fishPos < barPosMin)
                fishPos = barPosMin;
            else if (fishPos > barPosMax)
                fishPos = barPosMax;

            BarPosition = fishPos - (BarHeight / 2) + 25;
        }

        /// <summary>Auto close fish popup when fishing minigame finish</summary>
        private void AutoCloseFishPopup()
        {
            if (modEnable && IsRodShowingFish() && !Game1.isFestival())
            {
                if (autoClosePopupDelay-- > 0)
                    return;

                autoClosePopupDelay = 30;
                modState = ModState.Fishing;

                Farmer player = Game1.player;

                player.currentLocation.localSound("coin");
                if (!IsRodTreasureCaught)
                {
                    Object item2 = null;
                    if (RodItemCategory == "Object")
                    {
                        item2 = new Object(RodWhichFish, 1, isRecipe: false, -1, RodFishQuality);
                        if (RodWhichFish == GameLocation.CAROLINES_NECKLACE_ITEM)
                        {
                            item2.questItem.Value = true;
                        }
                        if (RodWhichFish == 79 || RodWhichFish == 842)
                        {
                            item2 = player.currentLocation.tryToCreateUnseenSecretNote(fishingRod.getLastFarmerToUse());
                            if (item2 == null)
                            {
                                return;
                            }
                        }
                        if (RodCaughtDoubleFish)
                        {
                            item2.Stack = 2;
                        }
                    }
                    else if (RodItemCategory == "Furniture")
                    {
                        item2 = new Furniture(RodWhichFish, Vector2.Zero);
                    }
                    bool cachedFromFishPond = RodFromFishPond;
                    fishingRod.getLastFarmerToUse().completelyStopAnimatingOrDoingAction();
                    fishingRod.doneFishing(fishingRod.getLastFarmerToUse(), !cachedFromFishPond);
                    if (!Game1.isFestival() && !cachedFromFishPond && RodItemCategory == "Object" && Game1.player.team.specialOrders != null)
                    {
                        foreach (SpecialOrder order2 in Game1.player.team.specialOrders)
                        {
                            if (order2.onFishCaught != null)
                            {
                                order2.onFishCaught(Game1.player, item2);
                            }
                        }
                    }
                    if (!Game1.isFestival() && !fishingRod.getLastFarmerToUse().addItemToInventoryBool(item2))
                    {
                        Game1.activeClickableMenu = new ItemGrabMenu(new List<Item> { item2 }, this).setEssential(essential: true);
                    }
                }
                else
                {
                    IsRodFishCaught = false;
                    IsRodShowingTreasure = true;
                    player.UsingTool = true;
                    int stack = 1;
                    if (RodCaughtDoubleFish)
                    {
                        stack = 2;
                    }
                    Object item = new Object(RodWhichFish, stack, isRecipe: false, -1, RodFishQuality);
                    if (Game1.player.team.specialOrders != null)
                    {
                        foreach (SpecialOrder order in Game1.player.team.specialOrders)
                        {
                            if (order.onFishCaught != null)
                            {
                                order.onFishCaught(Game1.player, item);
                            }
                        }
                    }
                    bool hadroomForfish = fishingRod.getLastFarmerToUse().addItemToInventoryBool(item);
                    fishingRod.animations.Add(new TemporaryAnimatedSprite("LooseSprites\\Cursors", new Rectangle(64, 1920, 32, 32), 500f, 1, 0, fishingRod.getLastFarmerToUse().Position + new Vector2(-32f, -160f), flicker: false, flipped: false, (float)fishingRod.getLastFarmerToUse().getStandingY() / 10000f + 0.001f, 0f, Color.White, 4f, 0f, 0f, 0f)
                    {
                        motion = new Vector2(0f, -0.128f),
                        timeBasedMotion = true,
                        endFunction = fishingRod.openChestEndFunction,
                        extraInfoForEndBehavior = ((!hadroomForfish) ? 1 : 0),
                        alpha = 0f,
                        alphaFade = -0.002f
                    });
                }

                if (hasDisableRequest)
                {
                    hasDisableRequest = false;
                    modEnable = false;
                    modState = ModState.Disable;
                }
            }
        }

        /// <summary>Loot all item in treasure if treasure is caught during fishing minigame</summary>
        private void AutoLootTreasure()
        {
            if (!modEnable || Game1.isFestival())
                return;

            if (!(Game1.activeClickableMenu is ItemGrabMenu itemGrab) || itemGrab.organizeButton != null || itemGrab.shippingBin)
                return;

            IList<Item> actualInventory = itemGrab.ItemsToGrabMenu.actualInventory;

            if (autoLootDelay-- > 0)
                return;

            autoLootDelay = 30;
            modState = ModState.Loot;
            if (actualInventory.Count == 0)
            {
                itemGrab.exitThisMenu(true);
                if (hasDisableRequest)
                {
                    hasDisableRequest = false;
                    modEnable = false;
                    modState = ModState.Disable;
                }
            }
            else
            {
                Item obj = ((IEnumerable<Item>)actualInventory).First();
                if (obj != null)
                {
                    if (obj.parentSheetIndex == 102)
                    {
                        Game1.player.foundArtifact(102, 1);
                        Game1.playSound("fireball");
                    }
                    else
                    {
                        Item inventory = Game1.player.addItemToInventory(obj);
                        if (inventory != null)
                        {
                            modEnable = false;
                            modState = ModState.Disable;
                            Game1.playSound("coin");
                            AddHUDMessage(3, KeyHelper.hud_message_inventory_full);
                            return;
                        }
                        else
                            Game1.playSound("coin");
                    }
                }
                actualInventory.RemoveAt(0);
                autoLootDelay = 10;
            }
        }

        private void AutoStopFishingOnTime()
        {
            if (Game1.timeOfDay >= Config.PauseFishingTime && modEnable && !isForceEnable)
            {
                Game1.playSound("coin");
                AddHUDMessage(3, KeyHelper.hud_message_auto_disable, ConvertTime(Game1.timeOfDay));

                ToggleMod();

                if (!Game1.IsMultiplayer)
                    Game1.activeClickableMenu = new GameMenu();
            }
        }
    }
}