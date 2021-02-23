using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;
using System;

namespace FishingAssistant.Menu.Option
{
    /// <summary>A checkbox UI option.</summary>
    internal class CheckboxOptionElement : BaseOptionsElement
    {
        /*********
        ** Fields
        *********/

        /// <summary>A callback to invoke when the value changes.</summary>
        private readonly Action<bool> SetValue;

        /*********
        ** Accessors
        *********/

        /// <summary>Whether the checkbox is currently checked.</summary>
        public bool IsChecked { get; private set; }

        /*********
        ** Public methods
        *********/

        /// <summary>Construct an instance.</summary>
        /// <param name="label">The checkbox label.</param>
        /// <param name="value">The initial value to set.</param>
        /// <param name="setValue">A callback to invoke when the value changes.</param>
        public CheckboxOptionElement(string label, bool value, Action<bool> setValue)
          : base(label, -1, -1, 9 * Game1.pixelZoom, 9 * Game1.pixelZoom, 0)
        {
            IsChecked = value;
            SetValue = setValue;
        }

        /// <summary>Handle the player clicking the left mouse button.</summary>
        /// <param name="x">The cursor's X pixel position.</param>
        /// <param name="y">The cursor's Y pixel position.</param>
        public override void receiveLeftClick(int x, int y)
        {
            if (greyedOut)
                return;
            Game1.soundBank.PlayCue("drumkit6");
            base.receiveLeftClick(x, y);
            IsChecked = !IsChecked;
            SetValue(IsChecked);
        }

        /// <summary>Draw the component to the screen.</summary>
        /// <param name="spriteBatch">The sprite batch being drawn.</param>
        /// <param name="slotX">The X position at which to draw, relative to the bounds.</param>
        /// <param name="slotY">The Y position at which to draw, relative to the bounds.</param>
        /// <param name="context">The menu drawing the component.</param>
        public override void draw(SpriteBatch spriteBatch, int slotX, int slotY, IClickableMenu context = null)
        {
            spriteBatch.Draw(Game1.mouseCursors, new Vector2(slotX + bounds.X, slotY + bounds.Y), IsChecked ? OptionsCheckbox.sourceRectChecked : OptionsCheckbox.sourceRectUnchecked, Color.White * (greyedOut ? 0.33f : 1f), 0.0f, Vector2.Zero, Game1.pixelZoom, SpriteEffects.None, 0.4f);
            base.draw(spriteBatch, slotX + GetOffsetX(), slotY, context);
        }
    }
}