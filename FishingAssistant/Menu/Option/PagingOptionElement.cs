using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;

namespace FishingAssistant.Menu.Option
{
    //Still not available to use
    internal class PagingOptionElement : BaseOptionsElement
    {
        private readonly List<string> options = new List<string>();

        private readonly List<string> displayOptions = new List<string>();

        private readonly Action<string> SetValue;

        private int selected;

        private static bool snapNext;

        private static bool snapPrevious;

        private Rectangle previousButton;

        private Rectangle nextButton;

        private static Rectangle previousButtonSource = new Rectangle(352, 495, 12, 11);

        private static Rectangle nextButtonSource = new Rectangle(365, 495, 12, 11);

        private Rectangle displayTxt;

        public PagingOptionElement(string label, List<string> options, Action<string> setValue, int x = -1, int y = -1)
            : base(label, x, y, 7 * Game1.pixelZoom, 7 * Game1.pixelZoom)
        {
            base.label = label;
            this.options = options;
            this.displayOptions = options;
            this.SetValue = setValue;

            if (x == -1) { x = 32; }
            if (y == -1) { y = 16; }

            int margin = 14;
            int width = 48;
            int hight = 44;

            int txtSize = (int)Game1.dialogueFont.MeasureString(options[0]).X;
            foreach (string displayOption in displayOptions)
            {
                txtSize = Math.Max((int)Game1.dialogueFont.MeasureString(displayOption).X, txtSize);
            }

            base.bounds = new Rectangle(x, y, txtSize + (width * 2) + (margin * 2), hight);
            this.previousButton = new Rectangle(x, y, width, hight);
            this.displayTxt = new Rectangle(x + previousButton.Width + margin, y, txtSize, hight);
            this.nextButton = new Rectangle(x + previousButton.Width + (margin * 2) + txtSize, y, width, hight);
        }

        public override void receiveLeftClick(int x, int y)
        {
            if (!base.greyedOut && this.options.Count > 0)
            {
                if (this.previousButton.Contains(x, y) && this.selected != 0)
                {
                    this.selected--;
                    snapPrevious = true;
                    Game1.playSound("drumkit6");
                }
                else if (this.nextButton.Contains(x, y) && this.selected != this.options.Count - 1)
                {
                    this.selected++;
                    snapNext = true;
                    Game1.playSound("drumkit6");
                }

                if (this.selected < 0)
                    this.selected = 0;
                else if (this.selected >= this.options.Count)
                    this.selected = this.options.Count - 1;

                SetValue(options[selected]);
            }
        }

        public override void receiveKeyPress(Keys key)
        {
            base.receiveKeyPress(key);
            if (Game1.options.snappyMenus && Game1.options.gamepadControls)
            {
                if (Game1.options.doesInputListContain(Game1.options.moveRightButton, key))
                {
                    this.receiveLeftClick(this.nextButton.Center.X, this.nextButton.Center.Y);
                }
                else if (Game1.options.doesInputListContain(Game1.options.moveLeftButton, key))
                {
                    this.receiveLeftClick(this.previousButton.Center.X, this.previousButton.Center.Y);
                }
            }
        }

        public override void draw(SpriteBatch b, int slotX, int slotY, IClickableMenu context = null)
        {
            b.Draw(Game1.mouseCursors, new Vector2(slotX + this.previousButton.X, slotY + this.previousButton.Y), previousButtonSource, Color.White * (base.greyedOut ? 0.33f : 1f) * ((this.selected == 0) ? 0.5f : 1f), 0f, Vector2.Zero, 4f, SpriteEffects.None, 0.4f);
            b.DrawString(Game1.dialogueFont, (this.selected < this.displayOptions.Count && this.selected != -1) ? this.displayOptions[this.selected] : "", new Vector2(slotX + this.displayTxt.X, slotY + this.displayTxt.Y), Game1.textColor);
            b.Draw(Game1.mouseCursors, new Vector2(slotX + this.nextButton.X, slotY + this.nextButton.Y), nextButtonSource, Color.White * (base.greyedOut ? 0.33f : 1f) * ((this.selected == this.displayOptions.Count - 1) ? 0.5f : 1f), 0f, Vector2.Zero, 4f, SpriteEffects.None, 0.4f);

            if (!Game1.options.snappyMenus && Game1.options.gamepadControls)
            {
                if (snapPrevious)
                {
                    Game1.setMousePosition(slotX + this.previousButton.Center.X, slotY + this.previousButton.Center.Y);
                    snapPrevious = false;
                }
                else if (snapNext)
                {
                    Game1.setMousePosition(slotX + this.nextButton.Center.X, slotY + this.nextButton.Center.Y);
                    snapNext = false;
                }
            }
            Utility.drawTextWithShadow(b, label, Game1.dialogueFont, new Vector2(slotX + nextButton.X + nextButton.Width + 4 + GetOffsetX(), slotY + this.bounds.Y), Game1.textColor);
        }
    }
}