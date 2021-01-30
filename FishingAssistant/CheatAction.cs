using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Tools;

namespace FishingAssistant
{
    partial class ModEntry : Mod
    {
        private void ApplyInfiniteBaitAndTackle(UpdateTickedEventArgs e)
        {
            if (!(Context.IsWorldReady && e.IsOneSecond && Game1.player.CurrentTool is FishingRod rod))
                return;

            if (Config.InfiniteBait && rod.attachments?.Length > 0 && rod.attachments[0] != null)
                rod.attachments[0].Stack = rod.attachments[0].maximumStackSize();

            if (Config.InfiniteTackle && rod.attachments?.Length > 1 && rod.attachments[1] != null)
                rod.attachments[1].uses.Value = 0;
        }

        private void InstantFishBite()
        {
            if (Config.InstantFishBite && (IsRodFishing && !IsRodHit && RodTimeUntilFishBite > 0))
                RodTimeUntilFishBite = 0;
        }

        private void SkipMiniGame()
        {
            if (Game1.isFestival())
                return;

            int attachmentValue = fishingRod.attachments[0] == null ? -1 : fishingRod.attachments[0].parentSheetIndex;
            bool caughtDouble = Config.AlwaysCatchDoubleFish || (BarBossFish && attachmentValue == 774 && Game1.random.NextDouble() < 0.25 + Game1.player.DailyLuck / 2.0);

            fishingRod.pullFishFromWater(BarWhichFish, BarFishSize, BarFishQuality, (int)BarDifficulty, BarHasTreasure, BarPerfect, BarFromFishPond, caughtDouble);
            Game1.exitActiveMenu();
            Game1.setRichPresence("location", Game1.currentLocation.Name);
        }

        private void AlwayFindTreasure()
        {
            if (Config.AlwaysFindTreasure)
                BarHasTreasure = true;
        }

        private void InstantCatchFish()
        {
            if (Config.InstantCatchFish)
            {
                if (BarHasTreasure)
                    BarTreasureCaught = true;
                BarDistanceFromCatching = 1.0f;
            }
        }

        private void InstantCatchTreasure()
        {
            if (Config.InstantCatchTreasure && (BarHasTreasure || Config.AlwaysFindTreasure))
                BarTreasureCaught = true;
        }

        private void OverrideFishDifficult()
        {
            BarDifficulty *= Config.FishDifficultyMultiplier;
            BarDifficulty += Config.FishDifficultyAdditive;
            if (BarDifficulty < 0) BarDifficulty = 0;
        }

        private void AlwayPerfectResult()
        {
            if (Config.AlwaysPerfect)
                BarPerfect = true;
        }

        private void MaxCastPower()
        {
            if (maxCastPower)
                RodCastPower = 1.01f;
        }
    }
}