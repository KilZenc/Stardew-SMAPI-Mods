using StardewModdingAPI;

namespace FishingAssistant
{
    internal class ModConfig
    {
        /// <summary>The default values.</summary>
        public static ModConfig Defaults { get; } = new ModConfig();

        /// <summary>Button for toggle max or free fishing rod cast power</summary>
        public SButton EnableModButton { get; set; } = SButton.F5;

        /// <summary>Button for toggle max or free fishing rod cast power</summary>
        public SButton CastPowerButton { get; set; } = SButton.F6;

        /// <summary>Button for toggle catch or ignore treasure in fishing minigame</summary>
        public SButton CatchTreasureButton { get; set; } = SButton.F7;

        /// <summary>Button for roload config file</summary>
        public SButton OpenMenuButton { get; set; } = SButton.F8;

        /// <summary>Whether the game should consider every catch to be perfectly executed, even if it wasn't.</summary>
        public bool AlwaysPerfect { get; set; } = false;

        /// <summary>Whether to always find treasure.</summary>
        public bool AlwaysFindTreasure { get; set; } = false;

        /// <summary>Make fish to bite  instantly.</summary>
        public bool InstantFishBite { get; set; } = false;

        /// <summary>Whether to catch fish instantly.</summary>
        public bool InstantCatchFish { get; set; } = false;

        /// <summary>Whether to catch treasure instantly.</summary>
        public bool InstantCatchTreasure { get; set; } = false;

        /// <summary>Whether fishing tackles last forever.</summary>
        public bool InfiniteTackle { get; set; } = false;

        /// <summary>Whether fishing bait lasts forever.</summary>
        public bool InfiniteBait { get; set; } = false;

        /// <summary>A multiplier applied to the fish difficulty. This can a number between 0 and 1 to lower difficulty, or more than 1 to increase it.</summary>
        public float FishDifficultyMultiplier { get; set; } = 1;

        /// <summary>A value added to the fish difficulty. This can be less than 0 to decrease difficulty, or more than 0 to increase it.</summary>
        public float FishDifficultyAdditive { get; set; } = 0;

        /// <summary>Make you catch double fish every time</summary>
        public bool AlwaysCatchDoubleFish { get; set; } = false;

        /// <summary>Position to display fish info when playing fishing minigame</summary>
        public string ModStatusDisplayPosition { get; set; } = "Right";

        /// <summary>Should mod show fish info while catching fish?</summary>
        public bool DisplayFishInfo { get; set; } = true;

        /// <summary>Position to display fish info when playing fishing minigame</summary>
        public string FishInfoDisplayPosition { get; set; } = "UpperRight";

        /// <summary>Show fish name with fish info</summary>
        public bool ShowFishName { get; set; } = true;

        /// <summary>Show treasure with fish info</summary>
        public bool ShowTreasure { get; set; } = true;

        /// <summary>Show fish info whether uncaught or not</summary>
        public bool ShowUncaughtFishSpecies { get; set; } = false;

        /// <summary>Alway show fish info if current fish is legendary</summary>
        public bool AlwaysShowLegendaryFish { get; set; } = false;

        /// <summary>Add auto-Hook enchantment to fishing rod</summary>
        public bool AddAutoHookEnchantment { get; set; } = false;

        /// <summary>Add Efficient enchantment to fishing rod</summary>
        public bool AddEfficientEnchantment { get; set; } = false;

        /// <summary>Add Master enchantment to fishing rod</summary>
        public bool AddMasterEnchantment { get; set; } = false;

        /// <summary>Add Preserving enchantment to fishing rod</summary>
        public bool AddPreservingEnchantment { get; set; } = false;

        /// <summary>Add Preserving enchantment to fishing rod</summary>
        public bool OnlyAddEnchantmentWhenHeld { get; set; } = false;

        /// <summary></summary>
        public string AutomationPresets { get; set; } = "Default";

        /// <summary>Auto cast fishing rod on custon preset</summary>
        public bool AutoCastFishingRod { get; set; } = true;

        /// <summary>Auto hook fish rod on custon preset</summary>
        public bool AutoHookFish { get; set; } = true;

        /// <summary>Auto play fishing mini-game on custon preset</summary>
        public bool AutoPlayMiniGame { get; set; } = true;

        /// <summary>Auto close fish popup on custon preset</summary>
        public bool AutoClosePopup { get; set; } = true;

        /// <summary>Auto loot treasure on custon preset</summary>
        public bool AutoLootTreasure { get; set; } = true;

        /// <summary>Shold mod auto attach bait if possible.</summary>
        public bool AutoAttachBait { get; set; } = false;

        /// <summary>Shold mod auto attach bait if possible.</summary>
        public bool AutoAttachTackles { get; set; } = false;

        /// <summary>Whether to pause fishing on given time</summary>
        public bool EnableAutoPauseFishing { get; set; } = true;

        /// <summary>Time to stop fishing</summary>
        public int PauseFishingTime { get; set; } = 24;

        /// <summary>Whether to eat some food if need</summary>
        public bool EnableAutoEatFood { get; set; } = false;

        /// <summary>Amount of energy in percent to find food to eat</summary>
        public int EnergyPrecentToEat { get; set; } = 0;

        /// <summary>Allow to eat fish that you caught</summary>
        public bool AllowEatingFish { get; set; } = false;
    }
}