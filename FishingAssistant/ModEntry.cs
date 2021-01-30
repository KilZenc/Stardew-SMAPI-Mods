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

        private bool modEnable;
        private int playerStandingX;
        private int playerStandingY;
        private int playerFacingDirection;

        private bool inFishingMiniGame;

        private bool maxCastPower;
        private bool autoHook;
        private bool autoCatchTreasure;

        private int autoCastDelay = 30;
        private int autoClosePopupDelay = 30;
        private int autoLootDelay = 30;

        private float catchStep = 0;
        private bool catchingTreasure;


        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            // Initialize mod
            Initialize(helper);
        }

        /// <summary> Raised after the game state is updated (≈60 times per second). </summary>
        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!modEnable)
                return;

            // apply infinite bait/tackle
            ApplyInfiniteBaitAndTackle(e);

            if (Game1.player?.CurrentTool is FishingRod rod)
            {
                fishingRod = rod;

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

            if (IsFishingMiniGameReady())
            {
                //Force fishing minigame result to be perfect
                AlwayPerfectResult();

                AutoPlayMiniGame();
            }
            else
            {
                //Auto loot item in treasure chest
                AutoLootTreasure();
            }
        }

        /// <summary>  Raised after a game menu is opened, closed, or replaced. </summary>
        private void OnMenuChanged(object sender, MenuChangedEventArgs e)
        {
            if (!modEnable)
                return;

            // Check if fishing minigame is start
            if (e.NewMenu is BobberBar bar)
                OnFishingMiniGameStart(bar);

            // Check if fishing minigame is end
            if (e.OldMenu is BobberBar)
                OnFishingMiniGameEnd();
        }

        /// <summary> Raised after the player presses a button on the keyboard, controller, or mouse. </summary>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            //Enable or disable mod
            ToggleMod(e);

            //Toggle cast power to max or free
            ToggleMaxCastPower(e);

            //Toggle catch or ignore treasure when play fishing minigame
            ToggleCatchTreasure(e);

            //Reload new config
            ReloadConfig(e);
        }

        private void OnFishingMiniGameStart(BobberBar bar)
        {
            bobberBar = bar;
            inFishingMiniGame = true;

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
            inFishingMiniGame = false;
            catchingTreasure = false;
            autoCastDelay = 30;
            autoClosePopupDelay = 30;
            autoLootDelay = 30;
        }
    }
}