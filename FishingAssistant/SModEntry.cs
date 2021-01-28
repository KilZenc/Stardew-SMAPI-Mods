using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;
using System.Collections.Generic;
using Object = StardewValley.Object;

namespace FishingAssistant
{
    partial class ModEntry : Mod
    {
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

        private void AutoCloseFishPopup()
        {
            if (modEnable && !Context.CanPlayerMove && fishingRod.fishCaught && fishingRod.inUse() && !fishingRod.isCasting && !fishingRod.isTimingCast && !fishingRod.castedButBobberStillInAir && !fishingRod.isFishing && !fishingRod.isReeling && !fishingRod.pullingOutOfWater && !fishingRod.showingTreasure)
            {
                //collectFish = true;
                if (autoClosePopupDelay-- > 0)
                    return;
                Farmer player = Game1.player;
                int whichFish = Helper.Reflection.GetField<int>(fishingRod, "whichFish", true).GetValue();
                int fishQuality = Helper.Reflection.GetField<int>(fishingRod, "fishQuality", true).GetValue();
                bool caughtDoubleFish = Helper.Reflection.GetField<bool>(fishingRod, "caughtDoubleFish", true).GetValue();
                bool fromFishPond = Helper.Reflection.GetField<bool>(fishingRod, "fromFishPond", true).GetValue();
                player.currentLocation.localSound("coin");

                if (!fishingRod.treasureCaught)
                {
                    fishingRod.doneFishing(player, true);
                    player.completelyStopAnimatingOrDoingAction();

                    Object @object = new Object(whichFish, 1, false, -1, fishQuality);
                    if (whichFish == GameLocation.CAROLINES_NECKLACE_ITEM)
                        @object.questItem.Value = true;
                    if (whichFish == 79)
                    {
                        @object = player.currentLocation.tryToCreateUnseenSecretNote(player);
                        if (@object == null)
                            return;
                    }
                    if (caughtDoubleFish)
                        @object.Stack = 2;

                    Game1.player.completelyStopAnimatingOrDoingAction();
                    fishingRod.doneFishing(Game1.player, !fromFishPond);
                    if (Game1.isFestival() || player.addItemToInventoryBool(@object, false))
                        return;

                    List<Item> objList = new List<Item>();
                    objList.Add(@object);
                    Game1.activeClickableMenu = new ItemGrabMenu(objList, null).setEssential(true);
                }
                else
                {
                    fishingRod.fishCaught = false;
                    fishingRod.showingTreasure = true;
                    player.UsingTool = true;
                    int initialStack = 1;
                    if (caughtDoubleFish)
                        initialStack = 2;
                    bool inventoryBool = player.addItemToInventoryBool(new Object(whichFish, initialStack, false, -1, fishQuality), false);
                    fishingRod.animations.Add(new TemporaryAnimatedSprite("LooseSprites\\Cursors", new Rectangle(64, 1920, 32, 32), 500f, 1, 0, player.Position + new Vector2(-32f, -160f), false, false, (float)(player.getStandingY() / 10000.0 + 0.001), 0.0f, Color.White, 4f, 0.0f, 0.0f, 0.0f, false)
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
            //else
            //collectFish = false;
        }
    }
}