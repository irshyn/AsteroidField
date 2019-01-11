/*
 * Explosion class is responsible for drawing explosion animation
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace AsteroidField
{
    /// <summary>
    /// Explosion class manages the display of explosion animation
    /// </summary>
    public class Explosion : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;

        public Vector2 Position { get => position; set => position = value; }

        /// <summary>
        /// Explosion constructor.
        /// </summary>
        public Explosion(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            dimension = new Vector2(64, 64);
            delay = 3;

            createFrames();
        }

        /// <summary>
        /// Creates a list of images depicting different stages of an explosion.
        /// </summary>
        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;

                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);

                    frames.Add(r);
                }
            }
        }
        /// <summary>
        /// This is called when a collision between a spaceship and an asteroid occurs
        /// to draw a set of images creating an explosion animation.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                spriteBatch.Draw(tex, position, frames[frameIndex], Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }


        /// <summary>
        /// Responsible for parsing through the list of images and informing other classes
        /// when the explosion animation has come to an end.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            delayCounter++;

            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > 24)
                {
                    frameIndex = -1;
                    this.Enabled = false;
                    this.Visible = false;
                    Shared.gameOver = true;
                    Shared.startGameOverSound = true;
                }
                delayCounter = 0;
            }
            base.Update(gameTime);
        }
    }
}
