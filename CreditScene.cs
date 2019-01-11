/*
 * CreditScene class depicts the name and email address of the author
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidField
{
    /// <summary>
    /// CreditScene is responsible for displaying information about the author of
    /// the game
    /// </summary>
    class CreditScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;

        /// <summary>
        /// CreditScene constructor
        /// </summary>
        public CreditScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            tex = game.Content.Load<Texture2D>("Images/credit_page");
        }
        /// <summary>
        /// Draws the credit scene background.
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
