using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingAssistant
{
    partial class ModEntry : Mod
    {
        private ModConfig Config;

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            // Initialize mod
            Initialize(helper);
        }

        /// <summary> Raised after the game state is updated (≈60 times per second). </summary>
        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {

        }

        /// <summary>  Raised after a game menu is opened, closed, or replaced. </summary>
        private void OnMenuChanged(object sender, MenuChangedEventArgs e)
        {

        }

        /// <summary> Raised after the player presses a button on the keyboard, controller, or mouse. </summary>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            //Reload new config
            ReloadConfig(e);
        }
    }
}
