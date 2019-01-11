/*
 * Gold class represent an pot of gold element of the game
 * and its logic
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace AsteroidField
{
    /// <summary>
    /// Gold class is used to create a game component
    /// </summary>
    public class Gold : DrawableGameComponent
    {
        private const float MIN_SCALE = 0.7f;
        private const float MAX_SCALE = 1.3F;

        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;

        private Rectangle srcRec;
        public float Scale { get; set; } = 1.0f;
        public Vector2 Position { get => position; set => position = value; }

        private float scaleChange = 0.03f;

        /// <summary>
        /// Gold constructor.
        /// </summary>
        public Gold(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            srcRec = new Rectangle(0, 0, tex.Width, tex.Height);

            this.Enabled = false;
            this.Visible = false;
        }

        /// <summary>
        /// Draws a pot of gold on the screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRec, Color.White,
                0.0f, new Vector2(tex.Width / 2, tex.Height / 2), Scale, SpriteEffects.None, 0.0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Provides logic for the Gold class objects
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (!Shared.isPaused)
            {
                Scale += scaleChange;
                if (Scale > MAX_SCALE || Scale < MIN_SCALE)
                {
                    scaleChange = -scaleChange;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Returns a rectangle occupying the same space as the pot of gold
        /// </summary>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)(tex.Width * Scale),
                (int)(tex.Height * Scale));
        }
    }
}
