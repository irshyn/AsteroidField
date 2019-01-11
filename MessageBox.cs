/*
 * MessageBox class represent a message box prompting user
 * to choose between saving game, go to the main menu without saving,
 * or return to the game
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace AsteroidField
{
    /// <summary>
    /// MessageBox prompts the user to Ok, deny or cancel saving the game
    /// </summary>
    class MessageBox : DrawableGameComponent
    {
        private const float BUTTON_TOP_SHIFT = 120f;
        private const float BUTTONYES_LEFT_SHIFT = 18.75f;
        private const float BUTTONNO_LEFT_SHIFT = 112.5f;
        private const float BUTTONCANCEL_LEFT_SHIFT = 206.25f;

        private SpriteBatch spriteBatch;
        private Vector2 position;
        private Texture2D tex;
        private List<Texture2D> texButton;
        private SpriteFont font;
        private Button buttonYes;
        private Button buttonNo;
        private Button buttonCancel;

        internal Button ButtonYes { get => buttonYes; set => buttonYes = value; }
        internal Button ButtonNo { get => buttonNo; set => buttonNo = value; }
        internal Button ButtonCancel { get => buttonCancel; set => buttonCancel = value; }

        /// <summary>
        /// MessageBox constructor.
        /// </summary>
        public MessageBox(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            List<Texture2D> texButton,
            SpriteFont font,
            Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.texButton = texButton;
            this.font = font;
            this.position = position;

            buttonYes = new Button(game, spriteBatch, texButton[0],
                new Vector2(position.X + BUTTONYES_LEFT_SHIFT, position.Y + BUTTON_TOP_SHIFT));

            buttonNo = new Button(game, spriteBatch, texButton[1],
                new Vector2(position.X + BUTTONNO_LEFT_SHIFT, position.Y + BUTTON_TOP_SHIFT));

            buttonCancel = new Button(game, spriteBatch, texButton[2],
                new Vector2(position.X + BUTTONCANCEL_LEFT_SHIFT, position.Y + BUTTON_TOP_SHIFT));
        }

        /// <summary>
        /// Handles the message box' response to a mouse click on one of its buttons
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed)
            {
                if (ms.X > position.X + BUTTONYES_LEFT_SHIFT &&
                    ms.X < position.X + BUTTONYES_LEFT_SHIFT + texButton[0].Width &&
                    ms.Y > position.Y + BUTTON_TOP_SHIFT &&
                    ms.Y < position.Y + BUTTON_TOP_SHIFT + texButton[0].Height)
                {
                    Shared.saveGame = true;
                    Game.Components.Remove(buttonYes);
                    Game.Components.Remove(buttonNo);
                    Game.Components.Remove(buttonCancel);
                    Game.Components.Remove(this);

                    Shared.removeItems = true;
                }

                if (ms.X > position.X + BUTTONNO_LEFT_SHIFT &&
                    ms.X < position.X + BUTTONNO_LEFT_SHIFT + texButton[1].Width &&
                    ms.Y > position.Y + BUTTON_TOP_SHIFT &&
                    ms.Y < position.Y + BUTTON_TOP_SHIFT + texButton[1].Height)
                {
                    Game.Components.Remove(buttonYes);
                    Game.Components.Remove(buttonNo);
                    Game.Components.Remove(buttonCancel);
                    Game.Components.Remove(this);

                    Shared.removeItems = true;
                }

                if (ms.X > position.X + BUTTONCANCEL_LEFT_SHIFT &&
                    ms.X < position.X + BUTTONCANCEL_LEFT_SHIFT + texButton[2].Width &&
                    ms.Y > position.Y + BUTTON_TOP_SHIFT &&
                    ms.Y < position.Y + BUTTON_TOP_SHIFT + texButton[2].Height)
                {
                    Game.Components.Remove(buttonYes);
                    Game.Components.Remove(buttonNo);
                    Game.Components.Remove(buttonCancel);
                    Game.Components.Remove(this);

                    Shared.isPaused = false;
                }
            }

            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the message box should appear on the screen.
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
