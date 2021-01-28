using StardewModdingAPI;
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
    }
}