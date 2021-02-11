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

        /// <summary>Get a translation equivalent to "Enable".</summary>
        public static string ModStatusEnable()
        {
            return I18n.GetByKey("mod_status_enable");
        }

        /// <summary>Get a translation equivalent to "Disable".</summary>
        public static string ModStatusDisable()
        {
            return I18n.GetByKey("mod_status_disable");
        }

        /// <summary>Get a translation equivalent to "{0} Fishing Assistant".</summary>
        public static string HudMessageModToggle()
        {
            return I18n.GetByKey("hud_message_mod_toggle");
        }

        /// <summary>Get a translation equivalent to "Mod will disable after task is finished".</summary>
        public static string HudMessageRequestDisable()
        {
            return I18n.GetByKey("hud_message_request_disable");
        }

        /// <summary>Get a translation equivalent to "Pressed {0} again to force enable".</summary>
        public static string HudMessageForceEnable()
        {
            return I18n.GetByKey("hud_message_force_enable");
        }

        /// <summary>Get a translation equivalent to "Max cast power is set to {0}".</summary>
        public static string HudMessageCastPower()
        {
            return I18n.GetByKey("hud_message_cast_power");
        }

        /// <summary>Get a translation equivalent to "Catch treasure is set to  {0}".</summary>
        public static string HudMessageCatchTreasure()
        {
            return I18n.GetByKey("hud_message_catch_treasure");
        }

        /// <summary>Get a translation equivalent to "Player have low energy".</summary>
        public static string HudMessageLowStamina()
        {
            return I18n.GetByKey("hud_message_low_stamina");
        }

        /// <summary>Get a translation equivalent to "Player inventory is full".</summary>
        public static string HudMessageInventoryFull()
        {
            return I18n.GetByKey("hud_message_inventory_full");
        }

        /// <summary>Get a translation equivalent to "Auto disable mod on {0}".</summary>
        public static string HudMessageAutoDisable()
        {
            return I18n.GetByKey("hud_message_auto_disable");
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

