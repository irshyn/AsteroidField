/*
 * MenuComponent class manages scrolling through the menu options
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsteroidField
{
    /// <summary>
    /// MenuComponent  manages scrolling through the menu options
    /// </summary>
    public class MenuComponent : DrawableGameComponent
    {
        private Game game;
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, hilightFont;
        private List<string> menuItems;
        public int SelectedIndex { get; set; } = 0;
        public bool BoxVisible { get => boxVisible; set => boxVisible = value; }

        private Vector2 position;
        private Color regularColor = Color.Black;
        private Color hilightColor = Color.Red;
        private KeyboardState oldState;

        private bool boxVisible;

        /// <summary>
        /// MenuComponent constructor.
        /// </summary>
        public MenuComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont regularFont,
            SpriteFont hilightFont,
            string[] menus) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.hilightFont = hilightFont;
            menuItems = menus.ToList<string>();
            position = new Vector2(Shared.stageScene.X / 2, Shared.stageScene.Y / 2);
            boxVisible = false;
        }

        /// <summary>
        /// Draws the list composed of menu items, the selected item hilighted.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            spriteBatch.Begin();

            for (int i = 0; i < menuItems.Count(); i++)
            {
                if (SelectedIndex == i)
                {
                    spriteBatch.DrawString(hilightFont, menuItems[i], tempPos, hilightColor);
                    tempPos.Y += hilightFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i], tempPos, regularColor);
                    tempPos.Y += regularFont.LineSpacing;
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Handles the menu options' response to the use of Up and Down buttons
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (!boxVisible)
            {
                KeyboardState ks = Keyboard.GetState();
                if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
                {
                    SelectedIndex = (SelectedIndex + 1) % menuItems.Count;
                }

                if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = menuItems.Count - 1;
                    }
                }
                oldState = ks;
            }
            base.Update(gameTime);
        }
    }
}
