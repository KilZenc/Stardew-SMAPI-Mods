using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System;

namespace FishingAssistant.Menu.Option
{
    /// <summary>A button which lets the user choose a key binding.</summary>
    internal class KeyListenerOptionElement : BaseOptionsElement
    {
        /*********
        ** Fields
        *********/

        /// <summary>The current key binding.</summary>
        private SButton Value;

        /// <summary>The action to perform when the key binding is chosen.</summary>
        private readonly Action<SButton> SetValue;

        /// <summary>The translated 'press new key' label.</summary>
        private readonly string PressNewKeyLabel;

        /// <summary>The source rectangle for the 'set' button sprite.</summary>
        private readonly Rectangle SetButtonSprite = new Rectangle(294, 428, 21, 11);

        /// <summary>The button area in screen pixels.</summary>
        private Rectangle SetButtonBounds;

        /// <summary>The button to set when the player clears it.</summary>
        private readonly SButton ClearToButton;

        private string ListenerMessage;
        private bool Listening;

        /*********
        ** Public methods
        *********/

        /// <summary>Construct an instance.</summary>
        /// <param name="label">The field label.</param>
        /// <param name="slotWidth">The field width.</param>
        /// <param name="value">The current key binding.</param>
        /// <param name="setValue">The action to perform when the button is toggled.</param>
        /// <param name="clearToButton">The button to set when the player clears it.</param>
        public KeyListenerOptionElement(string label, int slotWidth, SButton value, Action<SButton> setValue, SButton clearToButton = SButton.None)
          : base(label, -1, -1, slotWidth + 1, 11 * Game1.pixelZoom)
        {
            Value = value;
            PressNewKeyLabel = I18n.Menu_Config_PressNewKey();
            SetValue = setValue;
            SetButtonBounds = new Rectangle(slotWidth - 28 * Game1.pixelZoom, -1 + Game1.pixelZoom * 3, 21 * Game1.pixelZoom, 11 * Game1.pixelZoom);
            ClearToButton = clearToButton;
        }

        /// <summary>Handle the player clicking the left mouse button.</summary>
        /// <param name="x">The cursor's X pixel position.</param>
        /// <param name="y">The cursor's Y pixel position.</param>
        public override void receiveLeftClick(int x, int y)
        {
            if (greyedOut || Listening || !SetButtonBounds.Contains(x, y) || Constants.TargetPlatform == GamePlatform.Android)
                return;

            Listening = true;
            Game1.soundBank.PlayCue("breathin");
            GameMenu.forcePreventClose = true;
            ListenerMessage = PressNewKeyLabel;
        }

        /// <summary>Handle the player pressing a keyboard button.</summary>
        /// <param name="key">The key that was pressed.</param>
        public override void receiveKeyPress(Keys key)
        {
            if (greyedOut || !Listening)
                return;

            if (key == Keys.Escape)
            {
                Value = ClearToButton;
                Game1.soundBank.PlayCue("bigDeSelect");
            }
            else
            {
                Value = key.ToSButton();
                Game1.soundBank.PlayCue("coin");
            }

            SetValue(Value);
            Listening = false;
            GameMenu.forcePreventClose = false;
        }

        /// <summary>Draw the component to the screen.</summary>
        /// <param name="spriteBatch">The sprite batch being drawn.</param>
        /// <param name="slotX">The X position at which to draw, relative to the bounds.</param>
        /// <param name="slotY">The Y position at which to draw, relative to the bounds.</param>
        /// <param name="context">The menu drawing the component.</param>
        public override void draw(SpriteBatch spriteBatch, int slotX, int slotY, IClickableMenu context = null)
        {
            Utility.drawTextWithShadow(spriteBatch, $"{label}: {Value}", Game1.dialogueFont, new Vector2(bounds.X + slotX, bounds.Y + slotY), greyedOut ? Game1.textColor * 0.33f : Game1.textColor, 1f, 0.15f);
            if (Constants.TargetPlatform != GamePlatform.Android)
                Utility.drawWithShadow(spriteBatch, Game1.mouseCursors, new Vector2(SetButtonBounds.X + slotX, SetButtonBounds.Y + slotY), SetButtonSprite, Color.White, 0.0f, Vector2.Zero, Game1.pixelZoom, false, 0.15f);

            if (Listening)
            {
                spriteBatch.Draw(Game1.staminaRect, new Rectangle(0, 0, Game1.graphics.GraphicsDevice.Viewport.Width, Game1.graphics.GraphicsDevice.Viewport.Height), new Rectangle(0, 0, 1, 1), Color.Black * 0.75f, 0.0f, Vector2.Zero, SpriteEffects.None, 0.999f);
                spriteBatch.DrawString(Game1.dialogueFont, ListenerMessage, Utility.getTopLeftPositionForCenteringOnScreen(Game1.tileSize * 3, Game1.tileSize), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9999f);
            }
        }
    }
}