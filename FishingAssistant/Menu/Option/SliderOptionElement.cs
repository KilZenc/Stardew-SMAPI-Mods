﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;
using System;

namespace FishingAssistant.Menu.Option
{
    /// <summary>A UI slider for selecting from a range of values.</summary>
    internal class SliderOptionElement : BaseOptionsElement
    {
        /*********
        ** Fields
        *********/

        /// <summary>The field label.</summary>
        private readonly string Label;

        /// <summary>The callback to invoke when the value changes.</summary>
        private readonly Action<int> SetValue;

        /// <summary>A callback to invoke when the value changes.</summary>
        private readonly Action<bool> SetValueBool;

        /// <summary>The minimum value that can be selected using the field.</summary>
        private readonly int MinValue;

        /// <summary>The maximum value that can be selected using the field.</summary>
        private readonly int MaxValue;

        /// <summary>The current value.</summary>
        private int Value;

        /// <summary>The value's position in the range of values between <see cref="MinValue"/> and <see cref="MaxValue"/>.</summary>
        private float ValuePosition;

        /// <summary>Whether the slider should be disabled.</summary>
        private readonly Func<bool> IsDisabled;

        /// <summary>Format the display label.</summary>
        private readonly Func<int, string> Format;

        /// <summary>The pixel width of the slider area.</summary>
        private int PixelWidth => this.bounds.Width - 10 * Game1.pixelZoom;

        /*********
        ** Public methods
        *********/

        /// <summary>Construct an instance.</summary>
        /// <param name="label">The field label.</param>
        /// <param name="value">The initial value.</param>
        /// <param name="maxValue">The maximum value that can be selected using the field.</param>
        /// <param name="setValue">The callback to invoke when the value changes.</param>
        /// <param name="minValue">The minimum value that can be selected using the field.</param>
        /// <param name="disabled">Whether the slider should be disabled.</param>
        /// <param name="format">Format the display label.</param>
        /// <param name="width">The field width.</param>
        public SliderOptionElement(string label, int value, int maxValue, Action<int> setValue, Action<bool> setValueBool = null, int minValue = 0, Func<bool> disabled = null, Func<int, string> format = null, int width = 48)
            : base(label, -1, -1, width * Game1.pixelZoom, 6 * Game1.pixelZoom, 0)
        {
            this.Label = label;
            this.Value = value;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.SetValue = setValue;
            this.SetValueBool = setValueBool;
            this.IsDisabled = disabled ?? (() => false);
            this.Format = format ?? (v => v.ToString());

            this.ValuePosition = this.GetRangePosition();
        }

        /// <summary>Handle the player holding the left mouse button.</summary>
        /// <param name="x">The cursor's X pixel position.</param>
        /// <param name="y">The cursor's Y pixel position.</param>
        public override void leftClickHeld(int x, int y)
        {
            if (this.greyedOut)
                return;

            base.leftClickHeld(x, y);

            this.ValuePosition = GetRangePosition(x, this.bounds.X, this.bounds.X + this.PixelWidth);
            this.Value = GetValueAtPosition(this.ValuePosition, this.MinValue, this.MaxValue);
        }

        /// <summary>Handle the player clicking the left mouse button.</summary>
        /// <param name="x">The cursor's X pixel position.</param>
        /// <param name="y">The cursor's Y pixel position.</param>
        public override void receiveLeftClick(int x, int y)
        {
            if (this.greyedOut)
                return;
            base.receiveLeftClick(x, y);
            this.leftClickHeld(x, y);
        }

        /// <summary>Handle the player releasing the left mouse button.</summary>
        /// <param name="x">The cursor's X pixel position.</param>
        /// <param name="y">The cursor's Y pixel position.</param>
        public override void leftClickReleased(int x, int y)
        {
            this.ValuePosition = this.GetRangePosition(); // snap to value position
            this.SetValue(this.Value);
            if (SetValueBool != null)
                this.SetValueBool(false);
        }

        /// <summary>Draw the component to the screen.</summary>
        /// <param name="spriteBatch">The sprite batch being drawn.</param>
        /// <param name="slotX">The X position at which to draw, relative to the bounds.</param>
        /// <param name="slotY">The Y position at which to draw, relative to the bounds.</param>
        /// <param name="context">The menu drawing the component.</param>
        public override void draw(SpriteBatch spriteBatch, int slotX, int slotY, IClickableMenu context = null)
        {
            this.label = $"{this.Label}: {this.Format(this.Value)}";
            this.greyedOut = this.IsDisabled();

            base.draw(spriteBatch, slotX + this.GetOffsetX(), slotY, context);

            int sliderOffsetX = GetValueAtPosition(this.ValuePosition, 0, this.PixelWidth);
            IClickableMenu.drawTextureBox(spriteBatch, Game1.mouseCursors, OptionsSlider.sliderBGSource, slotX + this.bounds.X, slotY + this.bounds.Y, this.bounds.Width, this.bounds.Height, Color.White * (this.greyedOut ? 0.33f : 1f), Game1.pixelZoom, false);
            spriteBatch.Draw(Game1.mouseCursors, new Vector2(slotX + this.bounds.X + sliderOffsetX, slotY + this.bounds.Y), OptionsSlider.sliderButtonRect, Color.White * (this.greyedOut ? 0.33f : 1f), 0.0f, Vector2.Zero, Game1.pixelZoom, SpriteEffects.None, 0.9f);
        }

        /*********
        ** Private methods
        *********/

        /// <summary>Get the <see cref="Value"/>'s fractional position, as a value between 0 (<see cref="MinValue"/>) and 1 (<see cref="MaxValue"/>).</summary>
        private float GetRangePosition()
        {
            return GetRangePosition(this.Value, this.MinValue, this.MaxValue);
        }

        /****
        ** Math helpers
        ****/

        /// <summary>Get a value's fractional position within a range, as a value between 0 (<paramref name="minValue"/>) and 1 (<paramref name="maxValue"/>).</summary>
        /// <param name="value">The value within the range.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public static float GetRangePosition(int value, int minValue, int maxValue)
        {
            float position = (value - minValue) / (float)(maxValue - minValue);
            return MathHelper.Clamp(position, 0, 1);
        }

        /// <summary>Get the value from a position within a range.</summary>
        /// <param name="position">The position within the range, where 0 is the minimum value and 1 is the maximum value.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public static int GetValueAtPosition(float position, int minValue, int maxValue)
        {
            float value = position * (maxValue - minValue) + minValue;
            return (int)MathHelper.Clamp(value, minValue, maxValue);
        }
    }
}