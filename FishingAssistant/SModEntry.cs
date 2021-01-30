using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

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

        private void ToggleMod(ButtonPressedEventArgs e)
        {
            if (e.Button == Config.EnableModButton)
            {
                modEnable = !modEnable;
                if (modEnable) GetPlayerData();
                Monitor.Log(string.Format("Mod enable {0}", modEnable), LogLevel.Info);
            }
        }

        private void ToggleMaxCastPower(ButtonPressedEventArgs e)
        {
            if (e.Button == Config.CastPowerButton)
            {
                maxCastPower = !maxCastPower;
                Monitor.Log(string.Format("Max cast power {0}", maxCastPower), LogLevel.Info);
            }
        }

        private void ToggleCatchTreasure(ButtonPressedEventArgs e)
        {
            if (e.Button == Config.CatchTreasureButton)
            {
                autoCatchTreasure = !autoCatchTreasure;
                Monitor.Log(string.Format("Catch treasure {0}", autoCatchTreasure), LogLevel.Info);
            }
        }

        private void ReloadConfig(ButtonPressedEventArgs e)
        {
            if (e.Button == Config.ReloadConfigButton)
            {
                Config = Helper.ReadConfig<ModConfig>();
                Monitor.Log("Config reloaded", LogLevel.Info);
            }
        }

        private void GetPlayerData()
        {
            playerStandingX = Game1.player.getStandingX();
            playerStandingY = Game1.player.getStandingY();
            playerFacingDirection = Game1.player.getDirection() != -1 ? Game1.player.getDirection() : Game1.player.FacingDirection;
        }

        private bool IsFishingMiniGameReady()
        {
            return inFishingMiniGame && bobberBar != null;
        }
    }
}