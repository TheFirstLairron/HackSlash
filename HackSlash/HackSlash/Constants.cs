using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class Constants
    {
        public enum DIRECTION
        {
            NORTH = 0,
            EAST,
            SOUTH,
            WEST
        }

        public enum MAP_CHARS
        {
            EMPTY = ' ',
            ENEMY = '*',
            CHARACTER = '@',
            EXIT = '0',
            ITEMBOX = 'o'
        }

        // A list of the allowed keys for user input
        public static readonly List<ConsoleKey> allowedKeys = new List<ConsoleKey>()
        {
            ConsoleKey.A,
            ConsoleKey.W,
            ConsoleKey.S,
            ConsoleKey.D,
            ConsoleKey.Escape,
            ConsoleKey.Spacebar
        };

        // The main menu
        public static readonly List<string> mainMenuOptions = new List<string>()
        {
            "Main Menu: ",
            "   -Resume",
            "   -Inventory",
            "   -Equipment",
            "   -Key Items",
            "   -Quit"
        };

        public static string Level1Name = "Courtyard";
        public static string Level2Name = "Main Entryway";
        public static string Level3Name = "Basement Entrance";
        public static string Level4Name = "Dungeon Arena";

        #region Weapons
        public static string RustyScytheName = "Rusty Scythe";
        public static string RustyScytheDesc = "An old scythe with a rusty blade";
        public static int RustyScytheDamage = 5;

        public static string BoxScytheName = "Box Scythe";
        public static string BoxScytheDesc = "The boxiest of all boxes";
        public static int BoxScytheDamage = 15;

        public static string MegaScytheName = "Mega Scythe";
        public static string MegaScytheDesc = "An ancient scythe infused with immense power";
        public static int MegaScytheDamage = 500;
        #endregion

        #region HealingItems
        public static string BasicPotionName = "Basic Potion";
        public static int BasicPotionHeal = 10;
        public static string BasicPotionDesc = $"A basic potion that heals the player by {BasicPotionHeal}";

        public static string WeakenedPotionName = "Weakened Potion";
        public static int WeakenedPotionHeal = 5;
        public static string WeakenedPotionDesc = $"A tainted potion that heals the player by {WeakenedPotionHeal}";

        public static string SuperPotionName = "Super Potion";
        public static int SuperPotionHeal = 30;
        public static string SuperPotionDesc = $"A powerful potion that heals the player by {SuperPotionHeal}";
        #endregion

        #region KeyItems
        public static string EntrywayKeyName = "Entryway Key";
        public static string EntrywayKeyDesc = "A key to the entryway of Hallow Manor";

        public static string JaildoorKeyName = "Jailer's Key";
        public static string JaildoorKeyDesc = "The key to the jail door in the main room of the manor";
        #endregion

        #region Levels
        public static char[,] firstMap = new char[,]
        {
             {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
             {'#', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', '#'},
             {'#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', '#'},
             {'#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', '#'},
             {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'}
        };

        public static char[,] secondMap = new char[,]
        {
             {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', '#', '#', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'}
        };

        public static char[,] thirdMap = new char[,]
        {
             {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
             {'#', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', ' ', '#', '#', '#', '#', '#', '#', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'}
        };

        public static char[,] fourthMap = new char[,]
        {
             {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'}
        };
        #endregion
    }
}