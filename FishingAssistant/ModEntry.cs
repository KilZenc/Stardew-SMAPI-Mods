using FishingAssistant.Menu;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;

namespace FishingAssistant
{
    partial class ModEntry : Mod
    {
        private ModConfig Config;
        private bool isPause;

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        public override void Entry(IModHelper helper)
        {
            // Initialize mod
            Initialize(helper);
        }

        /// <summary>Raised after the game is launched, right before the first update tick.</summary>
        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            // read the Config for display position and get list priority for displayOrder
            ReloadConfig();
        }

        /// <summary>Raised after the in-game clock time changes.</summary>
        private void OnTimeChanged(object sender, TimeChangedEventArgs e)
        {
            AutoStopFishingOnTime();
        }

        /// <summary>Raised before the game ends the current day.
        /// This happens before it starts setting up the next day and before StardewModdingAPI.Events.IGameLoopEvents.Saving.</summary>
        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            isForceEnable = false;
            hasEnableRequest = false;
            hasDisableRequest = false;
        }

        /// <summary> Raised after the game state is updated (≈60 times per second). </summary>
        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!modEnable || !Context.IsWorldReady)
                return;

            // apply infinite bait/tackle
            ApplyInfiniteBaitAndTackle(e);

            if (IsFishingMiniGameReady())
            {
                //Force fishing minigame result to be perfect
                AlwayPerfectResult();

                //Auto play minigame
                AutoPlayMiniGame();
            }
            else
            {
                //Auto loot item in treasure chest
                AutoLootTreasure();
            }

            if (Game1.player?.CurrentTool is FishingRod rod)
            {
                fishingRod = rod;

                if (isPause)
                {
                    //Unpause mod status and get new player data
                    isPause = false;
                    GetPlayerData();
                }

                AutoAttachBaitAndTackles();

                // Cast fishing rod if possible
                AutoCastFishingRod();

                // Force max cast power
                MaxCastPower();

                // Make fish instantly bite
                InstantFishBite();

                //Auto hook fish when fish bite
                AutoHook();

                //Auto close fish popup
                AutoCloseFishPopup();
            }
            else
            {
                //Change mod status to pasue
                isPause = true;
                modState = ModState.Pause;
                autoCastDelay = 60;
                autoClosePopupDelay = 30;
                autoLootDelay = 30;
            }
        }

        /// <summary>  Raised after a game menu is opened, closed, or replaced. </summary>
        private void OnMenuChanged(object sender, MenuChangedEventArgs e)
        {
            //Save config value
            if (e.OldMenu is FishingAssistantMenu)
            {
                Helper.WriteConfig(Config);
                ReloadConfig();
            }

            if (!modEnable)
                return;

            // Check if fishing minigame is start
            if (e.NewMenu is BobberBar bar)
                OnFishingMiniGameStart(bar);

            // Check if fishing minigame is end
            if (e.OldMenu is BobberBar)
                OnFishingMiniGameEnd();
        }

        /// <summary>When a menu is open, raised after that menu is drawn to the sprite batch but before it's rendered to the screen.</summary>
        private void OnRenderMenu(object sender, RenderedActiveMenuEventArgs e)
        {
            DrawFishInfo();
        }

        /// <summary>
        ///  Raised before drawing the HUD (item toolbar, clock, etc) to the screen. The vanilla
        ///  HUD may be hidden at this point (e.g. because a menu is open). Content drawn
        ///  to the sprite batch at this point will appear under the HUD.
        /// </summary>
        private void OnRenderingHud(object sender, RenderingHudEventArgs e)
        {
            DrawModStatus();
        }

        /// <summary> Raised after the player presses a button on the keyboard, controller, or mouse. </summary>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady) return;

            //Enable or disable mod
            OnEnableModButtonPressed(e);

            //Toggle cast power to max or free
            OnCastPowerButtonPressed(e);

            //Toggle catch or ignore treasure when play fishing minigame
            OnCatchTreasureButtonPressed(e);

            //Open in-game mod menu
            OnToggleMenuButtonPressed(e);
        }

        private void OnFishingMiniGameStart(BobberBar bar)
        {
            bobberBar = bar;
            inFishingMiniGame = true;
            isCatching = true;
            fishPreviewFishId = 0;

            //Overide fish difficulty
            OverrideFishDifficult();

            //Make treasure appear every time
            AlwayFindTreasure();

            //Instantly catch fish when minigame start
            InstantCatchFish();

            //Instantly catch treasure when treasure appear
            InstantCatchTreasure();
        }

        private void OnFishingMiniGameEnd()
        {
            //Reset varible value
            inFishingMiniGame = false;
            isCatching = false;
            catchingTreasure = false;

            if (hasDisableRequest)
            {
                hasDisableRequest = false;
                modEnable = false;
                modState = ModState.Disable;
            }
        }
    }
}