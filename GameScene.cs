/*
 * GameScene class is a superclass implementing functions that allow to
 * hide and show scenes, and draw/update the game components.
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AsteroidField
{
    /// <summary>
    /// GameScene manages showing, hiding, updating and drawing of the scenes
    /// the game.
    /// </summary>
    public abstract class GameScene : DrawableGameComponent
    {
        private List<GameComponent> components;
        public List<GameComponent> Components { get => components; set => components = value; }

        /// <summary>
        /// Makes the scene visible and enabled.
        /// </summary>
        public virtual void Show()
        {
            this.Enabled = true;
            this.Visible = true;
        }
        /// <summary>
        /// Hides and disables the scene.
        /// </summary>
        public virtual void Hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }
        /// <summary>
        /// GameScene constructor
        /// </summary>
        public GameScene(Game game) : base(game)
        {
            components = new List<GameComponent>();
            Hide();
        }

        /// <summary>
        /// Draws game components that are drawable and have Visible property set to true.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent comp = null;
            foreach (GameComponent item in components)
            {
                if (item is DrawableGameComponent)
                {
                    comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }
        /// <summary>
        /// Updates game components are enabled.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }
    }
}
