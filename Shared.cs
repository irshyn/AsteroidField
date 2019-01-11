/*
 * Shared class containes static variable that will be shared by all classes
 * of the game
 * Final Project
 * Revision History
 *                  Iryna Shynkevych:   30.11.2018 Created 
 */
using Microsoft.Xna.Framework;

namespace AsteroidField
{
    /// <summary>
    /// Shared is responsible storing the global variables
    /// </summary>
    class Shared
    {
        //the variable defining the dimensions of the game screen
        public static Vector2 stageScene = new Vector2(800, 600);
        // a position for the sprites that were desactivated and taken off the screen
        public static Vector2 offScreen = new Vector2(-100, -100);
        // stores the location of a collision
        public static Vector2 exploded = Vector2.Zero;

        // names of files to save the highest scores and to save the game
        public static string scoreFileName = "hscore.txt";
        public static string saveFileName = "Savegame.sav";

        // variables saving the score and player name
        public static int score = 0;
        public static string playerName = "";

        // the variables defining the state of the game
        public static bool isPaused = false;
        public static bool exitInitiated = false;
        public static bool collisionHappened = false;
        public static bool startGameOverSound = false;
        public static bool removeItems = false;
        // becomes true after the end  of explosion animation
        public static bool gameOver = false;
        // determines if the pot of gold is currently on the scene
        public static bool goldOnScreen = false;
        public static bool saveGame = false;

        public static double timerGold = 0;
        // timer keeping the time needed to generate a new asteroid
        public static double timerAddAsteroid = 0;

        // the time period over which a new asteroid is generated
        public static float periodAddAsteroid = 15f;
        // the period over which a new pot of gold appears on the screen
        public static float periodGoldShow = 10.0f;
        // the time span over which a pot of gold remains on the screen
        public static float periodGoldHide = 5.0f;
    }
}
