using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;
using System.Collections.Generic;
using System.Linq;
using Object = StardewValley.Object;

namespace FishingAssistant
{
    partial class ModEntry : Mod
    {
        private bool catchingTreasure;

        private void Initialize(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();

            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
            helper.Events.Display.MenuChanged += OnMenuChanged;
            helper.Events.Input.ButtonPressed += OnButtonPressed;

            Monitor.Log("Initialized (press F5 to reload config)", LogLevel.Info);
        }

        private void ToggleMod(ButtonPressedEventArgs e)
        {
            if (e.Button == Config.EnableModButton)
            {
                modEnable = !modEnable;
                if (modEnable) GetPlayerData();
                Monitor.Log(string.Format("Mod enable {0}", modEnable), LogLevel.Info);
            }
        }

        private void ToggleMaxCastPower(ButtonPressedEventArgs e)
        {
            if (e.Button == Config.CastPowerButton)
            {
                maxCastPower = !maxCastPower;
                Monitor.Log(string.Format("Max cast power {0}", maxCastPower), LogLevel.Info);
            }
        }

        private void ToggleCatchTreasure(ButtonPressedEventArgs e)
        {
            if (e.Button == Config.CatchTreasureButton)
            {
                autoCatchTreasure = !autoCatchTreasure;
                Monitor.Log(string.Format("Catch treasure {0}", autoCatchTreasure), LogLevel.Info);
            }
        }

        private void ReloadConfig(ButtonPressedEventArgs e)
        {
            if (e.Button == Config.ReloadConfigButton)
            {
                Config = Helper.ReadConfig<ModConfig>();
                Monitor.Log("Config reloaded", LogLevel.Info);
            }
        }

        private void GetPlayerData()
        {
            playerStandingX = Game1.player.getStandingX();
            playerStandingY = Game1.player.getStandingY();
            playerFacingDirection = Game1.player.getDirection() != -1 ? Game1.player.getDirection() : Game1.player.FacingDirection;
        }

        private bool IsFishingMiniGameReady()
        {
            return inFishingMiniGame && bobberBar != null;
        }

        private void ApplyInfiniteBaitAndTackle(UpdateTickedEventArgs e)
        {
            if (!(Context.IsWorldReady && e.IsOneSecond && Game1.player.CurrentTool is FishingRod rod))
                return;

            if (Config.InfiniteBait && rod.attachments?.Length > 0 && rod.attachments[0] != null)
                rod.attachments[0].Stack = rod.attachments[0].maximumStackSize();

            if (Config.InfiniteTackle && rod.attachments?.Length > 1 && rod.attachments[1] != null)
                rod.attachments[1].uses.Value = 0;
        }

        private void AutoCastFishingRod()
        {
            if (modEnable && CanCastFishingRod())
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

        private void InstantFishBite()
        {
            if (Config.InstantFishBite && (IsRodFishing && !IsRodHit && RodTimeUntilFishBite > 0))
                RodTimeUntilFishBite = 0;
        }

        private void AutoHook()
        {
            //apply auto hook if mod enable
            if (modEnable && IsRodBobberStillInAir)
                autoHook = true;

            // auto hook fish when fish bite
            if (autoHook && IsRodCanHook())
            {
                autoHook = false;
                fishingRod.DoFunction(Game1.player.currentLocation, 1, 1, 1, Game1.player);
            }
        }

        private void AutoPlayMiniGame()
        {
            if (!modEnable)
                return;

            float fishPos = barBobberPosition;
            float barPosMax = (568 - barHeight / 2);
            float barPosMin = (barHeight / 2);

            if (barDistanceFromCatching >= 1.0f)
            {
                SkipMiniGame();
            }
            else if (autoCatchTreasure && barHasTreasure && !barTreasureCaught && (barDistanceFromCatching > 0.85 || catchingTreasure))
            {
                catchingTreasure = true;
                fishPos = barTreasurePosition;

                if (!IsTreasureInBar())
                    HandleTreasureCatchError();

            }
            else if (catchingTreasure && barDistanceFromCatching < 0.15)
            {
                catchingTreasure = false;
                fishPos = barBobberPosition;
            }
            fishPos += 20.0f;//center sprite

            if (barBobberPosition < barPosMin)
                fishPos = barPosMin;
            else if (barBobberPosition > barPosMax)
                fishPos = barPosMax;

            barPos = fishPos - (barHeight / 2);
        }

        private void SkipMiniGame()
        {
            if (Game1.isFestival())
                return;

            int attachmentValue = fishingRod.attachments[0] == null ? -1 : fishingRod.attachments[0].parentSheetIndex;
            bool caughtDouble = Config.AlwaysCatchDoubleFish || (barBossFish && attachmentValue == 774 && Game1.random.NextDouble() < 0.25 + Game1.player.DailyLuck / 2.0);

            fishingRod.pullFishFromWater(barWhichFish, barFishSize, barFishQuality, (int)barDifficulty, barHasTreasure, barPerfect, barFromFishPond, caughtDouble);
            Game1.exitActiveMenu();
            Game1.setRichPresence("location", Game1.currentLocation.Name);
        }


        private void AutoCloseFishPopup()
        {
            if (modEnable && IsRodShowingFish())
            {
                if (autoClosePopupDelay-- > 0)
                    return;

                Game1.player.currentLocation.localSound("coin");

                if (!IsRodTreasureCaught)
                {
                    fishingRod.doneFishing(Game1.player, true);
                    Game1.player.completelyStopAnimatingOrDoingAction();

                    Object @object = new Object(rodWhichFish, 1, false, -1, rodFishQuality);
                    if (rodWhichFish == GameLocation.CAROLINES_NECKLACE_ITEM)
                        @object.questItem.Value = true;
                    if (rodWhichFish == 79)
                    {
                        @object = Game1.player.currentLocation.tryToCreateUnseenSecretNote(Game1.player);
                        if (@object == null)
                            return;
                    }
                    if (rodCaughtDoubleFish)
                        @object.Stack = 2;

                    Game1.player.completelyStopAnimatingOrDoingAction();
                    fishingRod.doneFishing(Game1.player, !rodFromFishPond);
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
                    if (rodCaughtDoubleFish)
                        initialStack = 2;
                    bool inventoryBool = Game1.player.addItemToInventoryBool(new Object(rodWhichFish, initialStack, false, -1, rodFishQuality), false);
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
            if (!modEnable)
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