using FishingAssistant.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Tools;
using System;

namespace FishingAssistant
{
    internal partial class ModEntry : Mod
    {
        private FishingRod fishingRod;

        private bool IsRodBobberStillInAir
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.castedButBobberStillInAir;
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

        private bool IsRodFishing
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.isFishing;
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

        private bool IsRodInUse
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.inUse();
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

        private bool IsRodPullingOutOfWater
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.pullingOutOfWater;
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

        private bool IsRodTimingCasting
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.isTimingCast;
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

        private float RodCastPower
        {
            set
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                fishingRod.castingPower = value;
            }
        }

        private int RodStackSize
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.Stack;
            }
            set
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                fishingRod.Stack = value;
            }
        }

        private int RodFishQuality
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return Helper.Reflection.GetField<int>(fishingRod, KeyHelper.fishQuality, true).GetValue();
            }
            set
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                Helper.Reflection.GetField<int>(fishingRod, KeyHelper.fishQuality, true).SetValue(value);
            }
        }

        private bool RodFromFishPond
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return Helper.Reflection.GetField<bool>(fishingRod, KeyHelper.fromFishPond, true).GetValue();
            }
            set
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                Helper.Reflection.GetField<bool>(fishingRod, KeyHelper.fromFishPond, true).SetValue(value);
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
        private ItemMetadata RodWhichFish
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.whichFish;
            }
            set
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                fishingRod.whichFish = value;
            }
        }

        /// <summary>Is player able to cast fishing rod</summary>
        /// <returns>True if player can cast fishing rod</returns>
        private bool RodIsNotInUse()
        {
            return Context.CanPlayerMove && Game1.activeClickableMenu == null && !fishingRod.inUse();
        }

        /// <summary>Is fish bite or not</summary>
        /// <returns>True if fish bite</returns>
        private bool IsRodCanHook()
        {
            return IsRodNibbing && !IsRodReeling && !IsRodHit && !IsRodPullingOutOfWater && !IsRodFishCaught;
        }

        /// <summary>Is player currenly showing fish</summary>
        /// <returns>True if fish popup is show</returns>
        private bool IsRodShowingFish()
        {
            return !Context.CanPlayerMove && IsRodFishCaught && IsRodInUse && !IsRodCasting && !IsRodTimingCasting && !IsRodReeling && !IsRodPullingOutOfWater && !IsRodShowingTreasure;
        }
    }
}