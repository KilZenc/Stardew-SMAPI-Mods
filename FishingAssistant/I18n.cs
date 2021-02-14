using System;
using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using StardewModdingAPI;

namespace FishingAssistant
{
    /// <summary>Get translations from the mod's <c>i18n</c> folder.</summary>
    /// <remarks>This is auto-generated from the <c>i18n/default.json</c> file when the T4 template is saved.</remarks>
    [GeneratedCode("TextTemplatingFileGenerator", "1.0.0")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Deliberately named for consistency and to match translation conventions.")]
    internal static class I18n
    {
        /*********
        ** Fields
        *********/
        /// <summary>The mod's translation helper.</summary>
        private static ITranslationHelper Translations;


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="translations">The mod's translation helper.</param>
        public static void Init(ITranslationHelper translations)
        {
            I18n.Translations = translations;
        }

        /// <summary>Get a translation equivalent to "Fishing Assistant".</summary>
        public static string Mod_Name()
        {
            return I18n.GetByKey("mod.name");
        }

        /// <summary>Get a translation equivalent to "Enable".</summary>
        public static string Mod_Status_Enable()
        {
            return I18n.GetByKey("mod.status.enable");
        }

        /// <summary>Get a translation equivalent to "Disable".</summary>
        public static string Mod_Status_Disable()
        {
            return I18n.GetByKey("mod.status.disable");
        }

        /// <summary>Get a translation equivalent to "{0} Fishing Assistant.".</summary>
        public static string Hud_Message_Mod_Toggle()
        {
            return I18n.GetByKey("hud.message.mod.toggle");
        }

        /// <summary>Get a translation equivalent to "{0} Max cast power.".</summary>
        public static string Hud_Message_Cast_Power()
        {
            return I18n.GetByKey("hud.message.cast.power");
        }

        /// <summary>Get a translation equivalent to "{0} Catch treasure.".</summary>
        public static string Hud_Message_Catch_Treasure()
        {
            return I18n.GetByKey("hud.message.catch.treasure");
        }

        /// <summary>Get a translation equivalent to "Mod will disable after task is finished.".</summary>
        public static string Hud_Message_Request_Disable()
        {
            return I18n.GetByKey("hud.message.request.disable");
        }

        /// <summary>Get a translation equivalent to "Pressed {0} again to force enable.".</summary>
        public static string Hud_Message_Force_Enable()
        {
            return I18n.GetByKey("hud.message.force.enable");
        }

        /// <summary>Get a translation equivalent to "Low energy.".</summary>
        public static string Hud_Message_Low_Stamina()
        {
            return I18n.GetByKey("hud.message.low.stamina");
        }

        /// <summary>Get a translation equivalent to "Inventory full.".</summary>
        public static string Hud_Message_Inventory_Full()
        {
            return I18n.GetByKey("hud.message.inventory.full");
        }

        /// <summary>Get a translation equivalent to "Auto disable mod on {0}".</summary>
        public static string Hud_Message_Auto_Disable()
        {
            return I18n.GetByKey("hud.message.auto.disable");
        }

        /// <summary>Get a translation equivalent to "Saving new config".</summary>
        public static string Hud_Message_Config_Saved()
        {
            return I18n.GetByKey("hud.message.config.saved");
        }

        /// <summary>Get a translation equivalent to "Control".</summary>
        public static string Config_Menu_Header_Control()
        {
            return I18n.GetByKey("config.menu.header.control");
        }

        /// <summary>Get a translation equivalent to "Toggle Mod Button".</summary>
        public static string Config_Menu_Button_Toggle_Mod()
        {
            return I18n.GetByKey("config.menu.button.toggle.mod");
        }

        /// <summary>Get a translation equivalent to "Cast power button".</summary>
        public static string Config_Menu_Button_Max_Cast_Power()
        {
            return I18n.GetByKey("config.menu.button.max.cast.power");
        }

        /// <summary>Get a translation equivalent to "Catch treasure button".</summary>
        public static string Config_Menu_Button_Catch_Treasure()
        {
            return I18n.GetByKey("config.menu.button.catch.treasure");
        }

        /// <summary>Get a translation equivalent to "Open menu button".</summary>
        public static string Config_Menu_Button_Open_Menu()
        {
            return I18n.GetByKey("config.menu.button.open.menu");
        }

        /// <summary>Get a translation equivalent to "Reset".</summary>
        public static string Config_Menu_Reset_Control()
        {
            return I18n.GetByKey("config.menu.reset.control");
        }

        /// <summary>Get a translation equivalent to "Press New Key...".</summary>
        public static string Config_Menu_PressNewKey()
        {
            return I18n.GetByKey("config.menu.press-new-key");
        }

        /// <summary>Get a translation equivalent to "General".</summary>
        public static string Config_Menu_Header_General()
        {
            return I18n.GetByKey("config.menu.header.general");
        }

        /// <summary>Get a translation equivalent to "Mod status position".</summary>
        public static string Config_Menu_Mod_Status_Position()
        {
            return I18n.GetByKey("config.menu.mod.status.position");
        }

        /// <summary>Get a translation equivalent to "Enable auto pause fishing".</summary>
        public static string Config_Menu_Enable_Auto_Pause_Fishing()
        {
            return I18n.GetByKey("config.menu.enable.auto.pause.fishing");
        }

        /// <summary>Get a translation equivalent to "Pause fishing time".</summary>
        public static string Config_Menu_Pause_Fishing_Time()
        {
            return I18n.GetByKey("config.menu.pause.fishing.time");
        }

        /// <summary>Get a translation equivalent to "Fishing rod".</summary>
        public static string Config_Menu_Header_Fishing_Rod()
        {
            return I18n.GetByKey("config.menu.header.fishing.rod");
        }

        /// <summary>Get a translation equivalent to "Infinite bait".</summary>
        public static string Config_Menu_Label_Infinite_Bait()
        {
            return I18n.GetByKey("config.menu.label.infinite.bait");
        }

        /// <summary>Get a translation equivalent to "Infinite tackle".</summary>
        public static string Config_Menu_Label_Infinite_Tackle()
        {
            return I18n.GetByKey("config.menu.label.infinite.tackle");
        }

        /// <summary>Get a translation equivalent to "Fishing mini-game".</summary>
        public static string Config_Menu_Header_MiniGame()
        {
            return I18n.GetByKey("config.menu.header.mini-game");
        }

        /// <summary>Get a translation equivalent to "Always Perfect".</summary>
        public static string Config_Menu_Label_Always_Perfect()
        {
            return I18n.GetByKey("config.menu.label.always.perfect");
        }

        /// <summary>Get a translation equivalent to "Always catch double fish".</summary>
        public static string Config_Menu_Label_Always_Catch_Double()
        {
            return I18n.GetByKey("config.menu.label.always.catch.double");
        }

        /// <summary>Get a translation equivalent to "Always found treasure".</summary>
        public static string Config_Menu_Label_Always_Treasure()
        {
            return I18n.GetByKey("config.menu.label.always.treasure");
        }

        /// <summary>Get a translation equivalent to "Instant fish bite".</summary>
        public static string Config_Menu_Label_Instant_Fishbite()
        {
            return I18n.GetByKey("config.menu.label.instant.fishbite");
        }

        /// <summary>Get a translation equivalent to "Instant catch fish".</summary>
        public static string Config_Menu_Label_Instant_Catch_Fish()
        {
            return I18n.GetByKey("config.menu.label.instant.catch.fish");
        }

        /// <summary>Get a translation equivalent to "Instant catch treasure".</summary>
        public static string Config_Menu_Label_Instant_Catch_Treasure()
        {
            return I18n.GetByKey("config.menu.label.instant.catch.treasure");
        }

        /// <summary>Get a translation equivalent to "Fish difficulty multiplier".</summary>
        public static string Config_Menu_Label_Fish_Difficulty_Multiplier()
        {
            return I18n.GetByKey("config.menu.label.fish.difficulty.multiplier");
        }

        /// <summary>Get a translation equivalent to "Fish difficulty additive".</summary>
        public static string Config_Menu_Label_Fish_Difficulty_Additive()
        {
            return I18n.GetByKey("config.menu.label.fish.difficulty.additive");
        }

        /// <summary>Get a translation equivalent to "Value other than this can be set in config file".</summary>
        public static string Config_Menu_Description_Fish_Difficulty()
        {
            return I18n.GetByKey("config.menu.description.fish.difficulty");
        }

        /// <summary>Get a translation equivalent to "You can change the on-screen buttons in the config files for the Fishing Assistant and Virtual Keyboard mods.".</summary>
        public static string Config_Menu_AndroidConfigNote()
        {
            return I18n.GetByKey("config.menu.android-config-note");
        }

        /// <summary>Get a translation equivalent to "Fishing information".</summary>
        public static string Config_Menu_Header_Fish_Info()
        {
            return I18n.GetByKey("config.menu.header.fish.info");
        }

        /// <summary>Get a translation equivalent to "Fish info position".</summary>
        public static string Config_Menu_Label_Fishinfo_Display_Position()
        {
            return I18n.GetByKey("config.menu.label.fishinfo.display.position");
        }

        /// <summary>Get a translation equivalent to "Show fish name".</summary>
        public static string Config_Menu_Label_Show_Fish_Name()
        {
            return I18n.GetByKey("config.menu.label.show.fish.name");
        }

        /// <summary>Get a translation equivalent to "Show treasure".</summary>
        public static string Config_Menu_Label_Show_Treasure()
        {
            return I18n.GetByKey("config.menu.label.show.treasure");
        }

        /// <summary>Get a translation equivalent to "Show uncaught fish".</summary>
        public static string Config_Menu_Label_Show_Uncaught_Fish()
        {
            return I18n.GetByKey("config.menu.label.show.uncaught.fish");
        }

        /// <summary>Get a translation equivalent to "Show legendary fish".</summary>
        public static string Config_Menu_Label_Show_Legendaly_Fish()
        {
            return I18n.GetByKey("config.menu.label.show.legendaly.fish");
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Get a translation by its key.</summary>
        /// <param name="key">The translation key.</param>
        /// <param name="tokens">An object containing token key/value pairs. This can be an anonymous object (like <c>new { value = 42, name = "Cranberries" }</c>), a dictionary, or a class instance.</param>
        private static Translation GetByKey(string key, object tokens = null)
        {
            if (I18n.Translations == null)
                throw new InvalidOperationException($"You must call {nameof(I18n)}.{nameof(I18n.Init)} from the mod's entry method before reading translations.");
            return I18n.Translations.Get(key, tokens);
        }
    }
}

