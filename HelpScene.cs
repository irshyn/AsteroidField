/*
 * HelpScene class provides a description of the game
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidField
{
    /// <summary>
    /// HelpScene class is responsible for displaying information about the game
    /// </summary>
    class HelpScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;

        /// <summary>
        /// HelpScene constructor
        /// </summary>
        public HelpScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            tex = game.Content.Load<Texture2D>("Images/help_page");
        }

        /// <summary>
        /// Draws the help scene background.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, new Vector2(0, 0), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
