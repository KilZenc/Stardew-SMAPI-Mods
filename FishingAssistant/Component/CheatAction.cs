using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace FishingAssistant
{
    internal partial class ModEntry : Mod
    {
        /// <summary>Make bait and tackle last long forever</summary>
        private void ApplyInfiniteBaitAndTackle()
        {
            if (!(Context.IsWorldReady && fishingRod != null))
                return;

            if (Config.InfiniteBait && fishingRod.attachments?.Length > 0 && fishingRod.attachments[0] != null && fishingRod.attachments[0].Stack < fishingRod.attachments[0].maximumStackSize())
                fishingRod.attachments[0].Stack = fishingRod.attachments[0].maximumStackSize();

            if (Config.InfiniteTackle && fishingRod.attachments?.Length > 1 && fishingRod.attachments[1] != null && fishingRod.attachments[1].uses.Value != 0)
                fishingRod.attachments[1].uses.Value = 0;
        }

        /// <summary>Make fish instantly bite when fishing</summary>
        private void InstantFishBite()
        {
            if (Config.InstantFishBite && (IsRodFishing && !IsRodHit && RodTimeUntilFishBite > 0))
                RodTimeUntilFishBite = 0;
        }

        /// <summary>End fishing minigame</summary>
        private void EndMiniGame()
        {
            if (Game1.isFestival())
                return;

            Game1.playSound("jingle1");

            int attachmentValue = fishingRod.attachments[0] == null ? -1 : fishingRod.attachments[0].parentSheetIndex;
            bool caughtDouble = Config.AlwaysCatchDoubleFish || (!BarBossFish && attachmentValue == 774 && Game1.random.NextDouble() < 0.25 + Game1.player.DailyLuck / 2.0);

            fishingRod.pullFishFromWater(BarWhichFish, BarFishSize, BarFishQuality, (int)BarDifficulty, BarTreasureCaught, BarPerfect, BarFromFishPond, caughtDouble);
            Game1.exitActiveMenu();

            Game1.setRichPresence("location", Game1.currentLocation.Name);
        }

        /// <summary>Force treasure spawn every time when fishing</summary>
        private void AlwayFindTreasure()
        {
            if (Config.AlwaysFindTreasure && !Game1.isFestival())
                BarHasTreasure = true;
        }

        /// <summary>Make fish instantly catch, simply skip minigame</summary>
        private void InstantCatchFish()
        {
            if (Config.InstantCatchFish)
            {
                if (BarHasTreasure)
                    BarTreasureCaught = true;
                BarDistanceFromCatching = 1.0f;
            }
        }

        /// <summary>Make treasure is instantly caught when appearing</summary>
        private void InstantCatchTreasure()
        {
            if (Config.InstantCatchTreasure && (BarHasTreasure || Config.AlwaysFindTreasure))
                BarTreasureCaught = true;
        }

        /// <summary>Override fish difficult</summary>
        private void OverrideFishDifficult()
        {
            BarDifficulty *= Config.FishDifficultyMultiplier;
            BarDifficulty += Config.FishDifficultyAdditive;
            if (BarDifficulty < 0) BarDifficulty = 0;
        }

        /// <summary>Force fishing minigame alway perfect</summary>
        private void AlwayPerfectResult()
        {
            if (Config.AlwaysPerfect)
                BarPerfect = true;
        }

        /// <summary>Force fishing rod cast power to max</summary>
        private void MaxCastPower()
        {
            if (maxCastPower)
                RodCastPower = 1.01f;
        }
    }
}