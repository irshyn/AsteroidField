/*
 * AskNameBox class represent a message box prompting user
 * to enter their name
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidField
{
    /// <summary>
    /// AskNameBox is a message box that prompts the user
    /// to enter their name and handles the user's input
    /// </summary>
    public class AskNameBox : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Vector2 position;
        private Vector2 buttonYesPosition;
        private Vector2 buttonCancelPosition;
        private Vector2 buttonSize;
        private Texture2D tex;

        string playerName;
        SpriteFont font;

        public string PlayerName { get => playerName; set => playerName = value; }
        public Vector2 Position { get => position; }
        public Vector2 ButtonSize { get => buttonSize; }
        public Vector2 ButtonYesPosition { get => buttonYesPosition; }
        public Vector2 ButtonCancelPosition { get => buttonCancelPosition; }

        /// <summary>
        /// AskNameBox constructor.
        /// </summary>
        public AskNameBox(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = new Vector2(200, 200);
            this.buttonYesPosition = new Vector2(45, 186) + position;
            this.buttonCancelPosition = new Vector2(249, 185) + position;
            this.buttonSize = new Vector2(99, 50);

            playerName = "";
            font = game.Content.Load<SpriteFont>("Fonts/HilightFont");

            this.Visible = false;
            this.Enabled = false;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            // draws the user name input box itself
            spriteBatch.Draw(tex, position, Color.White);
            // draws the string corresponding to the one user has entered
            spriteBatch.DrawString(font, playerName, new Vector2(253, 312), Color.DarkBlue);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
