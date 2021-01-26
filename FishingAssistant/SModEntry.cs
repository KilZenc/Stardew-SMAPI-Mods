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
        private void Initialize(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();

            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
            helper.Events.Display.MenuChanged += OnMenuChanged;
            helper.Events.Input.ButtonPressed += OnButtonPressed;

            Monitor.Log("Initialized (press F5 to reload config)", LogLevel.Info);
        }

        private void ReloadConfig(ButtonPressedEventArgs e)
        {
            if (e.Button == SButton.F5)
            {
                Config = Helper.ReadConfig<ModConfig>();
                Monitor.Log("Config reloaded", LogLevel.Info);
            }
        }
    }
}
