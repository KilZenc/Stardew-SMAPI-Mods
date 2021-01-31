using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.BellsAndWhistles;

namespace FishingAssistant
{
    partial class ModEntry : Mod
    {
        private SparklingText sparkleText;

        /// <summary>Make bait and tackle last long forever</summary>
        private void ApplyInfiniteBaitAndTackle(UpdateTickedEventArgs e)
        {
            if (!(Context.IsWorldReady && e.IsOneSecond && fishingRod != null))
                return;

            if (Config.InfiniteBait && fishingRod.attachments?.Length > 0 && fishingRod.attachments[0] != null)
                fishingRod.attachments[0].Stack = fishingRod.attachments[0].maximumStackSize();

            if (Config.InfiniteTackle && fishingRod.attachments?.Length > 1 && fishingRod.attachments[1] != null)
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

            if (BarPerfect)
                sparkleText = new SparklingText(Game1.dialogueFont, Game1.content.LoadString("Strings\\UI:BobberBar_Perfect"), Color.Yellow, Color.White, rainbow: false, 0.1, 1500);

            if (bobberBar.readyToClose())
            {
                Game1.playSound("jingle1");

                int attachmentValue = fishingRod.attachments[0] == null ? -1 : fishingRod.attachments[0].parentSheetIndex;
                bool caughtDouble = Config.AlwaysCatchDoubleFish || (!BarBossFish && attachmentValue == 774 && Game1.random.NextDouble() < 0.25 + Game1.player.DailyLuck / 2.0);

                fishingRod.pullFishFromWater(BarWhichFish, BarFishSize, BarFishQuality, (int)BarDifficulty, BarTreasureCaught, BarPerfect, BarFromFishPond, caughtDouble);
                Game1.exitActiveMenu();
            }
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