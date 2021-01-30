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
                return Helper.Reflection.GetField<float>(bobberBar, FieldName.bobberPosition, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, FieldName.bobberPosition, true).SetValue(value);
            }
        }

        private bool BarBossFish
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, FieldName.bossFish, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, FieldName.bossFish, true).SetValue(value);
            }
        }

        private float BarDifficulty
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, FieldName.difficulty, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, FieldName.difficulty, true).SetValue(value);
            }
        }

        private float BarDistanceFromCatching
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, FieldName.distanceFromCatching, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, FieldName.distanceFromCatching, true).SetValue(value);
            }
        }

        private bool BarFadeOut
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, FieldName.fadeOut, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, FieldName.fadeOut, true).SetValue(value);
            }
        }

        private int BarFishQuality
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<int>(bobberBar, FieldName.fishQuality, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<int>(bobberBar, FieldName.fishQuality, true).SetValue(value);
            }
        }

        private int BarFishSize
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<int>(bobberBar, FieldName.fishSize, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<int>(bobberBar, FieldName.fishSize, true).SetValue(value);
            }
        }

        private bool BarFromFishPond
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, FieldName.fromFishPond, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, FieldName.fromFishPond, true).SetValue(value);
            }
        }

        private bool BarHasTreasure
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, FieldName.treasure, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, FieldName.treasure, true).SetValue(value);
            }
        }

        private int BarHeight // Hardcoded Max: 568
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<int>(bobberBar, FieldName.bobberBarHeight, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<int>(bobberBar, FieldName.bobberBarHeight, true).SetValue(value);
            }
        }

        private bool BarPerfect
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, FieldName.perfect, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, FieldName.perfect, true).SetValue(value);
            }
        }

        private float BarPosition
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, FieldName.bobberBarPos, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, FieldName.bobberBarPos, true).SetValue(value);
            }
        }

        private float BarTreasureCatchLevel
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, FieldName.treasureCatchLevel, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, FieldName.treasureCatchLevel, true).SetValue(value);
            }
        }

        private bool BarTreasureCaught
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, FieldName.treasureCaught, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, FieldName.treasureCaught, true).SetValue(value);
            }
        }

        private float BarTreasurePosition
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, FieldName.treasurePosition, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, FieldName.treasurePosition, true).SetValue(value);
            }
        }

        private Vector2 BarTreasureShake
        {
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<Vector2>(bobberBar, FieldName.treasureShake, true).SetValue(value);
            }
        }

        private int BarWhichFish
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<int>(bobberBar, FieldName.whichFish, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<int>(bobberBar, FieldName.whichFish, true).SetValue(value);
            }
        }

        /// <summary>Temporarily mothod for handle treasure catch error</summary>
        private void HandleTreasureCatchError()
        {
            catchStep = BarTreasureCatchLevel;
            Vector2 shakeValue = new Vector2(Game1.random.Next(-2, 3), Game1.random.Next(-2, 3));

            BarTreasureCatchLevel = catchStep + 0.0135f * 2;
            BarTreasureShake = shakeValue;

            if (BarTreasureCatchLevel >= 1.0)
            {
                Game1.playSound("newArtifact");
                BarTreasureCaught = true;
                catchStep = 0;
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