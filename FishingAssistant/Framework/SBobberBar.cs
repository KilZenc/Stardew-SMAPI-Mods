using FishingAssistant.Framework;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System;

namespace FishingAssistant
{
    partial class ModEntry : Mod
    {
        private BobberBar bobberBar;

        private float BarBobberPosition
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, KeyHelper.bobberPosition, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, KeyHelper.bobberPosition, true).SetValue(value);
            }
        }

        private bool BarBossFish
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, KeyHelper.bossFish, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, KeyHelper.bossFish, true).SetValue(value);
            }
        }

        private float BarDifficulty
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, KeyHelper.difficulty, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, KeyHelper.difficulty, true).SetValue(value);
            }
        }

        private float BarDistanceFromCatching
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, KeyHelper.distanceFromCatching, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, KeyHelper.distanceFromCatching, true).SetValue(value);
            }
        }

        private bool BarFadeOut
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, KeyHelper.fadeOut, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, KeyHelper.fadeOut, true).SetValue(value);
            }
        }

        private int BarFishQuality
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<int>(bobberBar, KeyHelper.fishQuality, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<int>(bobberBar, KeyHelper.fishQuality, true).SetValue(value);
            }
        }

        private int BarFishSize
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<int>(bobberBar, KeyHelper.fishSize, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<int>(bobberBar, KeyHelper.fishSize, true).SetValue(value);
            }
        }

        private bool BarFromFishPond
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, KeyHelper.fromFishPond, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, KeyHelper.fromFishPond, true).SetValue(value);
            }
        }

        private bool BarHasTreasure
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, KeyHelper.treasure, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, KeyHelper.treasure, true).SetValue(value);
            }
        }

        private int BarHeight // Hardcoded Max: 568
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<int>(bobberBar, KeyHelper.bobberBarHeight, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<int>(bobberBar, KeyHelper.bobberBarHeight, true).SetValue(value);
            }
        }

        private bool BarPerfect
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, KeyHelper.perfect, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, KeyHelper.perfect, true).SetValue(value);
            }
        }

        private float BarPosition
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, KeyHelper.bobberBarPos, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, KeyHelper.bobberBarPos, true).SetValue(value);
            }
        }

        private float BarTreasureCatchLevel
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, KeyHelper.treasureCatchLevel, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, KeyHelper.treasureCatchLevel, true).SetValue(value);
            }
        }

        private bool BarTreasureCaught
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, KeyHelper.treasureCaught, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, KeyHelper.treasureCaught, true).SetValue(value);
            }
        }

        private float BarTreasurePosition
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, KeyHelper.treasurePosition, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, KeyHelper.treasurePosition, true).SetValue(value);
            }
        }

        private Vector2 BarTreasureShake
        {
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<Vector2>(bobberBar, KeyHelper.treasureShake, true).SetValue(value);
            }
        }

        private int BarWhichFish
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<int>(bobberBar, KeyHelper.whichFish, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<int>(bobberBar, KeyHelper.whichFish, true).SetValue(value);
            }
        }

        /// <summary>Temporarily mothod for handle treasure catch error</summary>
        private void HandleTreasureCatchError()
        {
            treasureCatchStep = BarTreasureCatchLevel;
            Vector2 shakeValue = new Vector2(Game1.random.Next(-2, 3), Game1.random.Next(-2, 3));

            BarTreasureCatchLevel = treasureCatchStep + 0.0135f * 2;
            BarTreasureShake = shakeValue;

            if (BarTreasureCatchLevel >= 1.0)
            {
                Game1.playSound("newArtifact");
                BarTreasureCaught = true;
                treasureCatchStep = 0;
            }
        }

        /// <summary>Check if treasure chest is in bobber bar or not</summary>
        /// <returns>True if treasure chest is ni bobber bar</returns>
        private bool IsTreasureInBar()
        {
            return BarTreasurePosition + 12.0 <= BarPosition - 32.0 + BarHeight && BarTreasurePosition - 16.0 >= BarPosition - 32.0;
        }
    }
}