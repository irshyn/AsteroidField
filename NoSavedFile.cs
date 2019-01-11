/*
 * NoSavedFile class represent a message box advising user
 * that there is no file to load
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidField
{
    /// <summary>
    /// NoSavedFile is a message box that advises the user
    /// that there is no game file to load
    /// </summary>
    class NoSavedFile : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Vector2 position;
        private Vector2 buttonYesPosition;
        private Vector2 buttonSize;
        private Texture2D tex;

        public Vector2 Position { get => position; }
        public Vector2 ButtonSize { get => buttonSize; }
        public Vector2 ButtonYesPosition { get => buttonYesPosition; }

        public NoSavedFile(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = new Vector2(200, 200);
            this.buttonYesPosition = new Vector2(249, 185) + position;
            this.buttonSize = new Vector2(99, 50);

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
            // draws the message box
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
