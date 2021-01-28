using StardewModdingAPI;
using StardewValley;
using StardewValley.Tools;
using System;

namespace FishingAssistant
{
    partial class ModEntry : Mod
    {
        private FishingRod fishingRod;

        private bool IsRodFishing
        {
            get
            {
                if (fishingRod == null) throw new NullReferenceException(nameof(fishingRod));
                return fishingRod.isFishing;
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

        private bool CanCastFishingRod()
        {
            return Context.CanPlayerMove && Game1.activeClickableMenu == null && !fishingRod.inUse();
        }

        private bool IsRodCanHook()
        {
            return (IsRodNibbing && !IsRodReeling && !IsRodHit && !IsRodPullingOutOfWater && !IsRodFishCaught);
        }
    }
}