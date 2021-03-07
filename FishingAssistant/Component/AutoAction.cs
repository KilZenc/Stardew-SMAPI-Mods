using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Objects;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using Object = StardewValley.Object;

namespace FishingAssistant
{
    internal partial class ModEntry : Mod
    {
        private int defaultAutoCastDelay = 60;
        private int defaultAutoClosePopupDelay = 30;
        private int defaultAutoLootDelay = 30;

        private int autoCastDelay;
        private int autoClosePopupDelay;
        private int autoLootDelay;

        private float treasureCatchStep = 0;
        private bool catchingTreasure;

        public bool autoEatWhenLowEnergy;

        private enum ModState { Disable, Pause, Idle, Fishing, Loot }

        private ModState modState;

        private bool CheckCurrentPreset(bool automation)
        {
            if (Config.AutomationPresets == "Custom" && automation || Config.AutomationPresets == "Default")
                return true;
            return false;
        }

        public bool AutoEatFood(bool ignoreCondition)
        {
            if (!CheckCurrentPreset(Config.EnableAutoEatFood))
                return false;

            if (!Game1.player.isEating && RodIsNotInUse() && Config.EnableAutoEatFood && (ignoreCondition || Game1.player.Stamina <= Game1.player.MaxStamina * ((float)Config.EnergyPrecentToEat / 100)))
            {
                Object bestItem = null;
                foreach (Object item in Game1.player.Items.OfType<Object>())
                {
                    if (item.Edibility <= 0 || (item.Category == -4 && !Config.AllowEatingFish))
                        continue;

                    if (bestItem == null || bestItem.Edibility / bestItem.salePrice() < item.Edibility / item.salePrice())
                        bestItem = item;
                }

                if (bestItem != null)
                {
                    Game1.player.eatObject(bestItem);
                    bestItem.Stack--;
                    if (bestItem.Stack == 0)
                        Game1.player.removeItemFromInventory(bestItem);

                    AddHUDMessage(2, $"Auto eat : {bestItem.DisplayName}");
                    return true;
                }
            }
            return false;
        }

        private void AutoAttachBait()
        {
            if (!CheckCurrentPreset(Config.AutoAttachBait))
                return;

            if (RodIsNotInUse() && !Game1.isFestival() && Config.AutoAttachBait)
            {
                IList<Item> items = Game1.player.Items;

                if (Config.AutoAttachBait && fishingRod.upgradeLevel >= 2)
                {
                    // Check the bait slot.
                    // Case where there is already bait attached.
                    // We stack the same type of bait onto the existing bait attached to the fishing rod.
                    if (fishingRod.attachments[0] != null && fishingRod.attachments[0].Stack != fishingRod.attachments[0].maximumStackSize())
                    {
                        foreach (Item item in items)
                        {
                            // Category value for bait is -21.
                            // Source: https://github.com/veywrn/StardewValley/blob/master/StardewValley/Item.cs
                            if (item?.Category == -21 && item.Name.Equals(fishingRod.attachments[0].Name))
                            {
                                int stackAdd = Math.Min(fishingRod.attachments[0].getRemainingStackSpace(), item.Stack);
                                fishingRod.attachments[0].Stack += stackAdd;
                                item.Stack -= stackAdd;

                                if (item.Stack == 0)
                                    Game1.player.removeItemFromInventory(item);

                                AddHUDMessage(2, I18n.Hud_Message_Auto_Attach_Bait(), item.Name);
                            }
                        }
                    }
                    // Case where there is no bait attached.
                    // We simply attach the first instance of bait we see in the inventory onto the fishing rod.
                    else if (fishingRod.attachments[0] == null)
                    {
                        foreach (Item item in items)
                        {
                            if (item?.Category == -21)
                            {
                                fishingRod.attachments[0] = (Object)item;
                                Game1.player.removeItemFromInventory(item);
                                AddHUDMessage(2, I18n.Hud_Message_Auto_Attach_Bait(), item.Name);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void AutoAttachTackles()
        {
            if (!CheckCurrentPreset(Config.AutoAttachTackles))
                return;

            if (RodIsNotInUse() && !Game1.isFestival() && Config.AutoAttachTackles)
            {
                IList<Item> items = Game1.player.Items;

                // Check the tackle slot.
                if (Config.AutoAttachTackles && fishingRod.attachments[1] == null && fishingRod.upgradeLevel == 3)
                {
                    foreach (Item item in items)
                    {
                        // Category value for tackle is -22.
                        // Source: https://github.com/veywrn/StardewValley/blob/master/StardewValley/Item.cs
                        if (item?.Category == -22)
                        {
                            fishingRod.attachments[1] = (Object)item;
                            Game1.player.removeItemFromInventory(item);
                            AddHUDMessage(2, I18n.Hud_Message_Auto_Attach_Tackle(), item.Name);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>Auto cast fishing rod if posible by last player facing direction.</summary>
        private void AutoCastFishingRod()
        {
            if (!CheckCurrentPreset(Config.AutoCastFishingRod))
                return;

            if (RodIsNotInUse() && !Game1.isFestival() && Config.AutoCastFishingRod)
            {
                modState = ModState.Idle;
                if (autoCastDelay-- > 0)
                    return;

                autoCastDelay = defaultAutoCastDelay;
                modState = ModState.Fishing;

                //prevent player from exhausted
                if (Game1.player.stamina <= (8.0f - (Game1.player.fishingLevel * 0.1f)))
                {
                    if (autoEatWhenLowEnergy && AutoEatFood(ignoreCondition: true))
                        return;

                    AddHUDMessage(3, I18n.Hud_Message_Low_Stamina());
                    modEnable = false;
                    modState = ModState.Disable;
                    Game1.playSound("coin");
                    return;
                }

                if (Game1.player.isInventoryFull())
                {
                    modEnable = false;
                    modState = ModState.Disable;
                    Game1.playSound("coin");
                    AddHUDMessage(3, I18n.Hud_Message_Inventory_Full());
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
            if (!CheckCurrentPreset(Config.AutoHookFish))
                return;

            //Do nothing if fishing rod already had auto hook enchantment
            if (fishingRod.hasEnchantmentOfType<AutoHookEnchantment>())
                return;

            //apply auto hook if mod enable
            if (IsRodCasting && Config.AutoHookFish)
                autoHook = true;

            // auto hook fish when fish bite
            if (autoHook && IsRodCanHook() && !Game1.isFestival())
            {
                autoHook = false;
                fishingRod.timePerBobberBob = 1f;
                fishingRod.timeUntilFishingNibbleDone = FishingRod.maxTimeToNibble;
                fishingRod.DoFunction(Game1.player.currentLocation, (int)fishingRod.bobber.X, (int)fishingRod.bobber.Y, 1, Game1.player);
                Rumble.rumble(0.95f, 200f);
            }
        }

        /// <summary>Auto play fishing minigame.</summary>
        private void AutoPlayMiniGame()
        {
            if (!CheckCurrentPreset(Config.AutoPlayMiniGame))
                return;

            if (Config.AutoPlayMiniGame)
            {
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
                fishPos += 25;

                if (fishPos < barPosMin)
                    fishPos = barPosMin;
                else if (fishPos > barPosMax)
                    fishPos = barPosMax;

                BarPosition = fishPos - (BarHeight / 2);
            }
        }

        /// <summary>Auto close fish popup when fishing minigame finish</summary>
        private void AutoCloseFishPopup()
        {
            if (!CheckCurrentPreset(Config.AutoClosePopup))
                return;

            if (IsRodShowingFish() && !Game1.isFestival() && Config.AutoClosePopup)
            {
                if (autoClosePopupDelay-- > 0)
                    return;

                autoClosePopupDelay = defaultAutoClosePopupDelay;
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
            if (!CheckCurrentPreset(Config.AutoLootTreasure))
                return;

            if (Game1.isFestival() || !Config.AutoLootTreasure || !(Game1.activeClickableMenu is ItemGrabMenu itemGrab) || itemGrab.organizeButton != null || itemGrab.shippingBin)
                return;

            IList<Item> actualInventory = itemGrab.ItemsToGrabMenu.actualInventory;

            if (autoLootDelay-- > 0)
                return;

            autoLootDelay = defaultAutoLootDelay;
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
                            AddHUDMessage(3, I18n.Hud_Message_Inventory_Full());
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
            if (!CheckCurrentPreset(Config.EnableAutoPauseFishing))
                return;

            if (Game1.timeOfDay >= Config.PauseFishingTime * 100 && modEnable && !isForceEnable && Config.EnableAutoPauseFishing)
            {
                Game1.playSound("coin");
                AddHUDMessage(3, I18n.Hud_Message_Auto_Disable(), Game1.getTimeOfDayString(Game1.timeOfDay));

                ToggleMod();

                if (!Game1.IsMultiplayer)
                    Game1.activeClickableMenu = new GameMenu();
            }
        }
    }
}