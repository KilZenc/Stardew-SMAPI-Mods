using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System.Collections.Generic;

namespace FishingAssistant
{
    partial class ModEntry : Mod
    {
        private void Initialize(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();

            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
            helper.Events.Display.MenuChanged += OnMenuChanged;
            helper.Events.Display.RenderedActiveMenu += OnRenderMenu;
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

        private void ReloadConfig()
        {
            Config = Helper.ReadConfig<ModConfig>();
            Monitor.Log("Config reloaded", LogLevel.Info);

            switch (Config.FishDisplayPosition)
            {
                case "Top":
                    displayOrder = new List<string>() { "Top", "UpperRight", "UpperLeft", "LowerRight" };
                    break;
                case "UpperRight":
                    displayOrder = new List<string>() { "UpperRight", "UpperLeft", "LowerRight" };
                    break;
                case "UpperLeft":
                    displayOrder = new List<string>() { "UpperLeft", "UpperRight", "LowerLeft" };
                    break;
                case "Bottom":
                    displayOrder = new List<string>() { "Bottom", "LowerRight", "LowerLeft", "UpperRight" };
                    break;
                case "LowerRight":
                    displayOrder = new List<string>() { "LowerRight", "LowerLeft", "UpperRight" };
                    break;
                case "LowerLeft":
                    displayOrder = new List<string>() { "LowerLeft", "LowerRight", "UpperLeft" };
                    break;
                default:
                    displayOrder = new List<string>() { "UpperRight", "UpperLeft", "LowerLeft" };
                    this.Monitor.Log($"Invalid config value {Config.FishDisplayPosition} for FishDisplayPosition. Valid entries include Top, Bottom, UpperRight, UpperLeft, LowerRight and LowerLeft.", LogLevel.Warn);
                    break;
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