using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;

namespace FishingAssistant.Menu.Option
{
    internal class PagingOptionElement : BaseOptionsElement
    {
        public static bool snapZoomPlus;
        public static bool snapZoomMinus;
        public static Rectangle minusButtonSource = new Rectangle(177, 345, 7, 8);
        public static Rectangle plusButtonSource = new Rectangle(184, 345, 7, 8);
        private Rectangle minusButton;
        private Rectangle plusButton;
        public List<string> DisplayOptions;
        public List<string> Options;
        private int txtSize;
        public int selected;
        private Action<int> SetValue;

        public PagingOptionElement(string label, List<string> options, Action<int> setValue, int x = -1, int y = -1)
            : base(label, x, y, 28, 28)
        {
            this.label = label;
            this.Options = options;
            DisplayOptions = new List<string>();
            this.SetValue = setValue;

            if (x == -1) x = 32;
            if (y == -1) y = 16;

            txtSize = (int)Game1.dialogueFont.MeasureString(options[0]).X + 56;
            foreach (var displayOption in options)
            {
                txtSize = Math.Max((int)Game1.dialogueFont.MeasureString(displayOption).X + 56, txtSize);
                DisplayOptions.Add(displayOption);
            }

            bounds = new Rectangle(x, y, (int)(1.5 * txtSize), 32);
            minusButton = new Rectangle(x, 16, 28, 32);
            plusButton = new Rectangle(bounds.Right - 96, 16, 28, 32);
        }

        public override void receiveLeftClick(int x, int y)
        {
            if (!greyedOut && Options.Count > 0)
            {
                var num = selected;
                if (minusButton.Contains(x, y) && selected != 0)
                {
                    selected--;
                    snapZoomMinus = true;
                    Game1.playSound("drumkit6");
                }
                else if (plusButton.Contains(x, y) && selected != Options.Count - 1)
                {
                    selected++;
                    snapZoomPlus = true;
                    Game1.playSound("drumkit6");
                }

                if (selected < 0)
                    selected = 0;
                else if (selected >= Options.Count) selected = Options.Count - 1;
            }

            SetValue(selected);
        }

        public override void receiveKeyPress(Keys key)
        {
            base.receiveKeyPress(key);
            if (Game1.options.snappyMenus && Game1.options.gamepadControls)
            {
                if (Game1.options.doesInputListContain(Game1.options.moveRightButton, key))
                    receiveLeftClick(plusButton.Center.X, plusButton.Center.Y);
                else if (Game1.options.doesInputListContain(Game1.options.moveLeftButton, key))
                    receiveLeftClick(minusButton.Center.X, minusButton.Center.Y);
            }
        }

        public override void draw(SpriteBatch b, int slotX, int slotY, IClickableMenu context = null)
        {
            b.Draw(Game1.mouseCursors, new Vector2(slotX + minusButton.X, slotY + minusButton.Y), minusButtonSource,
                Color.White * (greyedOut ? 0.33f : 1f) * (selected == 0 ? 0.5f : 1f), 0f, Vector2.Zero, 4f, SpriteEffects.None,
                0.4f);
            b.DrawString(Game1.dialogueFont,
                selected < DisplayOptions.Count && selected != -1 ? DisplayOptions[selected] : "",
                new Vector2((int)(txtSize / 2) + slotX, slotY + minusButton.Y), Game1.textColor * (greyedOut ? 0.33f : 1f));
            b.Draw(Game1.mouseCursors, new Vector2(slotX + plusButton.X, slotY + plusButton.Y), plusButtonSource,
                Color.White * (greyedOut ? 0.33f : 1f) * (selected == DisplayOptions.Count - 1 ? 0.5f : 1f), 0f, Vector2.Zero,
                4f, SpriteEffects.None, 0.4f);
            if (!Game1.options.snappyMenus && Game1.options.gamepadControls)
            {
                if (snapZoomMinus)
                {
                    Game1.setMousePosition(slotX + minusButton.Center.X, slotY + minusButton.Center.Y);
                    snapZoomMinus = false;
                }
                else if (snapZoomPlus)
                {
                    Game1.setMousePosition(slotX + plusButton.Center.X, slotY + plusButton.Center.Y);
                    snapZoomPlus = false;
                }
            }

            base.draw(b, slotX, slotY, context);
        }
    }
}