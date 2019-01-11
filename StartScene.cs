/*
 * StartScene class is used to create an a scene which diplays a list of
 * menu options for the user to choose from
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidField
{
    /// <summary>
    /// StartScene is responsible for displaying menu options
    /// </summary>
    public class StartScene : GameScene
    {
        private SpriteBatch spriteBatch;
        public MenuComponent Menu { get; set; }


        string[] menuItems =
        {
            "Start New Game",
            "Load Game",
            "Help",
            "High Score",
            "Credit",
            "Quit"
        };

        /// <summary>
        /// StartScene constructor
        /// </summary>
        public StartScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            SpriteFont regularFont = game.Content.Load<SpriteFont>("Fonts/RegularFont");
            SpriteFont hilightFont = game.Content.Load<SpriteFont>("Fonts/HilightFont");
            Menu = new MenuComponent(game, spriteBatch, regularFont, hilightFont, menuItems);

            this.Components.Add(Menu);
        }
    }
}
