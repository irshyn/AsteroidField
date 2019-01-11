/*
 * Spaceship class create and manages spaceship element of the game
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsteroidField
{
    /// <summary>
    /// Spaceship class is used to create a spaceship game component
    /// </summary>
    public class Spaceship : DrawableGameComponent
    {
        private const float SHIP_SPEED = 2.0F;
        private SpriteBatch spriteBatch;
        private Texture2D tex;

        Vector2 position;
        private Rectangle srcRect;
        private Vector2 origin;
        private float rotation = 0.0f;
        private float speed = SHIP_SPEED;

        public Vector2 Position { get => position; set => position = value; }
        public float Rotation { get => rotation; set => rotation = value; }

        /// <summary>
        /// Spaceship constructor.
        /// </summary>
        public Spaceship(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            float rotation) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.rotation = rotation;
            srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);

            this.Enabled = false;
            this.Visible = false;
        }

        /// <summary>
        /// Provides logic for the Spaceship class objects
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (!Shared.isPaused)
            {
                KeyboardState ks = Keyboard.GetState();


                if (ks.IsKeyDown(Keys.Up) && ks.IsKeyDown(Keys.Left) &&
                    position.Y - tex.Height / 2 > 0 && position.X - tex.Width / 2 > 0)
                {
                    position.X -= speed;
                    position.Y -= speed;
                    rotation = (-1) * (float)Math.PI / 4;
                }
                else if (ks.IsKeyDown(Keys.Up) && ks.IsKeyDown(Keys.Right) &&
                    position.Y - tex.Height / 2 > 0 && position.X + tex.Width / 2 < Shared.stageScene.X)
                {
                    position.X += speed;
                    position.Y -= speed;
                    rotation = (float)Math.PI / 4;
                }
                else if (ks.IsKeyDown(Keys.Left) && ks.IsKeyDown(Keys.Down) &&
                    position.X - tex.Width / 2 > 0 && position.Y + tex.Height / 2 < Shared.stageScene.Y)
                {
                    position.X -= speed;
                    position.Y += speed;
                    rotation = (float)Math.PI * 3 * (-1) / 4;
                }
                else if (ks.IsKeyDown(Keys.Down) && ks.IsKeyDown(Keys.Right) &&
                    position.Y + tex.Height / 2 < Shared.stageScene.Y && position.X + tex.Width / 2 < Shared.stageScene.X)
                {
                    position.X += speed;
                    position.Y += speed;
                    rotation = (float)Math.PI * 3 / 4;
                }
                else if (ks.IsKeyDown(Keys.Up) && position.Y - tex.Height / 2 > 0)
                {
                    position.Y -= speed;
                    rotation = 0;
                }
                else if (ks.IsKeyDown(Keys.Right) && position.X + tex.Width / 2 < Shared.stageScene.X)
                {
                    position.X += speed;
                    rotation = (float)Math.PI / 2;
                }
                else if (ks.IsKeyDown(Keys.Down) && position.Y + tex.Height / 2 < Shared.stageScene.Y)
                {
                    position.Y += speed;
                    rotation = (float)Math.PI * (-1);
                }
                else if (ks.IsKeyDown(Keys.Left) && position.X - tex.Width / 2 > 0)
                {
                    position.X -= speed;
                    rotation = (float)Math.PI * (-1) / 2;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws a spaceship on the screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Returns a rectangle occupying the same space as the spaceship
        /// </summary>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
