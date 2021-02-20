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

        /// <summary>Get a translation equivalent to "Left".</summary>
        public static string General_Position_Left()
        {
            return I18n.GetByKey("general.position.left");
        }

        /// <summary>Get a translation equivalent to "Right".</summary>
        public static string General_Position_Right()
        {
            return I18n.GetByKey("general.position.right");
        }

        /// <summary>Get a translation equivalent to "Top".</summary>
        public static string General_Position_Top()
        {
            return I18n.GetByKey("general.position.top");
        }

        /// <summary>Get a translation equivalent to "Bottom".</summary>
        public static string General_Position_Bottom()
        {
            return I18n.GetByKey("general.position.bottom");
        }

        /// <summary>Get a translation equivalent to "UpperRight".</summary>
        public static string General_Position_UpperRight()
        {
            return I18n.GetByKey("general.position.upper-right");
        }

        /// <summary>Get a translation equivalent to "UpperLeft".</summary>
        public static string General_Position_UpperLeft()
        {
            return I18n.GetByKey("general.position.upper-left");
        }

        /// <summary>Get a translation equivalent to "LowerRight".</summary>
        public static string General_Position_LowerRight()
        {
            return I18n.GetByKey("general.position.lower-right");
        }

        /// <summary>Get a translation equivalent to "LowerLeft".</summary>
        public static string General_Position_LowerLeft()
        {
            return I18n.GetByKey("general.position.lower-left");
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

        /// <summary>Get a translation equivalent to "Auto attach bait : {0}".</summary>
        public static string Hud_Message_Auto_Attach_Bait()
        {
            return I18n.GetByKey("hud.message.auto.attach.bait");
        }

        /// <summary>Get a translation equivalent to "Auto attach tackle : {0}".</summary>
        public static string Hud_Message_Auto_Attach_Tackle()
        {
            return I18n.GetByKey("hud.message.auto.attach.tackle");
        }

        /// <summary>Get a translation equivalent to "Control".</summary>
        public static string Menu_Config_Header_Control()
        {
            return I18n.GetByKey("menu.config.header.control");
        }

        /// <summary>Get a translation equivalent to "Toggle Mod Button".</summary>
        public static string Menu_Config_Button_Toggle_Mod()
        {
            return I18n.GetByKey("menu.config.button.toggle.mod");
        }

        /// <summary>Get a translation equivalent to "Cast power button".</summary>
        public static string Menu_Config_Button_Max_Cast_Power()
        {
            return I18n.GetByKey("menu.config.button.max.cast.power");
        }

        /// <summary>Get a translation equivalent to "Catch treasure button".</summary>
        public static string Menu_Config_Button_Catch_Treasure()
        {
            return I18n.GetByKey("menu.config.button.catch.treasure");
        }

        /// <summary>Get a translation equivalent to "Open menu button".</summary>
        public static string Menu_Config_Button_Open_Menu()
        {
            return I18n.GetByKey("menu.config.button.open.menu");
        }

        /// <summary>Get a translation equivalent to "Reset".</summary>
        public static string Menu_Config_Reset_Control()
        {
            return I18n.GetByKey("menu.config.reset.control");
        }

        /// <summary>Get a translation equivalent to "Press New Key...".</summary>
        public static string Menu_Config_PressNewKey()
        {
            return I18n.GetByKey("menu.config.press-new-key");
        }

        /// <summary>Get a translation equivalent to "General".</summary>
        public static string Menu_Config_Header_General()
        {
            return I18n.GetByKey("menu.config.header.general");
        }

        /// <summary>Get a translation equivalent to "Mod status position".</summary>
        public static string Menu_Config_Label_Mod_Status_Position()
        {
            return I18n.GetByKey("menu.config.label.mod.status.position");
        }

        /// <summary>Get a translation equivalent to "Enable auto pause fishing".</summary>
        public static string Menu_Config_Label_Enable_Auto_Pause_Fishing()
        {
            return I18n.GetByKey("menu.config.label.enable.auto.pause.fishing");
        }

        /// <summary>Get a translation equivalent to "Pause fishing time".</summary>
        public static string Menu_Config_Label_Pause_Fishing_Time()
        {
            return I18n.GetByKey("menu.config.label.pause.fishing.time");
        }

        /// <summary>Get a translation equivalent to "Fishing rod".</summary>
        public static string Menu_Config_Header_Fishing_Rod()
        {
            return I18n.GetByKey("menu.config.header.fishing.rod");
        }

        /// <summary>Get a translation equivalent to "Auto attach bait".</summary>
        public static string Menu_Config_Label_Auto_Attach_Bait()
        {
            return I18n.GetByKey("menu.config.label.auto.attach.bait");
        }

        /// <summary>Get a translation equivalent to "Auto attach tackle".</summary>
        public static string Menu_Config_Label_Auto_Attach_Tackle()
        {
            return I18n.GetByKey("menu.config.label.auto.attach.tackle");
        }

        /// <summary>Get a translation equivalent to "Infinite bait".</summary>
        public static string Menu_Config_Label_Infinite_Bait()
        {
            return I18n.GetByKey("menu.config.label.infinite.bait");
        }

        /// <summary>Get a translation equivalent to "Infinite tackle".</summary>
        public static string Menu_Config_Label_Infinite_Tackle()
        {
            return I18n.GetByKey("menu.config.label.infinite.tackle");
        }

        /// <summary>Get a translation equivalent to "Fishing mini-game".</summary>
        public static string Menu_Config_Header_MiniGame()
        {
            return I18n.GetByKey("menu.config.header.mini-game");
        }

        /// <summary>Get a translation equivalent to "Always Perfect".</summary>
        public static string Menu_Config_Label_Always_Perfect()
        {
            return I18n.GetByKey("menu.config.label.always.perfect");
        }

        /// <summary>Get a translation equivalent to "Always catch double fish".</summary>
        public static string Menu_Config_Label_Always_Catch_Double()
        {
            return I18n.GetByKey("menu.config.label.always.catch.double");
        }

        /// <summary>Get a translation equivalent to "Always found treasure".</summary>
        public static string Menu_Config_Label_Always_Treasure()
        {
            return I18n.GetByKey("menu.config.label.always.treasure");
        }

        /// <summary>Get a translation equivalent to "Instant fish bite".</summary>
        public static string Menu_Config_Label_Instant_Fishbite()
        {
            return I18n.GetByKey("menu.config.label.instant.fishbite");
        }

        /// <summary>Get a translation equivalent to "Instant catch fish".</summary>
        public static string Menu_Config_Label_Instant_Catch_Fish()
        {
            return I18n.GetByKey("menu.config.label.instant.catch.fish");
        }

        /// <summary>Get a translation equivalent to "Instant catch treasure".</summary>
        public static string Menu_Config_Label_Instant_Catch_Treasure()
        {
            return I18n.GetByKey("menu.config.label.instant.catch.treasure");
        }

        /// <summary>Get a translation equivalent to "Fish difficulty multiplier".</summary>
        public static string Menu_Config_Label_Fish_Difficulty_Multiplier()
        {
            return I18n.GetByKey("menu.config.label.fish.difficulty.multiplier");
        }

        /// <summary>Get a translation equivalent to "Fish difficulty additive".</summary>
        public static string Menu_Config_Label_Fish_Difficulty_Additive()
        {
            return I18n.GetByKey("menu.config.label.fish.difficulty.additive");
        }

        /// <summary>Get a translation equivalent to "Value other than this can be set in config file.".</summary>
        public static string Menu_Config_Description_Fish_Difficulty()
        {
            return I18n.GetByKey("menu.config.description.fish.difficulty");
        }

        /// <summary>Get a translation equivalent to "You can change the on-screen buttons in the config files for the Fishing Assistant and Virtual Keyboard mods.".</summary>
        public static string Menu_Config_Description_AndroidConfigNote()
        {
            return I18n.GetByKey("menu.config.description.android-config-note");
        }

        /// <summary>Get a translation equivalent to "Fishing information".</summary>
        public static string Menu_Config_Header_Fish_Info()
        {
            return I18n.GetByKey("menu.config.header.fish.info");
        }

        /// <summary>Get a translation equivalent to "Fish info position".</summary>
        public static string Menu_Config_Label_Fishinfo_Display_Position()
        {
            return I18n.GetByKey("menu.config.label.fishinfo.display.position");
        }

        /// <summary>Get a translation equivalent to "Show fish name".</summary>
        public static string Menu_Config_Label_Show_Fish_Name()
        {
            return I18n.GetByKey("menu.config.label.show.fish.name");
        }

        /// <summary>Get a translation equivalent to "Show treasure".</summary>
        public static string Menu_Config_Label_Show_Treasure()
        {
            return I18n.GetByKey("menu.config.label.show.treasure");
        }

        /// <summary>Get a translation equivalent to "Show uncaught fish".</summary>
        public static string Menu_Config_Label_Show_Uncaught_Fish()
        {
            return I18n.GetByKey("menu.config.label.show.uncaught.fish");
        }

        /// <summary>Get a translation equivalent to "Show legendary fish".</summary>
        public static string Menu_Config_Label_Show_Legendaly_Fish()
        {
            return I18n.GetByKey("menu.config.label.show.legendaly.fish");
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

