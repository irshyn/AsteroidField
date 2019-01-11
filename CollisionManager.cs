/*
 * CollisionManager class monitors game components in order to determine whether
 * a collision has occured and manages the collision situation
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace AsteroidField
{
    /// <summary>
    /// CollisionManager manages collision of game components
    /// </summary>
    class CollisionManager : GameComponent
    {
        private List<Asteroid> asteroids;
        private Spaceship shattle;
        private Gold gold;
        private SoundEffect explosion;
        private SoundEffect loot;

        /// <summary>
        /// CollisionManager constructor
        /// </summary>
        public CollisionManager(Game game,
            List<Asteroid> asteroids,
            Spaceship shattle,
            Gold gold,
            SoundEffect explosion,
            SoundEffect loot) : base(game)
        {
            this.asteroids = asteroids;
            this.shattle = shattle;
            this.gold = gold;
            this.explosion = explosion;
            this.loot = loot;

            this.Enabled = false;
        }

        /// <summary>
        /// Manages collisions between a spaceship and an asteroid and between a spaceship and
        /// a pot of gold.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (!Shared.isPaused)
            {
                Rectangle shipRec = shattle.GetBounds();
                for (int i = 0; i < asteroids.Count; i++)
                {
                    Rectangle meteorRec = asteroids[i].GetBounds();

                    if (shipRec.Intersects(meteorRec))
                    {
                        Shared.exploded = asteroids[i].Position;
                        Shared.timerAddAsteroid = 0;
                        Shared.collisionHappened = true;

                        shattle.Enabled = false;
                        shattle.Visible = false;
                        shattle.Position = Shared.offScreen;

                        asteroids[i].Enabled = false;
                        asteroids[i].Visible = false;
                        asteroids[i].Position = Shared.offScreen;

                        explosion.Play();

                        this.Enabled = false;
                    }
                }

                if (Shared.goldOnScreen)
                {
                    Rectangle rectGold = gold.GetBounds();
                    if (shipRec.Intersects(rectGold))
                    {
                        gold.Enabled = false;
                        gold.Visible = false;
                        gold.Position = Shared.offScreen;
                        Shared.score += 20;
                        Shared.goldOnScreen = false;
                        loot.Play();
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
