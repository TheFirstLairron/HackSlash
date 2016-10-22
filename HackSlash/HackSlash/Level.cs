using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class Level
    {
        public string Name { get; private set; }
        public Char[,] Map { get; private set; }
        public LevelTransition[] Exits { get; set; }

        public LevelTransition MoveEntity(Tuple<int, int> initialPos, Constants.DIRECTION dir, bool isPlayer = false)
        {
            LevelTransition newLevel = null;
            char entity = Map[initialPos.Item1, initialPos.Item2];
            int xToCheck = initialPos.Item1;
            int yToCheck = initialPos.Item2;

            switch (dir)
            {
                case Constants.DIRECTION.NORTH:
                    xToCheck--;
                    break;

                case Constants.DIRECTION.EAST:
                    yToCheck++;
                    break;

                case Constants.DIRECTION.SOUTH:
                    xToCheck++;
                    break;

                case Constants.DIRECTION.WEST:
                    yToCheck--;
                    break;
            }

            if(Map[xToCheck, yToCheck] == (char)Constants.MAP_CHARS.EMPTY)
            {
                Map[xToCheck, yToCheck] = entity;
                Map[initialPos.Item1, initialPos.Item2] = (char)Constants.MAP_CHARS.EMPTY;
            }
            else 
            {
                if(isPlayer && Map[xToCheck, yToCheck] == (char)Constants.MAP_CHARS.EXIT)
                {
                    newLevel = Exits.Where(x => x.ExitLocation.Item1 == xToCheck && x.ExitLocation.Item2 == yToCheck).FirstOrDefault();
                }
            }

            return newLevel;
        }
    }
}
