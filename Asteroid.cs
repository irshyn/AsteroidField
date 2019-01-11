/*
 * Asteroid class represent an asteroid element of the game
 * and its logic
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidField
{
    /// <summary>
    /// Asteroid class is used to create a game component
    /// </summary>
    public class Asteroid : DrawableGameComponent
    {
        private const int MIN_SPEED_FACTOR = 1;
        private const int MAX_SPEED_FACTOR = 3;
        private const float ASTEROID_ROTATION_CHANGE = 0.03F;

        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 movement;

        private Rectangle srcRec;
        private float rotation = 0.0f;

        public Vector2 Position { get => position; set => position = value; }
        public float Rotation { get => rotation; }
        public Vector2 Movement { get => movement; set => movement = value; }

        /// <summary>
        /// Asteroid constructor.
        /// </summary>
        public Asteroid(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 movement) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.movement = movement;

            this.Enabled = false;
            this.Visible = false;

            srcRec = new Rectangle(0, 0, tex.Width, tex.Height);
        }
        /// <summary>
        /// Provides logic for the asteroid class objects
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // if game is not on pause
            if (!Shared.isPaused)
            {
                // the asteroid will move and rotate
                position += movement;
                rotation += ASTEROID_ROTATION_CHANGE;

                // when the asteroid disappears from the screen, it is relocated to
                //  the top of the screen at a random location and is given a randomly
                // generated speed vector
                if (position.Y > Shared.stageScene.Y)
                {
                    if (Shared.collisionHappened == false)
                    {
                        Shared.score++;
                    }
                    Random r = new Random();
                    int startInt = r.Next(0, (int)Shared.stageScene.X);
                    int endInt = r.Next(0, (int)Shared.stageScene.X);
                    float speed = r.Next(MIN_SPEED_FACTOR, MAX_SPEED_FACTOR);

                    Vector2 direction = new Vector2(endInt - startInt, Shared.stageScene.Y);
                    direction.Normalize();

                    position = new Vector2(startInt, 0);
                    movement = speed * direction;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the asteroid on the screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRec, Color.White,
                rotation, new Vector2(tex.Width / 2, tex.Height / 2), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Returns a rectangle occupying the same space as the asteroid
        /// </summary>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
