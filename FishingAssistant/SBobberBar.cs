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

        private int bobberBarHeight // Hardcoded Max: 568
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<int>(bobberBar, nameof(bobberBarHeight), true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<int>(bobberBar, nameof(bobberBarHeight), true).SetValue(value);
            }
        }

        private float difficulty
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, nameof(difficulty), true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, nameof(difficulty), true).SetValue(value);
            }
        }

        private float distanceFromCatching
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, nameof(distanceFromCatching), true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, nameof(distanceFromCatching), true).SetValue(value);
            }
        }

        private float bobberPosition
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, nameof(bobberPosition), true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, nameof(bobberPosition), true).SetValue(value);
            }
        }

        private float bobberBarPos
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, nameof(bobberBarPos), true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, nameof(bobberBarPos), true).SetValue(value);
            }
        }

        private float treasurePosition
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, nameof(treasurePosition), true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, nameof(treasurePosition), true).SetValue(value);
            }
        }

        private bool perfect
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, nameof(perfect), true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, nameof(perfect), true).SetValue(value);
            }
        }

        private bool treasure
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, nameof(treasure), true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, nameof(treasure), true).SetValue(value);
            }
        }

        private bool treasureCaught
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<bool>(bobberBar, nameof(treasureCaught), true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<bool>(bobberBar, nameof(treasureCaught), true).SetValue(value);
            }
        }

        private float treasureCatchLevel
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<float>(bobberBar, nameof(treasureCatchLevel), true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<float>(bobberBar, nameof(treasureCatchLevel), true).SetValue(value);
            }
        }

        private Vector2 treasureShake
        {
            get
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                return Helper.Reflection.GetField<Vector2>(bobberBar, nameof(treasureShake), true).GetValue();
            }
            set
            {
                if (bobberBar == null) throw new NullReferenceException(nameof(bobberBar));
                Helper.Reflection.GetField<Vector2>(bobberBar, nameof(treasureShake), true).SetValue(value);
            }
        }

        private bool IsTreasureInBar()
        {
            return treasurePosition + 12.0 <= bobberBarPos - 32.0 + bobberBarHeight && treasurePosition - 16.0 >= bobberBarPos - 32.0;
        }

        private void HandleTreasureCatchError()
        {
            catchStep = treasureCatchLevel;
            Vector2 shakeValue = new Vector2(Game1.random.Next(-2, 3), Game1.random.Next(-2, 3));

            treasureCatchLevel = catchStep + 0.0135f * 2;
            treasureShake = shakeValue;

            if (treasureCatchLevel >= 1.0)
            {
                Game1.playSound("newArtifact");
                treasureCaught = true;
                catchStep = 0;
            }
        }
    }
}