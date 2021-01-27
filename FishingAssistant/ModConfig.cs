namespace FishingAssistant
{
    internal class ModConfig
    {
        /// <summary>Whether the game should consider every catch to be perfectly executed, even if it wasn't.</summary>
        public bool AlwaysPerfect { get; set; }

        /// <summary>Whether to always find treasure.</summary>
        public bool AlwaysFindTreasure { get; set; }

        /// <summary>Whether to catch fish instantly.</summary>
        public bool InstantCatchFish { get; set; }

        /// <summary>Whether to catch treasure instantly.</summary>
        public bool InstantCatchTreasure { get; set; }

        /// <summary>Whether fishing tackles last forever.</summary>
        public bool InfiniteTackle { get; set; } = true;

        /// <summary>Whether fishing bait lasts forever.</summary>
        public bool InfiniteBait { get; set; } = true;

        /// <summary>A multiplier applied to the fish difficulty. This can a number between 0 and 1 to lower difficulty, or more than 1 to increase it.</summary>
        public float FishDifficultyMultiplier { get; set; } = 1;

        /// <summary>A value added to the fish difficulty. This can be less than 0 to decrease difficulty, or more than 0 to increase it.</summary>
        public float FishDifficultyAdditive { get; set; } = 0;
    }
}