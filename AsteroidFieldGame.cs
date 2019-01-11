/*
 * AsteroidFieldGame class manages switching from one scene to another
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text.RegularExpressions;

namespace AsteroidField
{
    /// <summary>
    /// This is the main type for the Asteroid Field game.
    /// </summary>
    public class AsteroidFieldGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // all of of the scenes used in the game
        private StartScene startScene;
        private ActionScene actionScene;
        private HighScoreScene highScoreScene;
        private HelpScene helpScene;
        private CreditScene creditScene;

        // the message boxes
        private AskNameBox askNameBox;
        private NoSavedFile noSavedFile;

        /// <summary>
        /// AsteroidFieldGame constructor
        /// </summary>
        public AsteroidFieldGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = (int)Shared.stageScene.X;
            graphics.PreferredBackBufferHeight = (int)Shared.stageScene.Y;
            Window.TextInput += TextInputHandler;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Shared.stageScene = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            startScene = new StartScene(this, spriteBatch);
            this.Components.Add(startScene);
            startScene.Show();

            askNameBox = new AskNameBox(this, spriteBatch, this.Content.Load<Texture2D>("Images/AskName"));
            this.Components.Add(askNameBox);

            noSavedFile = new NoSavedFile(this, spriteBatch, this.Content.Load<Texture2D>("Images/NoSavedFile"));
            this.Components.Add(noSavedFile);

            actionScene = new ActionScene(this, spriteBatch);
            this.Components.Add(actionScene);

            highScoreScene = new HighScoreScene(this, spriteBatch);
            this.Components.Add(highScoreScene);

            helpScene = new HelpScene(this, spriteBatch);
            this.Components.Add(helpScene);

            creditScene = new CreditScene(this, spriteBatch);
            this.Components.Add(creditScene);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            if (startScene.Enabled)
            {
                MouseState ms = Mouse.GetState();

                selectedIndex = startScene.Menu.SelectedIndex;

                // if "Start New Game" option was selected
                if (selectedIndex == 0)
                {
                    if (!askNameBox.Visible)
                    {
                        if (ks.IsKeyDown(Keys.Enter))
                        {
                            askNameBox.Visible = true;
                            askNameBox.Enabled = true;
                            startScene.Menu.BoxVisible = true;
                        }
                    }
                    else
                    {
                        if (ks.IsKeyDown(Keys.Enter) || (ms.LeftButton == ButtonState.Pressed &&
                            ms.X > askNameBox.ButtonYesPosition.X && ms.X < askNameBox.ButtonYesPosition.X + askNameBox.ButtonSize.X &&
                            ms.Y > askNameBox.ButtonYesPosition.Y && ms.Y < askNameBox.ButtonYesPosition.Y + askNameBox.ButtonSize.Y))
                        {
                            if (askNameBox.PlayerName.Trim() != "")
                            {
                                actionScene.InitializeGame();
                                Shared.playerName = askNameBox.PlayerName.Trim();
                                askNameBox.PlayerName = "";
                                askNameBox.Enabled = false;
                                askNameBox.Visible = false;
                                startScene.Menu.BoxVisible = false;
                                startScene.Hide();
                                actionScene.Show();
                            }
                        }
                        else if (ks.IsKeyDown(Keys.Escape) || (ms.LeftButton == ButtonState.Pressed &&
                            ms.X > askNameBox.ButtonCancelPosition.X && ms.X < askNameBox.ButtonCancelPosition.X + askNameBox.ButtonSize.X &&
                            ms.Y > askNameBox.ButtonCancelPosition.Y && ms.Y < askNameBox.ButtonCancelPosition.Y + askNameBox.ButtonSize.Y))
                        {
                            askNameBox.PlayerName = "";
                            askNameBox.Visible = false;
                            askNameBox.Enabled = false;
                            startScene.Menu.BoxVisible = false;
                        }
                    }
                }

                // if "Load Game" was selected
                if (selectedIndex == 1)
                {
                    if (!noSavedFile.Visible && ks.IsKeyDown(Keys.Enter))
                    {
                        if (actionScene.LoadGame())
                        {
                            startScene.Hide();
                            actionScene.Show();
                        }
                        else
                        {                            
                            noSavedFile.Visible = true;
                            noSavedFile.Enabled = true;
                            startScene.Menu.BoxVisible = true;
                        }
                    }
                    else
                    {
                        if (ks.IsKeyDown(Keys.Escape) || (ms.LeftButton == ButtonState.Pressed &&
                            ms.X > noSavedFile.ButtonYesPosition.X && ms.X < noSavedFile.ButtonYesPosition.X + noSavedFile.ButtonSize.X &&
                            ms.Y > noSavedFile.ButtonYesPosition.Y && ms.Y < noSavedFile.ButtonYesPosition.Y + noSavedFile.ButtonSize.Y))
                        {
                            noSavedFile.Visible = false;
                            noSavedFile.Enabled = false;
                            startScene.Menu.BoxVisible = false;
                        }
                    }

                }

                // if "Help" option was selected
                if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.Hide();
                    helpScene.Show();
                }

                // if "High Score" option was selected
                if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.Hide();
                    highScoreScene.LoadScores();
                    highScoreScene.Show();
                }

                // if "Credit" option was selected
                if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.Hide();
                    creditScene.Show();
                }

                // "Quit" option was selected
                if (selectedIndex == 5 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }

            if (actionScene.Enabled)
            {
                if (Shared.exitInitiated == true)
                {
                    actionScene.Hide();
                    startScene.Show();
                    Shared.exitInitiated = false;
                }
            }

            if (highScoreScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    highScoreScene.Hide();
                    startScene.Show();
                }
            }

            if (helpScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    helpScene.Hide();
                    startScene.Show();
                }
            }

            if (creditScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    creditScene.Hide();
                    startScene.Show();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        /// <summary>
        /// Manages the text unput from the user
        /// </summary>
        /// <param name="sender">Contains reference to the control/object 
        /// that triggered the event</param>
        /// <param name="args">Contains the text input from the keyboard</param>
        private void TextInputHandler(object sender, TextInputEventArgs args)
        {
            if (askNameBox.Visible)
            {
                var character = args.Character;
                if (Regex.IsMatch(character.ToString(), @"^[A-Za-z0-9\-. ]$"))
                {
                    askNameBox.PlayerName += character;
                }
                else if (character == '\b' && askNameBox.PlayerName.Length > 0)
                {
                    askNameBox.PlayerName = 
                        askNameBox.PlayerName.Remove(askNameBox.PlayerName.Length - 1);
                }
            }
        }
    }
}
