using StardewModdingAPI;
using StardewValley;
using StardewValley.Tools;
using System;

namespace FishingAssistant
{
    partial class ModEntry : Mod
    {
        private FishingRod fishingRod;

        private int whichFish
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return Helper.Reflection.GetField<int>(fishingRod, nameof(whichFish), true).GetValue();
            }
            set
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                Helper.Reflection.GetField<int>(fishingRod, nameof(whichFish), true).SetValue(value);
            }
        }

        private int fishQuality
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return Helper.Reflection.GetField<int>(fishingRod, nameof(fishQuality), true).GetValue();
            }
            set
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                Helper.Reflection.GetField<int>(fishingRod, nameof(fishQuality), true).SetValue(value);
            }
        }

        private bool caughtDoubleFish
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return Helper.Reflection.GetField<bool>(fishingRod, nameof(caughtDoubleFish), true).GetValue();
            }
            set
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                Helper.Reflection.GetField<bool>(fishingRod, nameof(caughtDoubleFish), true).SetValue(value);
            }
        }

        private bool fromFishPond
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return Helper.Reflection.GetField<bool>(fishingRod, nameof(fromFishPond), true).GetValue();
            }
            set
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                Helper.Reflection.GetField<bool>(fishingRod, nameof(fromFishPond), true).SetValue(value);
            }
        }

        private bool IsRodFishing
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.isFishing;
            }
        }

        private bool IsRodCasting
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.isCasting;
            }
        }

        private bool IsRodTimingCasting
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.isTimingCast;
            }
        }

        private bool IsRodNibbing
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.isNibbling;
            }
        }

        private bool IsRodHit
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.hit;
            }
        }

        private bool IsRodBobberStillInAir
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.castedButBobberStillInAir;
            }
        }

        private bool IsRodReeling
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.isReeling;
            }
        }

        private bool IsRodPullingOutOfWater
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.pullingOutOfWater;
            }
        }

        private bool IsRodFishCaught
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.fishCaught;
            }
            set
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                fishingRod.fishCaught = value;
            }
        }

        private bool IsRodTreasureCaught
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.treasureCaught;
            }
        }

        private bool IsRodShowingTreasure
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.showingTreasure;
            }
            set
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                fishingRod.showingTreasure = value;
            }
        }

        private float RodTimeUntilFishBite
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.timeUntilFishingBite;
            }
            set
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                fishingRod.timeUntilFishingBite = value;
            }
        }

        private float RodCastPower
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.castingPower;
            }
            set
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                fishingRod.castingPower = value;
            }
        }

        private bool IsRodInUse
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.inUse();
            }
        }

        private bool CanCastFishingRod()
        {
            return Context.CanPlayerMove && Game1.activeClickableMenu == null && !fishingRod.inUse();
        }

        private bool IsRodCanHook()
        {
            return (IsRodNibbing && !IsRodReeling && !IsRodHit && !IsRodPullingOutOfWater && !IsRodFishCaught);
        }

        private bool IsRodShowingFish()
        {
            return !Context.CanPlayerMove && IsRodFishCaught && IsRodInUse && !IsRodCasting && !IsRodTimingCasting && !IsRodReeling && !IsRodPullingOutOfWater && !IsRodShowingTreasure;
        }
    }
}