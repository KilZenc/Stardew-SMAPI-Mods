using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using Object = StardewValley.Object;

namespace FishingAssistant
{
    internal partial class ModEntry : Mod
    {
        private bool isCatching = false;
        private int fishPreviewFishId = 0;
        private Object fishSprite;
        private Object treasureSprite;
        private bool showFish;
        private bool showText;
        private string textValue;
        private IList<string> displayOrder;
        private Vector2 textSize;
        private int margin = 18;
        private int spriteSize = 64;
        private float scale = 0.7f;
        private int boxwidth;
        private int boxheight;
        private int treasureSpacing = 50;
        private int treasureOffset = 8;

        private void DrawFishInfo()
        {
            if (Game1.player == null || !Game1.player.IsLocalPlayer || !Config.DisplayFishInfo)
                return;

            if (Game1.activeClickableMenu is BobberBar bar && isCatching)
            {
                // stop drawing on fadeOut
                if (BarFadeOut)
                    return;

                // figure out which fish is being caught
                int newFishId = BarWhichFish;

                // check if fish has changed somehow. If yes, re-make the fish sprite and its display text.
                GetFishData(newFishId);

                // call a function to position and draw the box
                DrawFishDisplay(displayOrder, bar);
            }
        }

        private void GetFishData(int newFishId)
        {
            if (newFishId != fishPreviewFishId) // catching a new fish OR the fish species has changed mid-cast
            {
                fishPreviewFishId = newFishId;
                Monitor.Log($"Currently catching: {fishPreviewFishId}", LogLevel.Trace);

                // save fish object to use in drawing // check for errors?
                fishSprite = new Object(fishPreviewFishId, 1);

                // determine if species has been caught before
                bool caughtSpecies = Game1.player.fishCaught.ContainsKey(fishPreviewFishId) && Game1.player.fishCaught[fishPreviewFishId][0] > 0;

                // is it a legendary fish?
                bool isLegendary = Helper.Reflection.GetMethod(typeof(FishingRod), "isFishBossFish").Invoke<bool>(fishPreviewFishId);
                Monitor.Log($"Catching legendary fish? {isLegendary}", LogLevel.Trace);

                // determine value of showFish value
                showFish = Config.ShowUncaughtFishSpecies || caughtSpecies || (Config.AlwaysShowLegendaryFish && isLegendary);

                // determine value of showText value
                showText = Config.ShowFishName;

                // determine text to show if true
                if (showText && showFish)
                {
                    textValue = fishSprite.DisplayName;  // does it need translation help? (Seems to work with French already)
                    this.Monitor.Log($"Fish name: {textValue}", LogLevel.Trace);
                }
                else
                {
                    textValue = "???";
                }

                // determine width and height of display box
                boxwidth = 160; boxheight = 100;

                textSize = Game1.dialogueFont.MeasureString(textValue) * scale;

                if (showText && showFish) { boxwidth = Math.Max(160, (2 * margin) + (int)textSize.X); }
                if (showText) { boxheight += (int)textSize.Y; }
            }
        }

        private void DrawFishDisplay(IList<string> positions, BobberBar bar)
        {
            // call a function to determine the x and y coords
            DetermineCoordinates(positions, bar, out int x, out int y);
            // call a function to draw the box
            DrawAtCoordinates((int)(x * (Game1.options.zoomLevel / Game1.options.uiScale)), (int)(y * (Game1.options.zoomLevel / Game1.options.uiScale)));
        }

        private void DetermineCoordinates(IList<string> positions, BobberBar bar, out int x, out int y)
        {
            // define offset values
            int xLeftOffset = -32; int xCenterOffset = 32; int xRightOffset = 80;
            int yTopOffset = -32; int yUpperOffset = 0; int yLowerOffset = -40; int yBottomOffset = -12;

            // determine x and y positions
            foreach (string position in positions)
            {
                // set the correct display coordinates from position values in order of priority
                switch (position)
                {
                    case "Top":
                        x = bar.xPositionOnScreen + (bar.width / 2) - (boxwidth / 2) + xCenterOffset;
                        y = bar.yPositionOnScreen - boxheight + yTopOffset;
                        break;

                    case "UpperRight":
                        x = bar.xPositionOnScreen + bar.width + xRightOffset;
                        y = bar.yPositionOnScreen + yUpperOffset;
                        break;

                    case "UpperLeft":
                        x = bar.xPositionOnScreen - boxwidth + xLeftOffset;
                        y = bar.yPositionOnScreen + yUpperOffset;
                        break;

                    case "Bottom":
                        x = bar.xPositionOnScreen + (bar.width / 2) - (boxwidth / 2) + xCenterOffset;
                        y = bar.yPositionOnScreen + bar.height + yBottomOffset;
                        break;

                    case "LowerRight":
                        x = bar.xPositionOnScreen + bar.width + xRightOffset;
                        y = bar.yPositionOnScreen + bar.height - boxheight + yLowerOffset;
                        break;

                    case "LowerLeft":
                        x = bar.xPositionOnScreen - boxwidth + xLeftOffset;
                        y = bar.yPositionOnScreen + bar.height - boxheight + yLowerOffset;
                        break;

                    default:
                        // default to UpperRight position
                        x = bar.xPositionOnScreen + bar.width + xRightOffset;
                        y = bar.yPositionOnScreen + yUpperOffset;
                        //this.Monitor.Log($"Invalid position {position} listed in displayOrder.", LogLevel.Debug);
                        break;
                }

                // if the box display is in bounds, break the loop. Otherwise proceed to alternative display position(s).
                if (x >= 0 && y >= 0 && x + boxwidth <= Game1.viewport.Width && y + boxheight <= Game1.viewport.Height)
                    return;
            }
            // if no suitable location found in foreach, default to UpperRight position
            x = bar.xPositionOnScreen + bar.width + xRightOffset;
            y = bar.yPositionOnScreen + yUpperOffset;
            //this.Monitor.Log($"No suitable coordinate position found in displayOrder.", LogLevel.Debug);
        }

        private void DrawAtCoordinates(int x, int y)
        {
            // draw box of height and width at location
            IClickableMenu.drawTextureBox(Game1.spriteBatch, x, y, boxwidth, boxheight, Color.White);

            //Calculate draw position for fish
            Vector2 drawPos;
            if (Config.ShowTreasure && BarHasTreasure && !BarTreasureCaught)
                drawPos = new Vector2(x + (boxwidth / 2) - (spriteSize / 2) - (treasureSpacing / 2) + treasureOffset, y + margin);
            else
                drawPos = new Vector2(x + (boxwidth / 2) - (spriteSize / 2), y + margin);

            if (showFish)  // if showFish, draw fish in normal color
                fishSprite.drawInMenu(Game1.spriteBatch, drawPos, 1.0f, 1.0f, 1.0f, StackDrawType.Hide);
            else               // else (if not showFish), draw fish in black color
                fishSprite.drawInMenu(Game1.spriteBatch, drawPos, 1.0f, 1.0f, 1.0f, StackDrawType.Hide, Color.Black * 0.8f, true);

            if (showText) // if showText, center the text x below the fish
                Game1.spriteBatch.DrawString(Game1.dialogueFont, textValue, new Vector2(x + (boxwidth / 2) - ((int)textSize.X / 2), y + spriteSize + margin), Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            // if show treasure draw treasure with fish icon
            if (Config.ShowTreasure && BarHasTreasure && !BarTreasureCaught)
            {
                treasureSprite = new Object(693, 1);
                treasureSprite.drawInMenu(Game1.spriteBatch, new Vector2(x + (boxwidth / 2) - (spriteSize / 2) + (treasureSpacing / 2) + treasureOffset, y + margin), 1.0f, 1.0f, 1.0f, StackDrawType.Hide);
            }
        }
    }
}