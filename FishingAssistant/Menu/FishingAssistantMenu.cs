using FishingAssistant.Menu.Option;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FishingAssistant.Menu
{
    /// <summary>An interactive menu for configuring and toggling cheats.</summary>
    internal class FishingAssistantMenu : IClickableMenu
    {
        /*********
		** Fields
		*********/
        private ModEntry Mod;
        private ModConfig Config;

        private readonly List<OptionsElement> Options = new List<OptionsElement>();
        private readonly List<ClickableComponent> OptionSlots = new List<ClickableComponent>();
        private ClickableTextureComponent UpArrow;
        private ClickableTextureComponent DownArrow;
        private ClickableTextureComponent Scrollbar;
        private Rectangle ScrollbarRunner;
        private ClickableComponent Title;
        private const int ItemsPerPage = 7;

        private string HoverText = "";
        private int OptionsSlotHeld = -1;
        private int CurrentItemIndex;
        private bool IsScrolling;
        private bool CanClose;

        /// <summary>Whether the mod is running on Android.</summary>
        private readonly bool IsAndroid = Constants.TargetPlatform == GamePlatform.Android;

        /// <summary>Whether the menu was opened in the current tick.</summary>
        private bool JustOpened = true;

        /*********
		** Public methods
		*********/

        /// <summary>Construct an instance.</summary>
        public FishingAssistantMenu(ModEntry Mod, ModConfig Config)
        {
            this.Mod = Mod;
            this.Config = Config;

            ResetComponents();
            SetOptions();
        }

        private void ResetComponents()
        {
            // set dimensions
            this.width = (this.IsAndroid ? 750 : 800) + IClickableMenu.borderWidth * 2;
            this.height = (this.IsAndroid ? 550 : 600) + IClickableMenu.borderWidth * 2;
            this.xPositionOnScreen = Game1.uiViewport.Width / 2 - (this.width /*- (int)(Game1.tileSize * 2.4f)*/) / 2;
            this.yPositionOnScreen = Game1.uiViewport.Height / 2 - this.height / 2;

            // show close button on Android
            if (this.IsAndroid)
                this.initializeUpperRightCloseButton();

            // add title
            this.Title = new ClickableComponent(new Rectangle(this.xPositionOnScreen + this.width / 2, this.yPositionOnScreen, Game1.tileSize * 4, Game1.tileSize), I18n.Mod_Name());

            // add scroll UI
            int scrollbarOffset = Game1.tileSize * (this.IsAndroid ? 1 : 4) / 16;
            this.UpArrow = new ClickableTextureComponent("up-arrow", new Rectangle(this.xPositionOnScreen + this.width + scrollbarOffset, this.yPositionOnScreen + Game1.tileSize, 11 * Game1.pixelZoom, 12 * Game1.pixelZoom), "", "", Game1.mouseCursors, new Rectangle(421, 459, 11, 12), Game1.pixelZoom);
            this.DownArrow = new ClickableTextureComponent("down-arrow", new Rectangle(this.xPositionOnScreen + this.width + scrollbarOffset, this.yPositionOnScreen + this.height - Game1.tileSize, 11 * Game1.pixelZoom, 12 * Game1.pixelZoom), "", "", Game1.mouseCursors, new Rectangle(421, 472, 11, 12), Game1.pixelZoom);
            this.Scrollbar = new ClickableTextureComponent("scrollbar", new Rectangle(this.UpArrow.bounds.X + Game1.pixelZoom * 3, this.UpArrow.bounds.Y + this.UpArrow.bounds.Height + Game1.pixelZoom, 6 * Game1.pixelZoom, 10 * Game1.pixelZoom), "", "", Game1.mouseCursors, new Rectangle(435, 463, 6, 10), Game1.pixelZoom);
            this.ScrollbarRunner = new Rectangle(this.Scrollbar.bounds.X, this.UpArrow.bounds.Y + this.UpArrow.bounds.Height + Game1.pixelZoom, this.Scrollbar.bounds.Width, this.height - Game1.tileSize * 2 - this.UpArrow.bounds.Height - Game1.pixelZoom * 2);
            this.SetScrollBarToCurrentIndex();

            // add option slots
            this.OptionSlots.Clear();
            for (int i = 0; i < ItemsPerPage; i++)
                this.OptionSlots.Add(new ClickableComponent(
                    new Rectangle(this.xPositionOnScreen + Game1.tileSize / 4, this.yPositionOnScreen + Game1.tileSize * 5 / 4 + Game1.pixelZoom + i * ((this.height - Game1.tileSize * 2) / ItemsPerPage),
                    this.width - Game1.tileSize / 2, (this.height - Game1.tileSize * 2) / ItemsPerPage + Game1.pixelZoom), string.Concat(i)));
        }

        private void SetOptions()
        {
            this.Options.Clear();

            #region Control

            this.Options.Add(new OptionsElement(I18n.Menu_Config_Header_Control()));

            if (this.IsAndroid)
                this.AddDescription(I18n.Menu_Config_Description_AndroidConfigNote());

            this.Options.Add(new KeyListenerOptionElement(
                label: I18n.Menu_Config_Button_Toggle_Mod(),
                value: Config.EnableModButton,
                setValue: key => Config.EnableModButton = key,
                slotWidth: OptionSlots[0].bounds.Width,
                clearToButton: SButton.F5));

            this.Options.Add(new KeyListenerOptionElement(
                label: I18n.Menu_Config_Button_Max_Cast_Power(),
                value: Config.CastPowerButton,
                setValue: key => Config.CastPowerButton = key,
                slotWidth: OptionSlots[0].bounds.Width,
                clearToButton: SButton.F6));

            this.Options.Add(new KeyListenerOptionElement(
                label: I18n.Menu_Config_Button_Catch_Treasure(),
                value: Config.CatchTreasureButton,
                setValue: key => Config.CatchTreasureButton = key,
                slotWidth: OptionSlots[0].bounds.Width,
                clearToButton: SButton.F7));

            this.Options.Add(new KeyListenerOptionElement(
                label: I18n.Menu_Config_Button_Open_Menu(),
                value: Config.OpenMenuButton,
                setValue: key => Config.OpenMenuButton = key,
                slotWidth: OptionSlots[0].bounds.Width,
                clearToButton: SButton.F8));

            this.Options.Add(new ButtonOptionElement(
                label: I18n.Menu_Config_Reset_Control(),
                toggle: this.ResetControls,
                slotWidth: OptionSlots[0].bounds.Width));

            #endregion Control

            #region General

            this.Options.Add(new OptionsElement(I18n.Menu_Config_Header_General()));
            this.Options.Add(new SliderOptionElement(
                label: I18n.Menu_Config_Label_Mod_Status_Position(),
                value: Mod.GetCurrentListValue(Config.ModStatusDisplayPosition, Mod.ModDisplayPosition),
                maxValue: Mod.ModDisplayPosition.Count - 1,
                setValue: value => Config.ModStatusDisplayPosition = Mod.ModDisplayPosition[value],
                format: value => MenuHelper.GetLocalizationPosition(Mod.ModDisplayPosition[value])));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Auto_Pause_Fishing(),
                value: Config.EnableAutoPauseFishing,
                setValue: value => Config.EnableAutoPauseFishing = value));

            this.Options.Add(new SliderOptionElement(
                label: I18n.Menu_Config_Label_Pause_Fishing_Time(),
                value: Config.PauseFishingTime,
                minValue: 6, maxValue: 25,
                setValue: value => Config.PauseFishingTime = value,
                setValueBool: value => Mod.IsForceEnable = value,
                disabled: () => !Config.EnableAutoPauseFishing,
                format: value => Game1.getTimeOfDayString(value * 100)));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Auto_Eat_Food(),
                value: Config.EnableAutoEatFood,
                setValue: value => Config.EnableAutoEatFood = value));

            this.Options.Add(new SliderOptionElement(
                label: I18n.Menu_Config_Label_Energy_To_Eat_Food(),
                value: Config.EnergyPrecentToEat / 5,
                minValue: 0, maxValue: 19,
                setValue: value => Config.EnergyPrecentToEat = value * 5,
                disabled: () => !Config.EnableAutoEatFood,
                format: value => value == 0 ? I18n.Menu_Config_Etc_WhenLowEnergy() : string.Format("{0}%", value * 5)));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Allow_Eat_Fish(),
                value: Config.AllowEatingFish,
                setValue: value => Config.AllowEatingFish = value));

            this.AddDescription(I18n.Menu_Config_Etc_Allow_Eat_Fish_Warning());

            #endregion General

            #region Fishing Rod

            this.Options.Add(new OptionsElement(I18n.Menu_Config_Header_Fishing_Rod()));
            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Auto_Attach_Bait(),
                value: Config.AutoAttachBait,
                setValue: value => Config.AutoAttachBait = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Auto_Attach_Tackle(),
                value: Config.AutoAttachTackles,
                setValue: value => Config.AutoAttachTackles = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Infinite_Bait(),
                value: Config.InfiniteBait,
                setValue: value => Config.InfiniteBait = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Infinite_Tackle(),
                value: Config.InfiniteTackle,
                setValue: value => Config.InfiniteTackle = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Enchantment_AutoHook(),
                value: Config.AddAutoHookEnchantment,
                setValue: value => Config.AddAutoHookEnchantment = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Enchantment_Efficient(),
                value: Config.AddEfficientEnchantment,
                setValue: value => Config.AddEfficientEnchantment = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Enchantment_Master(),
                value: Config.AddMasterEnchantment,
                setValue: value => Config.AddMasterEnchantment = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Enchantment_Preserving(),
                value: Config.AddPreservingEnchantment,
                setValue: value => Config.AddPreservingEnchantment = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Enchantment_WhenHeld(),
                value: Config.OnlyAddEnchantmentWhenHeld,
                setValue: value => Config.OnlyAddEnchantmentWhenHeld = value));

            #endregion Fishing Rod

            #region Mini-game

            this.Options.Add(new OptionsElement(I18n.Menu_Config_Header_MiniGame()));
            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Always_Perfect(),
                value: Config.AlwaysPerfect,
                setValue: value => Config.AlwaysPerfect = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Always_Catch_Double(),
                value: Config.AlwaysCatchDoubleFish,
                setValue: value => Config.AlwaysCatchDoubleFish = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Always_Treasure(),
                value: Config.AlwaysFindTreasure,
                setValue: value => Config.AlwaysFindTreasure = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Instant_Fishbite(),
                value: Config.InstantFishBite,
                setValue: value => Config.InstantFishBite = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Instant_Catch_Fish(),
                value: Config.InstantCatchFish,
                setValue: value => Config.InstantCatchFish = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Instant_Catch_Treasure(),
                value: Config.InstantCatchTreasure,
                setValue: value => Config.InstantCatchTreasure = value));

            this.Options.Add(new SliderOptionElement(
                label: I18n.Menu_Config_Label_Fish_Difficulty_Multiplier(),
                value: (int)Config.FishDifficultyMultiplier * 10,
                minValue: 0,
                maxValue: 20,
                setValue: value => Config.FishDifficultyMultiplier = (float)value / 10,
                format: value => ((float)value / 10).ToString()));

            AddDescription(I18n.Menu_Config_Description_Fish_Difficulty());

            this.Options.Add(new SliderOptionElement(
                label: I18n.Menu_Config_Label_Fish_Difficulty_Additive(),
                value: (int)Config.FishDifficultyAdditive * 10,
                minValue: -10,
                maxValue: 10,
                setValue: value => Config.FishDifficultyAdditive = (float)value,
                format: value => ((float)value).ToString()));

            AddDescription(I18n.Menu_Config_Description_Fish_Difficulty());

            #endregion Mini-game

            #region Fish info

            this.Options.Add(new OptionsElement(I18n.Menu_Config_Header_Fish_Info()));
            this.Options.Add(new SliderOptionElement(
                label: I18n.Menu_Config_Label_Fishinfo_Display_Position(),
                value: Mod.GetCurrentListValue(Config.FishInfoDisplayPosition, Mod.FishInfoDisplayPosition),
                maxValue: Mod.FishInfoDisplayPosition.Count - 1,
                setValue: value => Config.FishInfoDisplayPosition = Mod.FishInfoDisplayPosition[value],
                format: value => MenuHelper.GetLocalizationPosition(Mod.FishInfoDisplayPosition[value])));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Show_Fish_Name(),
                value: Config.ShowFishName,
                setValue: value => Config.ShowFishName = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Show_Treasure(),
                value: Config.ShowTreasure,
                setValue: value => Config.ShowTreasure = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Show_Uncaught_Fish(),
                value: Config.ShowUncaughtFishSpecies,
                setValue: value => Config.ShowUncaughtFishSpecies = value));

            this.Options.Add(new CheckboxOptionElement(
                label: I18n.Menu_Config_Label_Show_Legendaly_Fish(),
                value: Config.AlwaysShowLegendaryFish,
                setValue: value => Config.AlwaysShowLegendaryFish = value));

            #endregion Fish info
        }

        /// <summary>Whether controller-style menus should be disabled for this menu.</summary>
        public override bool overrideSnappyMenuCursorMovementBan()
        {
            return true;
        }

        /// <summary>Handle the game window being resized.</summary>
        /// <param name="oldBounds">The previous window bounds.</param>
        /// <param name="newBounds">The new window bounds.</param>
        public override void gameWindowSizeChanged(Rectangle oldBounds, Rectangle newBounds)
        {
            this.ResetComponents();
        }

        private void SetScrollBarToCurrentIndex()
        {
            if (!this.Options.Any())
                return;
            this.Scrollbar.bounds.Y = this.ScrollbarRunner.Height / Math.Max(1, this.Options.Count - ItemsPerPage + 1) * this.CurrentItemIndex + this.UpArrow.bounds.Bottom + Game1.pixelZoom;
            if (this.CurrentItemIndex != this.Options.Count - ItemsPerPage)
                return;
            this.Scrollbar.bounds.Y = this.DownArrow.bounds.Y - this.Scrollbar.bounds.Height - Game1.pixelZoom;
        }

        /// <summary>Handle the player holding the left mouse button.</summary>
        /// <param name="x">The cursor's X pixel position.</param>
        /// <param name="y">The cursor's Y pixel position.</param>
        public override void leftClickHeld(int x, int y)
        {
            if (GameMenu.forcePreventClose)
                return;
            base.leftClickHeld(x, y);
            if (this.IsScrolling)
            {
                int num = this.Scrollbar.bounds.Y;
                this.Scrollbar.bounds.Y = Math.Min(this.yPositionOnScreen + this.height - Game1.tileSize - Game1.pixelZoom * 3 - this.Scrollbar.bounds.Height, Math.Max(y, this.yPositionOnScreen + this.UpArrow.bounds.Height + Game1.pixelZoom * 5));
                this.CurrentItemIndex = Math.Min(this.Options.Count - ItemsPerPage, Math.Max(0, (int)Math.Round((this.Options.Count - ItemsPerPage) * (double)((y - this.ScrollbarRunner.Y) / (float)this.ScrollbarRunner.Height))));
                this.SetScrollBarToCurrentIndex();
                if (num == this.Scrollbar.bounds.Y)
                    return;
                Game1.soundBank.PlayCue("shiny4");
            }
            else
            {
                if (this.OptionsSlotHeld == -1 || this.OptionsSlotHeld + this.CurrentItemIndex >= this.Options.Count)
                    return;
                this.Options[this.CurrentItemIndex + this.OptionsSlotHeld].leftClickHeld(x - this.OptionSlots[this.OptionsSlotHeld].bounds.X, y - this.OptionSlots[this.OptionsSlotHeld].bounds.Y);
            }
        }

        /// <summary>Handle the player pressing a keyboard button.</summary>
        /// <param name="key">The key that was pressed.</param>
        public override void receiveKeyPress(Keys key)
        {
            SButton openMenuKey = Config.OpenMenuButton;
            bool isExitKey = Game1.options.menuButton.Contains(new InputButton(key)) || (openMenuKey.TryGetKeyboard(out Keys exitKey) && key == exitKey);
            if (isExitKey && this.readyToClose() && this.CanClose && !GameMenu.forcePreventClose)
            {
                Game1.exitActiveMenu();
                Game1.soundBank.PlayCue("bigDeSelect");
                return;
            }

            this.CanClose = true;

            if (this.OptionsSlotHeld == -1 || this.OptionsSlotHeld + this.CurrentItemIndex >= this.Options.Count)
                return;
            this.Options[this.CurrentItemIndex + this.OptionsSlotHeld].receiveKeyPress(key);
        }

        /// <summary>Handle the player scrolling the mouse wheel.</summary>
        /// <param name="direction">The scroll direction.</param>
        public override void receiveScrollWheelAction(int direction)
        {
            if (GameMenu.forcePreventClose)
                return;
            base.receiveScrollWheelAction(direction);
            if (direction > 0 && this.CurrentItemIndex > 0)
                this.UpArrowPressed();
            else
            {
                if (direction >= 0 || this.CurrentItemIndex >= Math.Max(0, this.Options.Count - ItemsPerPage))
                    return;
                this.DownArrowPressed();
            }
        }

        /// <summary>Handle the player releasing the left mouse button.</summary>
        /// <param name="x">The cursor's X pixel position.</param>
        /// <param name="y">The cursor's Y pixel position.</param>
        public override void releaseLeftClick(int x, int y)
        {
            if (GameMenu.forcePreventClose)
                return;
            base.releaseLeftClick(x, y);
            if (this.OptionsSlotHeld != -1 && this.OptionsSlotHeld + this.CurrentItemIndex < this.Options.Count)
                this.Options[this.CurrentItemIndex + this.OptionsSlotHeld].leftClickReleased(x - this.OptionSlots[this.OptionsSlotHeld].bounds.X, y - this.OptionSlots[this.OptionsSlotHeld].bounds.Y);
            this.OptionsSlotHeld = -1;
            this.IsScrolling = false;
        }

        private void DownArrowPressed()
        {
            this.DownArrow.scale = this.DownArrow.baseScale;
            ++this.CurrentItemIndex;
            this.SetScrollBarToCurrentIndex();
        }

        private void UpArrowPressed()
        {
            this.UpArrow.scale = this.UpArrow.baseScale;
            --this.CurrentItemIndex;
            this.SetScrollBarToCurrentIndex();
        }

        /// <summary>Handle the player clicking the left mouse button.</summary>
        /// <param name="x">The cursor's X pixel position.</param>
        /// <param name="y">The cursor's Y pixel position.</param>
        /// <param name="playSound">Whether to play a sound if needed.</param>
        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {
            if (GameMenu.forcePreventClose)
                return;
            base.receiveLeftClick(x, y, playSound);

            if (this.DownArrow.containsPoint(x, y) && this.CurrentItemIndex < Math.Max(0, this.Options.Count - ItemsPerPage))
            {
                this.DownArrowPressed();
                Game1.soundBank.PlayCue("shwip");
            }
            else if (this.UpArrow.containsPoint(x, y) && this.CurrentItemIndex > 0)
            {
                this.UpArrowPressed();
                Game1.soundBank.PlayCue("shwip");
            }
            else if (this.Scrollbar.containsPoint(x, y))
                this.IsScrolling = true;
            else if (!this.DownArrow.containsPoint(x, y) && x > this.xPositionOnScreen + this.width && (x < this.xPositionOnScreen + this.width + Game1.tileSize * 2 && y > this.yPositionOnScreen) && y < this.yPositionOnScreen + this.height)
            {
                this.IsScrolling = true;
                this.leftClickHeld(x, y);
                this.releaseLeftClick(x, y);
            }
            this.CurrentItemIndex = Math.Max(0, Math.Min(this.Options.Count - ItemsPerPage, this.CurrentItemIndex));
            for (int index = 0; index < this.OptionSlots.Count; ++index)
            {
                if (this.OptionSlots[index].bounds.Contains(x, y) && this.CurrentItemIndex + index < this.Options.Count && this.Options[this.CurrentItemIndex + index].bounds.Contains(x - this.OptionSlots[index].bounds.X, y - this.OptionSlots[index].bounds.Y - 5))
                {
                    this.Options[this.CurrentItemIndex + index].receiveLeftClick(x - this.OptionSlots[index].bounds.X, y - this.OptionSlots[index].bounds.Y + 5);
                    this.OptionsSlotHeld = index;
                    break;
                }
            }
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (!Game1.options.showMenuBackground)
                spriteBatch.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.4f);

            Game1.drawDialogueBox(this.xPositionOnScreen, this.yPositionOnScreen, this.width, this.height, false, true);
            MenuHelper.DrawTab(this.Title.bounds.X, this.Title.bounds.Y, Game1.dialogueFont, this.Title.name, 1);
            for (int index = 0; index < this.OptionSlots.Count; ++index)
            {
                if (this.CurrentItemIndex >= 0 && this.CurrentItemIndex + index < this.Options.Count)
                    this.Options[this.CurrentItemIndex + index].draw(spriteBatch, this.OptionSlots[index].bounds.X, this.OptionSlots[index].bounds.Y + 5);
            }
            if (!GameMenu.forcePreventClose)
            {
                this.UpArrow.draw(spriteBatch);
                this.DownArrow.draw(spriteBatch);
                if (this.Options.Count > ItemsPerPage)
                {
                    drawTextureBox(spriteBatch, Game1.mouseCursors, new Rectangle(403, 383, 6, 6), this.ScrollbarRunner.X, this.ScrollbarRunner.Y, this.ScrollbarRunner.Width, this.ScrollbarRunner.Height, Color.White, 4f, drawShadow: false);
                    this.Scrollbar.draw(spriteBatch);
                }
            }
            if (this.HoverText != "")
                IClickableMenu.drawHoverText(spriteBatch, this.HoverText, Game1.smallFont);

            if (!Game1.options.hardwareCursor && !this.IsAndroid)
                spriteBatch.Draw(Game1.mouseCursors, new Vector2(Game1.getOldMouseX(), Game1.getOldMouseY()), Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, Game1.options.gamepadControls ? 44 : 0, 16, 16), Color.White, 0f, Vector2.Zero, Game1.pixelZoom + Game1.dialogueButtonScale / 150f, SpriteEffects.None, 1f);

            // reinitialize the UI to fix Android pinch-zoom scaling issues
            if (this.JustOpened)
            {
                this.JustOpened = false;
                if (this.IsAndroid) this.ResetComponents();
            }
        }

        /// <summary>Add descriptive text that may extend onto multiple lines if it's too long.</summary>
        /// <param name="text">The text to render.</param>
        private void AddDescription(string text)
        {
            // get text lines
            int maxWidth = this.width - Game1.tileSize - 10;

            foreach (string originalLine in text.Replace("\r\n", "\n").Split('\n'))
            {
                string line = "";
                foreach (string word in originalLine.Split(' '))
                {
                    if (line == "")
                        line = word;
                    else if (Game1.smallFont.MeasureString(line + " " + word).X <= maxWidth)
                        line += " " + word;
                    else
                    {
                        this.Options.Add(new DescriptionElement(line));
                        line = word;
                    }
                }
                if (line != "")
                    this.Options.Add(new DescriptionElement(line));
            }
        }

        /// <summary>Reset all controls to their default value.</summary>
        private void ResetControls()
        {
            var config = this.Config;

            config.EnableModButton = ModConfig.Defaults.EnableModButton;
            config.CastPowerButton = ModConfig.Defaults.CastPowerButton;
            config.CatchTreasureButton = ModConfig.Defaults.CatchTreasureButton;
            config.OpenMenuButton = ModConfig.Defaults.OpenMenuButton;

            Game1.soundBank.PlayCue("bigDeSelect");

            this.SetOptions();
        }
    }
}