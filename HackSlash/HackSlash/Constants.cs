using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class Constants
    {
        public static readonly ConsoleKey[] allowedKeys = new ConsoleKey[] { ConsoleKey.A, ConsoleKey.W, ConsoleKey.S, ConsoleKey.D, ConsoleKey.Escape, ConsoleKey.Spacebar };

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

        public static readonly List<string> mainMenuOptions = new List<string>() { "Main Menu: ", "   -Resume", "   -Inventory", "   -Equipment", "   -Quit" };
    
        public static readonly Tuple<int, int> START_POINT = new Tuple<int, int>(5, 1);

        public static readonly char WALL = '#';

        public const string FIRST_MAP_NAME = "First Level";

        public static char[,] firstMap = new char[,]
        {
             {'#', '#', '#', '#', '#', '#', '#', '#', '#', '0', '#'},
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
             {'#', '#', '#', '#', '#', '#', '#', '#', '#', '0', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', ' ', '#', ' ', '#', '#', '#', '#', '#', ' ', '#'},
             {'#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
             {'#', '#', '#', ' ', ' ', ' ', ' ', '#', '#', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#'},
             {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
             {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'}
        };
    }
}