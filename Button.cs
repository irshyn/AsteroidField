/*
 * Button class represent a button used in a message box
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidField
{
    /// <summary>
    /// Button class creates an object used in a MessageBox class object
    /// </summary>
    class Button : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Vector2 position;
        private Texture2D tex;

        /// <summary>
        /// Button constructor
        /// </summary>
        public Button(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
        }

        /// <summary>
        /// This is called when the button is drawn.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
