using FishingAssistant.Menu;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;

namespace FishingAssistant
{
    partial class ModEntry : Mod
    {
        private bool modEnable;
        private bool hasDisableRequest;
        private bool hasEnableRequest;
        private bool isForceEnable;

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

        private int boxSize = 96;
        private int iconSize = 40;
        private int screenMargin = 8;
        private int spacing = 2;
        private int toolBarWidth = 0;

        public bool IsForceEnable { get => isForceEnable; set => isForceEnable = value; }

        public List<string> ModDisplayPosition = new List<string> { "Left", "Right" };
        public List<string> FishInfoDisplayPosition = new List<string> { "Top", "UpperRight", "UpperLeft", "Bottom", "LowerRight", "LowerLeft" };


        private void Initialize(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();
            I18n.Init(helper.Translation);

            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
            helper.Events.Display.MenuChanged += OnMenuChanged;
            helper.Events.Display.RenderedActiveMenu += OnRenderMenu;
            helper.Events.Input.ButtonPressed += OnButtonPressed;
            helper.Events.Display.RenderingHud += OnRenderingHud;
            helper.Events.GameLoop.TimeChanged += OnTimeChanged;
            helper.Events.GameLoop.DayStarted += OnDayStarted;

            Monitor.Log("Initialized (press F8 to reload config)", LogLevel.Info);
        }

        private void OnEnableModButtonPressed(ButtonPressedEventArgs e)
        {
            if (e.Button == Config.EnableModButton)
                ToggleMod();
        }

        private void ToggleMod()
        {
            if (modEnable)
            {
                isForceEnable = false;
                if (modState == ModState.Fishing || modState == ModState.Loot)
                {
                    hasDisableRequest = true;
                    Game1.playSound("coin");
                    AddHUDMessage(2, I18n.Hud_Message_Request_Disable());
                    return;
                }
            }
            else
            {
                if (Game1.timeOfDay >= Config.PauseFishingTime * 100 && Config.EnableAutoPauseFishing)
                {
                    if (hasEnableRequest)
                    {
                        hasEnableRequest = false;
                        isForceEnable = true;
                    }
                    else
                    {
                        hasEnableRequest = true;
                        Game1.playSound("coin");
                        AddHUDMessage(2, I18n.Hud_Message_Force_Enable(), Config.EnableModButton.ToString());
                        return;
                    }
                }
            }

            modEnable = isForceEnable || !modEnable;

            if (Game1.isFestival())
            {
                string status = modEnable ? I18n.Mod_Status_Enable() : I18n.Mod_Status_Disable();
                AddHUDMessage(2, I18n.Hud_Message_Mod_Toggle(), status);
            }

            if (modEnable) GetPlayerData();
            else modState = ModState.Disable;
        }

        private void OnCastPowerButtonPressed(ButtonPressedEventArgs e)
        {
            if (e.Button == Config.CastPowerButton)
            {
                maxCastPower = !maxCastPower;
                if (Game1.isFestival())
                {
                    string status = maxCastPower ? I18n.Mod_Status_Enable() : I18n.Mod_Status_Disable();
                    AddHUDMessage(2, I18n.Hud_Message_Cast_Power(), status);
                }
            }
        }

        private void OnCatchTreasureButtonPressed(ButtonPressedEventArgs e)
        {
            if (e.Button == Config.CatchTreasureButton)
            {
                autoCatchTreasure = !autoCatchTreasure;
                if (Game1.isFestival())
                {
                    string status = autoCatchTreasure ? I18n.Mod_Status_Enable() : I18n.Mod_Status_Disable();
                    AddHUDMessage(2, I18n.Hud_Message_Catch_Treasure(), status);
                }
            }
        }

        private void OnToggleMenuButtonPressed(ButtonPressedEventArgs e)
        {
            if (e.Button == Config.OpenMenuButton && !(Game1.activeClickableMenu is FishingAssistantMenu))
            {
                Game1.activeClickableMenu = new FishingAssistantMenu(this, Config);
            }
        }

        private void ReloadConfig()
        {
            Config = Helper.ReadConfig<ModConfig>();
            AddHUDMessage(2, I18n.Hud_Message_Config_Saved());

            switch (Config.FishInfoDisplayPosition)
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
                    this.Monitor.Log($"Invalid config value {Config.FishInfoDisplayPosition} for FishDisplayPosition. Valid entries include Top, Bottom, UpperRight, UpperLeft, LowerRight and LowerLeft.", LogLevel.Warn);
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

        private void AddHUDMessage(int whatType, string key, params object[] args)
        {
            if (!Context.IsWorldReady) return;
            Game1.addHUDMessage(new HUDMessage(string.Format(key, args), whatType));
        }

        private void DrawModStatus()
        {
            if (Game1.eventUp) return;

            CalculateModPosition(out float toolBarTransparency, out int screenXPos, out int screenYPos, out int boxCenterY, out int boxCenterX);

            bool drawBox = DrawModStatusBox(toolBarTransparency, screenXPos, screenYPos, ref boxCenterX);

            DrawStatusIcon(toolBarTransparency, boxCenterY, boxCenterX, drawBox);
        }

        private void DrawStatusIcon(float toolBarTransparency, int boxCenterY, int boxCenterX, bool drawBox)
        {
            DrawIcon(modEnable, new Rectangle(20, 428, 10, 10),
                boxCenterX - (boxSize / 4) - spacing,
                boxCenterY - (boxSize / 4) - spacing,
                modEnableIcon, 2f);

            DrawIcon(maxCastPower, new Rectangle(545, 1921, 53, 19),
                boxCenterX - (boxSize / 4),
                boxCenterY + (boxSize / 4) - (iconSize / 2) + spacing,
                maxCastIcon, 1f);

            DrawIcon(autoCatchTreasure, new Rectangle(137, 412, 10, 11),
                boxCenterX + (boxSize / 4) - (iconSize / 2) + spacing,
                boxCenterY - (boxSize / 4) - spacing,
                catchTreasureIcon, 2f);

            void DrawIcon(bool value, Rectangle source, int x, int y, ClickableTextureComponent icon, float scale)
            {
                float iconTransparency = value ? 1 : drawBox ? 0.2f : 0;
                icon = new ClickableTextureComponent(new Rectangle(x, y, iconSize, iconSize), Game1.mouseCursors, source, scale);
                icon.draw(Game1.spriteBatch, Color.White * toolBarTransparency * iconTransparency, 0);
            }
        }

        private bool DrawModStatusBox(float toolBarTransparency, int screenXPos, int screenYPos, ref int boxCenterX)
        {
            bool drawBox = false;

            if (modEnable || maxCastPower || autoCatchTreasure)
            {
                if (Game1.activeClickableMenu != null)
                {
                    IClickableMenu.drawTextureBox(Game1.spriteBatch, Game1.menuTexture, new Rectangle(0, 256, 60, 60), screenXPos - boxSize / 2, screenYPos, boxSize, boxSize, Color.White * toolBarTransparency, 1, drawShadow: false);
                    boxCenterX = screenXPos;
                }
                else
                {
                    if (Config.ModStatusDisplayPosition == "Left")
                    {
                        IClickableMenu.drawTextureBox(Game1.spriteBatch, Game1.menuTexture, new Rectangle(0, 256, 60, 60), screenXPos - toolBarWidth - boxSize, screenYPos, boxSize, boxSize, Color.White * toolBarTransparency, drawShadow: false);
                        boxCenterX = screenXPos - toolBarWidth - boxSize / 2;
                    }
                    else if (Config.ModStatusDisplayPosition == "Right")
                    {
                        IClickableMenu.drawTextureBox(Game1.spriteBatch, Game1.menuTexture, new Rectangle(0, 256, 60, 60), screenXPos + toolBarWidth, screenYPos, boxSize, boxSize, Color.White * toolBarTransparency, 1, drawShadow: false);
                        boxCenterX = screenXPos + toolBarWidth + boxSize / 2;
                    }
                }
                drawBox = true;
            }

            return drawBox;
        }

        private void CalculateModPosition(out float toolBarTransparency, out int screenXPos, out int screenYPos, out int boxCenterY, out int boxCenterX)
        {
            bool alignTop = false;
            Point playerGlobalPos = Game1.player.GetBoundingBox().Center;
            Vector2 playerLocalVec = Game1.GlobalToLocal(globalPosition: new Vector2(playerGlobalPos.X, playerGlobalPos.Y), viewport: Game1.viewport);

            toolBarTransparency = 1;
            for (int i = 0; i < Game1.onScreenMenus.Count; i++)
            {
                if (Game1.onScreenMenus[i] is Toolbar toolBar)
                {
                    toolBarTransparency = Helper.Reflection.GetField<float>(toolBar, "transparency", true).GetValue();
                    toolBarWidth = toolBar.width / 2;
                    break;
                }
            }

            if (!Game1.options.pinToolbarToggle)
                alignTop = playerLocalVec.Y > (float)(Game1.viewport.Height / 2 + 64);

            var viewport = Game1.graphics.GraphicsDevice.Viewport;
            screenXPos = (int)viewport.Width / 2;
            if (alignTop)
                screenYPos = screenMargin;
            else
                screenYPos = (int)viewport.Height - screenMargin - boxSize;

            boxCenterY = screenYPos + boxSize / 2;
            boxCenterX = 0;
        }

        public int GetCurrentListValue(string currentValue, List<string> list)
        {
            return list.FindIndex(value => currentValue == value);
        }
    }
}