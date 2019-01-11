/*
 * ActionScene class manages game logic
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace AsteroidField
{
    /// <summary>
    /// ActionScene class contains all objects used in the game,
    /// their interactions and game logic
    /// </summary>
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D background;
        private Texture2D goldTex;
        private Texture2D shattleTex;
        Game game;

        SpriteFont font;
        SpriteFont hilightFont;

        private Asteroid asteroid;
        private List<Asteroid> asteroids;
        private Spaceship shattle;
        private Gold pot;
        private CollisionManager cm;
        private Explosion explosion;
        private MessageBox messageBox;
        private Song backgroundMusic;

        /// <summary>
        /// ActionScene constructor
        /// </summary>
        public ActionScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;

            goldTex = game.Content.Load<Texture2D>("Images/gold_pot");
            font = game.Content.Load<SpriteFont>("Fonts/RegularFont");
            hilightFont = game.Content.Load<SpriteFont>("Fonts/HilightFont");

            asteroids = new List<Asteroid>();
            Texture2D tex = game.Content.Load<Texture2D>("Images/asteroid");

            //generate 5 meteors and  add them to the game components
            for (int i = 0; i < 5; i++)
            {
                asteroid = new Asteroid(game, spriteBatch, tex, Shared.offScreen, Vector2.Zero);
                asteroids.Add(asteroid);
                Components.Add(asteroid);
            }

            //generate a spaceship and add it to the game components
            shattleTex = game.Content.Load<Texture2D>("Images/shattle");
            shattle = new Spaceship(game, spriteBatch, shattleTex, Shared.offScreen, 0.0f);
            Components.Add(shattle);

            // generate a pot of gold object and add it to game components
            pot = new Gold(game, spriteBatch, goldTex, Shared.offScreen);
            Components.Add(pot);

            List<Texture2D> textures;
            textures = new List<Texture2D>();
            textures.Add(game.Content.Load<Texture2D>("Images/ButtonYes"));
            textures.Add(game.Content.Load<Texture2D>("Images/ButtonNo"));
            textures.Add(game.Content.Load<Texture2D>("Images/ButtonCancel"));

            // generate a message box and add it to the components
            messageBox = new MessageBox(game, spriteBatch, game.Content.Load<Texture2D>("Images/message_box"),
                    textures, font, new Vector2(275, 200));

            // add sound effects for pot collecting and explosion
            SoundEffect hit = game.Content.Load<SoundEffect>("Audio/Explosion");
            SoundEffect loot = game.Content.Load<SoundEffect>("Audio/coins");
            cm = new CollisionManager(game, asteroids, shattle, pot, hit, loot);
            Components.Add(cm);

            // background music
            backgroundMusic = game.Content.Load<Song>("Audio/Theyre-Here");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Stop();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // when Esc is pressed, a message box appears and asks if the user wants
            // to save the game before leaving
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !Shared.isPaused)
            {
                Shared.isPaused = true;

                game.Components.Add(messageBox);
                game.Components.Add(messageBox.ButtonYes);
                game.Components.Add(messageBox.ButtonNo);
                game.Components.Add(messageBox.ButtonCancel);
            }

            // every 15 seconds a new asteroid is generated
            if (Shared.timerAddAsteroid >= Shared.periodAddAsteroid)
            {
                Random r = new Random();
                int startInt = r.Next(0, (int)Shared.stageScene.X);
                int endInt = r.Next(0, (int)Shared.stageScene.Y);
                float speed = r.Next(1, 3);

                Vector2 direction = new Vector2(endInt - startInt, Shared.stageScene.Y);
                direction.Normalize();

                asteroid = new Asteroid(game, spriteBatch, game.Content.Load<Texture2D>("Images/asteroid"),
                    new Vector2(startInt, 0), speed * direction);
                asteroid.Visible = true;
                asteroid.Enabled = true;
                asteroids.Add(asteroid);
                Components.Add(asteroid);
                Shared.timerAddAsteroid = 0;
            }
            // pots appear and disappear from the screen only when the game is not paused
            if (!Shared.isPaused)
            {
                Shared.timerAddAsteroid += gameTime.ElapsedGameTime.TotalSeconds;

                Shared.timerGold += gameTime.ElapsedGameTime.TotalSeconds;

                // after each 10 seconds the gold pot was off the screen a new gold pot is added in
                // a random location
                if (!Shared.goldOnScreen && Shared.timerGold >= Shared.periodGoldShow && !Shared.gameOver)
                {

                    Random r = new Random();
                    int x = r.Next(goldTex.Width / 2, (int)Shared.stageScene.X - goldTex.Width);
                    int y = r.Next(goldTex.Height / 2, (int)Shared.stageScene.Y - goldTex.Height);
                    Rectangle goldRct = new Rectangle(x, y, goldTex.Width, goldTex.Height);
                    while (goldRct.Intersects(shattle.GetBounds()))
                    {
                        x = r.Next(0, (int)Shared.stageScene.X - goldTex.Width);
                        y = r.Next(0, (int)Shared.stageScene.Y - goldTex.Height);
                        goldRct = new Rectangle(x, y, goldTex.Width, goldTex.Height);
                    }
                    pot.Position = new Vector2(x, y);
                    pot.Visible = true;
                    pot.Enabled = true;
                    Shared.goldOnScreen = true;
                    Shared.timerGold = 0;
                }
                // after spending 5 sec on the screen, the pot disappears
                if (Shared.goldOnScreen && Shared.timerGold >= Shared.periodGoldHide)
                {
                    pot.Enabled = false;
                    pot.Visible = false;
                    pot.Position = Shared.offScreen;
                    Shared.goldOnScreen = false;
                    Shared.timerGold = 0;
                }
            }

            // if a collision was detected, render an explosion animation
            if (Shared.exploded != Vector2.Zero)
            {
                explosion = new Explosion(game, spriteBatch, game.Content.Load<Texture2D>("Images/explosion"),
                    Shared.exploded - new Vector2(15, 15));
                Components.Add(explosion);
                pot.Position = Shared.offScreen;
                pot.Visible = false;
                pot.Enabled = false;
                Shared.timerGold = 0;
                Shared.exploded = Vector2.Zero;
            }
            // after the explosion animation was rendered, we display Game Over screen for 3 sec
            // after which the exit to the start scene is initiated
            if (Shared.gameOver == true)
            {
                MediaPlayer.Stop();
                if (Shared.startGameOverSound)
                {
                    SoundEffect gameOver = game.Content.Load<SoundEffect>("Audio/game-over-arcade");
                    gameOver.Play();
                    Shared.startGameOverSound = false;
                }
                background = game.Content.Load<Texture2D>("Images/GameOver");
                for (int i = 0; i < asteroids.Count; i++)
                {
                    asteroids[i].Enabled = false;
                    asteroids[i].Visible = false;
                    asteroids[i].Position = Shared.offScreen;
                }
                if (Shared.timerAddAsteroid >= 5.0f)
                {
                    SaveScore();
                    MediaPlayer.Stop();
                    RemoveComponents();
                    ResetGlobalVariables();
                    Shared.exitInitiated = true;
                    Shared.gameOver = false;
                }
            }

            // if the exit from game was initiated by user, game is saved (if requested),
            // components removed and global variable reset
            if (Shared.removeItems)
            {
                if (Shared.saveGame)
                {
                    SaveGame();
                    Shared.saveGame = false;
                }
                RemoveComponents();
                ResetGlobalVariables();
                MediaPlayer.Stop();
                Shared.removeItems = false;
                Shared.exitInitiated = true;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // draws background
            spriteBatch.Draw(background, new Rectangle(0, 0, (int)Shared.stageScene.X,
               (int)Shared.stageScene.Y), Color.White);
            // draws the player's name and the score in the top left corner
            spriteBatch.DrawString(font, "Player: " + Shared.playerName + "\nScore: " + Shared.score.ToString(),
                new Vector2(25, 15), Color.White);
            // when the Game Over background is displayed, shows the final score on it
            if (Shared.gameOver)
            {
                if (Shared.score < 10)
                {
                    spriteBatch.DrawString(hilightFont, Shared.score.ToString(),
                        new Vector2(407, 370), Color.Red);
                }
                else if (Shared.score > 10 && Shared.score < 100)
                {
                    spriteBatch.DrawString(hilightFont, Shared.score.ToString(),
                        new Vector2(400, 370), Color.Red);
                }
                else
                {
                    spriteBatch.DrawString(hilightFont, Shared.score.ToString(),
                        new Vector2(390, 370), Color.Red);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Makes all game components invisible and disables them
        /// </summary>
        private void RemoveComponents()
        {
            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].Visible = false;
                asteroids[i].Enabled = false;
            }
            while (asteroids.Count > 5)
            {
                asteroids.RemoveAt(0);
            }

            shattle.Rotation = 0.0f;
            shattle.Visible = false;
            shattle.Enabled = false;

            pot.Visible = false;
            pot.Enabled = false;

            cm.Enabled = false;
        }

        /// <summary>
        /// Resets global variables to their initial values
        /// </summary>
        private void ResetGlobalVariables()
        {
            Shared.score = 0;
            Shared.playerName = "";
            Shared.collisionHappened = false;
            Shared.exploded = Vector2.Zero;
            Shared.isPaused = false;
            Shared.startGameOverSound = false;
            Shared.removeItems = false;
            Shared.gameOver = false;
            Shared.goldOnScreen = false;
            Shared.timerGold = 0;
            Shared.timerAddAsteroid = 0;
        }

        /// <summary>
        /// Is invoked when we start a new game; puts all the components to their
        /// "start-a-game" positions and makes them visible
        /// </summary>
        public void InitializeGame()
        {
            MediaPlayer.Play(backgroundMusic);
            background = game.Content.Load<Texture2D>("Images/background");

            Vector2[] positions = new Vector2[5] { new Vector2(100, 150), new Vector2(220, 450),
                new Vector2(550, 410), new Vector2(600, 130), new Vector2(750,300)};

            Vector2[] speeds = new Vector2[5] { new Vector2(1.5f, 1), new Vector2(2.0f, 1),
                new Vector2(-1, 2), new Vector2(-1.0f, 0.9f), new Vector2(-1, 3)};
            for (int i = 0; i < 5; i++)
            {
                asteroids[i].Position = positions[i];
                asteroids[i].Movement = speeds[i];
                asteroids[i].Visible = true;
                asteroids[i].Enabled = true;
            }
            shattle.Position = new Vector2(Shared.stageScene.X / 2 - shattleTex.Width / 2,
                Shared.stageScene.Y / 2 - shattleTex.Height / 2);
            shattle.Visible = true;
            shattle.Enabled = true;

            cm.Enabled = true;
        }

        /// <summary>
        /// Reads the saved game from the save file, and restores the components'
        /// positions and values from it
        /// </summary>
        public bool LoadGame()
        {
            string lineContent = "";
            string[] bits;

            if (!File.Exists(Shared.saveFileName))
            {
                // display error message, return to the start scene
                return false;
            }

            using (StreamReader reader = new StreamReader(Shared.saveFileName))
            {
                if (reader.EndOfStream)
                {
                    // display empty file message
                }
                else
                {
                    try
                    {
                        int counter = 0;

                        //initiate player's name
                        lineContent = reader.ReadLine();
                        Shared.playerName = lineContent;
                        // initiate score
                        lineContent = reader.ReadLine();
                        Shared.score = int.Parse(lineContent);

                        // add spaceship
                        lineContent = reader.ReadLine();
                        bits = lineContent.Split('|');

                        shattle.Position = new Vector2(float.Parse(bits[0]), float.Parse(bits[1]));
                        shattle.Rotation = float.Parse(bits[2]);
                        shattle.Visible = true;
                        shattle.Enabled = true;

                        // initialize pot of gold
                        lineContent = reader.ReadLine();
                        bits = lineContent.Split('|');
                        if (bits[0] != "none")
                        {
                            pot.Position = new Vector2(float.Parse(bits[0]), float.Parse(bits[1]));
                            pot.Enabled = true;
                            pot.Visible = true;
                            Shared.goldOnScreen = true;
                        }
                        Shared.timerGold = float.Parse(bits[2]);

                        lineContent = reader.ReadLine();
                        Shared.timerAddAsteroid = float.Parse(lineContent);

                        //add the asteroids
                        do
                        {
                            lineContent = reader.ReadLine();
                            bits = lineContent.Split('|');

                            if (counter < 5)
                            {
                                asteroids[counter].Position = new Vector2(float.Parse(bits[0]), float.Parse(bits[1]));
                                asteroids[counter].Movement = new Vector2(float.Parse(bits[2]), float.Parse(bits[3]));
                                asteroids[counter].Visible = true;
                                asteroids[counter].Enabled = true;
                            }
                            else
                            {
                                Asteroid a = new Asteroid(game, spriteBatch,
                                    game.Content.Load<Texture2D>("Images/asteroid"),
                                    new Vector2(float.Parse(bits[0]), float.Parse(bits[1])),
                                    new Vector2(float.Parse(bits[2]), float.Parse(bits[3])));
                                a.Visible = true;
                                a.Enabled = true;
                                asteroids.Add(a);
                                Components.Add(a);
                            }
                            counter++;
                        } while (!reader.EndOfStream);

                        cm.Enabled = true;
                        MediaPlayer.Play(backgroundMusic);
                        background = game.Content.Load<Texture2D>("Images/background");
                    }
                    catch
                    {
                        // display error message
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Collects the components' positions and values meaningful for the game
        /// and stores them in a .sav file
        /// </summary>
        private void SaveGame()
        {
            string goldPot = "";
            string asteroidsData = "";

            string dataToWrite = Shared.playerName + "\n" + Shared.score.ToString() + "\n" +
                shattle.Position.X.ToString("N3") + "|" + shattle.Position.Y.ToString("N3") +
                "|" + shattle.Rotation.ToString("N3") + "\n";

            if (pot.Position == Shared.offScreen)
                goldPot = "none|none|";
            else
                goldPot = pot.Position.X.ToString("N3") + "|" + pot.Position.Y.ToString("N3") + "|";
            goldPot += Shared.timerGold.ToString("N3") + "\n";

            foreach (Asteroid item in asteroids)
            {
                asteroidsData += item.Position.X.ToString("N3") + "|" + item.Position.Y.ToString("N3") +
                       "|" + item.Movement.X.ToString("N3") + "|" + item.Movement.Y.ToString("N3") + "\n";
            }

            dataToWrite += goldPot + Shared.timerAddAsteroid.ToString("N3") + "\n" + asteroidsData.Trim('\n');
            using (StreamWriter writer = new StreamWriter(Shared.saveFileName))
            {
                writer.Write(dataToWrite);
            }
        }

        /// <summary>
        /// Opens a file containing 5 highest scores, reads them, adds the current score
        /// from the game, if there are more than 5 record, cuts the lowest one, then
        /// saves the result to the file
        /// </summary>
        private void SaveScore()
        {
            bool fileIsEmpty = false;
            string lineContent = "";
            string[] line = new string[2];
            SortedList<int, string> scoreList = new SortedList<int, string>();
            string dataToSave = "";

            StreamWriter writer;

            if (!File.Exists(Shared.scoreFileName))
            {
                writer = new StreamWriter(Shared.scoreFileName);
                writer.Close();
            }

            using (StreamReader reader = new StreamReader(Shared.scoreFileName))
            {
                if (reader.EndOfStream)
                    fileIsEmpty = true;
                else
                {
                    do
                    {
                        lineContent = reader.ReadLine();
                        line = lineContent.Split('|');
                        scoreList.Add(int.Parse(line[0]), line[1]);
                    } while (!reader.EndOfStream);
                    while (scoreList.ContainsKey(Shared.score)) Shared.score++;
                    scoreList.Add(Shared.score, Shared.playerName + "/" + DateTime.Now.ToString());
                }
            }

            if (fileIsEmpty)
                dataToSave = Shared.score.ToString() + "|" + Shared.playerName + "/" + DateTime.Now.ToString();
            else
            {
                if (scoreList.Count > 5)
                    scoreList.RemoveAt(0);
                for (int i = scoreList.Count - 1; i >= 0; i--)
                {
                    dataToSave += scoreList.Keys[i].ToString() + "|" + scoreList.Values[i] + "\n";
                }
                dataToSave.Trim('\n');
            }
            using (writer = new StreamWriter(Shared.scoreFileName))
            {
                writer.Write(dataToSave);
            }
        }
    }
}
