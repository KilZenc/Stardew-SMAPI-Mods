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

        private int barWhichFish
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

        private int barFishSize
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

        private int barFishQuality
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

        private float barDifficulty
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

        private bool barPerfect
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

        private bool barCaughtDoubleFish
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, FieldName.caughtDoubleFish, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, FieldName.caughtDoubleFish, true).SetValue(value);
            }
        }

        private bool barFromFishPond
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

        private bool barBossFish
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

        private int barHeight // Hardcoded Max: 568
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

        private float barDistanceFromCatching
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

        private float barBobberPosition
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

        private float barPos
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

        private float barTreasurePosition
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

        private bool barHasTreasure
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

        private bool barTreasureCaught
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

        private float barTreasureCatchLevel
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

        private Vector2 barTreasureShake
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<Vector2>(bobberBar, FieldName.treasureShake, true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<Vector2>(bobberBar, FieldName.treasureShake, true).SetValue(value);
            }
        }

        private bool IsTreasureInBar()
        {
            return barTreasurePosition + 12.0 <= barPos - 32.0 + barHeight && barTreasurePosition - 16.0 >= barPos - 32.0;
        }

        private void HandleTreasureCatchError()
        {
            catchStep = barTreasureCatchLevel;
            Vector2 shakeValue = new Vector2(Game1.random.Next(-2, 3), Game1.random.Next(-2, 3));

            barTreasureCatchLevel = catchStep + 0.0135f * 2;
            barTreasureShake = shakeValue;

            if (barTreasureCatchLevel >= 1.0)
            {
                Game1.playSound("newArtifact");
                barTreasureCaught = true;
                catchStep = 0;
            }
        }
    }
}