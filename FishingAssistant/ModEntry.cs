using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley.Menus;

namespace FishingAssistant
{
    partial class ModEntry : Mod
    {
        private ModConfig Config;

        private bool inFishingMiniGame;

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
            // apply infinite bait/tackle
            ApplyInfiniteBaitAndTackle(e);

            if (IsFishingMiniGameReady())
            {
                if (Config.AlwaysPerfect)
                    perfect = true;
            }
        }

        /// <summary>  Raised after a game menu is opened, closed, or replaced. </summary>
        private void OnMenuChanged(object sender, MenuChangedEventArgs e)
        {
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
            //Reload new config
            ReloadConfig(e);
        }

        private void OnFishingMiniGameStart(BobberBar bar)
        {
            bobberBar = bar;
            inFishingMiniGame = true;

            //Overide fish difficulty
            difficulty *= Config.FishDifficultyMultiplier;
            difficulty += Config.FishDifficultyAdditive;

            //Make treasure appear every time
            if (Config.AlwaysFindTreasure)
                treasure = true;

            //Instantly catch fish when minigame start
            if (Config.InstantCatchFish)
            {
                if (treasure)
                    treasureCaught = true;
                distanceFromCatching = 1.01f;
            }

            //Instantly catch treasure when treasure appear
            if (Config.InstantCatchTreasure && (treasure || Config.AlwaysFindTreasure))
                treasureCaught = true;
        }

        private void OnFishingMiniGameEnd()
        {
            inFishingMiniGame = false;
        }
    }
}