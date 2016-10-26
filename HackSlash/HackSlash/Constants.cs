using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class Constants
    {
        public static readonly List<ConsoleKey> allowedKeys = new List<ConsoleKey>() 
        {
            ConsoleKey.A,
            ConsoleKey.W,
            ConsoleKey.S,
            ConsoleKey.D,
            ConsoleKey.Escape,
            ConsoleKey.Spacebar
        };

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
            EXIT = '0'
        }

        public static readonly List<string> mainMenuOptions = new List<string>()
        {
            "Main Menu: ",
            "   -Resume",
            "   -Inventory",
            "   -Equipment",
            "   -Quit"
        };
    
        public static char[,] firstMap = new char[,]
        {
             {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', '#', '#', '#', '#', '#', ' ', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', '#', '#', ' ', ' ', ' ', ' ', '#', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'}
        };

        public static char[,] secondMap = new char[,]
        {
             {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', '#', ' ', '#', '#', '#', '#', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', '#', '#', '#', ' ', ' ', ' ', '#', '#', ' ', '#'},
             {'#', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', '#'},
             {'#', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'}
        };
    }
}