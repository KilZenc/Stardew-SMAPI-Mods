using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System.Collections.Generic;
using System.Linq;
using Object = StardewValley.Object;

namespace FishingAssistant
{
    partial class ModEntry : Mod
    {
        private void AutoCastFishingRod()
        {
            if (modEnable && CanCastFishingRod() && !Game1.isFestival())
            {
                if (autoCastDelay-- > 0)
                    return;

                //prevent player from exhausted
                if (Game1.player.stamina <= 15)
                {
                    modEnable = false;
                    Game1.addHUDMessage(new HUDMessage("Player Have Low Energy", 3));
                    return;
                }

                if (Game1.player.isInventoryFull())
                {
                    modEnable = false;
                    Game1.addHUDMessage(new HUDMessage("Player Inventory Full", 3));
                    return;
                }

                Game1.player.faceDirection(playerFacingDirection);
                fishingRod.beginUsing(Game1.currentLocation, playerStandingX, playerStandingY, Game1.player);
            }
        }

        private void AutoHook()
        {
            //apply auto hook if mod enable
            if (modEnable && IsRodBobberStillInAir)
                autoHook = true;

            // auto hook fish when fish bite
            if (autoHook && IsRodCanHook() && !Game1.isFestival())
            {
                autoHook = false;
                fishingRod.DoFunction(Game1.player.currentLocation, 1, 1, 1, Game1.player);
            }
        }

        private void AutoPlayMiniGame()
        {
            if (!modEnable)
                return;

            float fishPos = BarBobberPosition;
            float barPosMax = (568 - BarHeight / 2);
            float barPosMin = (BarHeight / 2);

            if (BarDistanceFromCatching >= 1.0f)
            {
                SkipMiniGame();
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
            fishPos += 20.0f;//center sprite

            if (BarBobberPosition < barPosMin)
                fishPos = barPosMin;
            else if (BarBobberPosition > barPosMax)
                fishPos = barPosMax;

            BarPosition = fishPos - (BarHeight / 2);
        }

        private void AutoCloseFishPopup()
        {
            if (modEnable && IsRodShowingFish() && !Game1.isFestival())
            {
                if (autoClosePopupDelay-- > 0)
                    return;

                Game1.player.currentLocation.localSound("coin");

                if (!IsRodTreasureCaught)
                {
                    fishingRod.doneFishing(Game1.player, true);
                    Game1.player.completelyStopAnimatingOrDoingAction();

                    Object @object = new Object(RodWhichFish, 1, false, -1, RodFishQuality);
                    if (RodWhichFish == GameLocation.CAROLINES_NECKLACE_ITEM)
                        @object.questItem.Value = true;
                    if (RodWhichFish == 79)
                    {
                        @object = Game1.player.currentLocation.tryToCreateUnseenSecretNote(Game1.player);
                        if (@object == null)
                            return;
                    }
                    if (RodCaughtDoubleFish)
                        @object.Stack = 2;

                    Game1.player.completelyStopAnimatingOrDoingAction();
                    fishingRod.doneFishing(Game1.player, !RodFromFishPond);
                    if (Game1.isFestival() || Game1.player.addItemToInventoryBool(@object, false))
                        return;

                    List<Item> objList = new List<Item>();
                    objList.Add(@object);
                    Game1.activeClickableMenu = new ItemGrabMenu(objList, null).setEssential(true);
                }
                else
                {
                    IsRodFishCaught = false;
                    IsRodShowingTreasure = true;
                    Game1.player.UsingTool = true;
                    int initialStack = 1;
                    if (RodCaughtDoubleFish)
                        initialStack = 2;
                    bool inventoryBool = Game1.player.addItemToInventoryBool(new Object(RodWhichFish, initialStack, false, -1, RodFishQuality), false);
                    fishingRod.animations.Add(new TemporaryAnimatedSprite("LooseSprites\\Cursors", new Rectangle(64, 1920, 32, 32), 500f, 1, 0, Game1.player.Position + new Vector2(-32f, -160f), false, false, (float)(Game1.player.getStandingY() / 10000.0 + 0.001), 0.0f, Color.White, 4f, 0.0f, 0.0f, 0.0f, false)
                    {
                        motion = new Vector2(0.0f, -0.128f),
                        timeBasedMotion = true,
                        endFunction = new TemporaryAnimatedSprite.endBehavior(fishingRod.openChestEndFunction),
                        extraInfoForEndBehavior = inventoryBool ? 0 : 1,
                        alpha = 0.0f,
                        alphaFade = (-1.0f / 500.0f)
                    });
                }
            }
        }

        private void AutoLootTreasure()
        {
            if (!modEnable || !Game1.isFestival())
                return;

            if (Game1.player.isInventoryFull())
            {
                modEnable = false;
                Game1.addHUDMessage(new HUDMessage("Player Inventory Full", 3));
                return;
            }

            if (!(Game1.activeClickableMenu is ItemGrabMenu itemGrab) || itemGrab.organizeButton != null || itemGrab.shippingBin)
                return;

            IList<Item> actualInventory = itemGrab.ItemsToGrabMenu.actualInventory;

            if (autoLootDelay-- > 0)
                return;

            if (actualInventory.Count == 0)
                itemGrab.exitThisMenu(true);
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
                            Game1.playSound("dwoop");
                            Game1.createItemDebris(inventory, Game1.player.getStandingPosition(), Game1.player.facingDirection, null, -1);
                        }
                        else
                            Game1.playSound("coin");
                    }
                }
                actualInventory.RemoveAt(0);
                autoLootDelay = 10;
            }
        }
    }
}