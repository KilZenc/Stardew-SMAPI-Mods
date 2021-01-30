using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using System.Collections.Generic;

namespace FishingAssistant
{
    partial class ModEntry : Mod
    {
        private bool modEnable;

        private int playerStandingX;
        private int playerStandingY;
        private int playerFacingDirection;

        private bool maxCastPower;
        private bool autoHook;
        private bool autoCatchTreasure;

        private bool inFishingMiniGame;

        private ClickableTextureComponent modEnableIcon;
        private ClickableTextureComponent maxCastIcon;
        private ClickableTextureComponent catchTreasureIcon;

        private void Initialize(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();

            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
            helper.Events.Display.MenuChanged += OnMenuChanged;
            helper.Events.Display.RenderedActiveMenu += OnRenderMenu;
            helper.Events.Input.ButtonPressed += OnButtonPressed;
            helper.Events.Display.RenderingHud += OnRenderingHud;
            helper.Events.GameLoop.TimeChanged += OnTimeChange;

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

        private string ConvertTime(int currentTime)
        {
            int time = currentTime;
            string text = time >= 12 && time < 24 ? "PM" : "AM";
            time = time > 12 && time <= 24 ? time -= 12 : time = time == 25 ? 1 : time;
            return time + " " + text;
        }

        private void DrawIcon()
        {
            if (Game1.eventUp)
                return;

            if (modEnable)
            {
                Rectangle Icon = new Rectangle(20, 428, 10, 10);
                modEnableIcon = new ClickableTextureComponent(new Rectangle(10, 10, 40, 40), Game1.mouseCursors, Icon, 2f);
                modEnableIcon.draw(Game1.spriteBatch);
            }

            if (maxCastPower)
            {
                Rectangle Icon = new Rectangle(545, 1921, 53, 19);
                maxCastIcon = new ClickableTextureComponent(new Rectangle(10, 50, 40, 40), Game1.mouseCursors, Icon, 1f);
                maxCastIcon.draw(Game1.spriteBatch);
            }

            if (autoCatchTreasure)
            {
                Rectangle Icon = new Rectangle(137, 412, 10, 11);
                catchTreasureIcon = new ClickableTextureComponent(new Rectangle(50, 10, 40, 40), Game1.mouseCursors, Icon, 2f);
                catchTreasureIcon.draw(Game1.spriteBatch);
            }
        }

    }
}