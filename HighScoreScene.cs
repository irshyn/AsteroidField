/*
 * HighScoreScene class depicts the list of the five highest scores
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace AsteroidField
{
    /// <summary>
    /// HighScoreScene class is responsible for displaying the highest scores
    /// obtained in the game
    /// </summary>
    class HighScoreScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private SpriteFont hilightFont;
        string title;
        string highestScore;
        string message;

        /// <summary>
        /// HighScoreScene constructor
        /// </summary>
        public HighScoreScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            font = game.Content.Load<SpriteFont>("Fonts/RegularFont");
            hilightFont = game.Content.Load<SpriteFont>("Fonts/HilightFont");
            title = "The five highest scores are:";
            message = "To return to the main menu, press ESC";
            LoadScores();
        }

        /// <summary>
        /// Opens the file with the highest scores and displays them
        /// </summary>
        public void LoadScores()
        {
            if (!File.Exists(Shared.scoreFileName))
            {
                highestScore = "Problem when opening file";
            }

            else
            {
                using (StreamReader reader = new StreamReader(Shared.scoreFileName))
                {
                    highestScore = "";

                    if (reader.EndOfStream)
                        highestScore = "No saved score to display";
                    else
                    {
                        do
                        {
                            highestScore += reader.ReadLine() + "\n";
                        } while (!reader.EndOfStream);
                        highestScore = highestScore.Replace("|", ": scored by ");
                        highestScore = highestScore.Replace("/", " on ");
                    }
                }
            }
        }

        /// <summary>
        /// Draws the list of the five highest scores.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(hilightFont, title, new Vector2(50, 50), Color.Red);
            spriteBatch.DrawString(font, highestScore, new Vector2(50, 100), Color.White);
            spriteBatch.DrawString(hilightFont, message, new Vector2(50, 400), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
