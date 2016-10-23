﻿using System;
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

        public LevelTransition MovePlayer(Player player, Constants.DIRECTION dir)
        {
            LevelTransition newLevel = null;
            Tuple<int, int> playerLoc = player.GetCoords();
            char entity = Map[playerLoc.Item1, playerLoc.Item2];
            int xToCheck = playerLoc.Item1;
            int yToCheck = playerLoc.Item2;

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
                Map[playerLoc.Item1, playerLoc.Item2] = (char)Constants.MAP_CHARS.EMPTY;
                player.SetCoords(Tuple.Create(xToCheck, yToCheck));
            }
            else 
            {
                if(Map[xToCheck, yToCheck] == (char)Constants.MAP_CHARS.EXIT)
                {
                    newLevel = Exits.Where(x => x.ExitLocation.Item1 == xToCheck && x.ExitLocation.Item2 == yToCheck).FirstOrDefault();
                }
            }

            return newLevel;
        }

        public bool MoveEnemy(Enemy enemy, Player player)
        {
            bool moved = false;

            BreadthFirstSearch search = new BreadthFirstSearch();

            var graph = search.GenerateMap(Map, enemy.GetCoords(), player.GetCoords());

            var path = search.GatherPath(graph, player.GetCoords());

            if(path.Count > 1)
            {
                Tuple<int, int> nextMove = path.ElementAt(1);
                if (Map[nextMove.Item1, nextMove.Item2] == (char)Constants.MAP_CHARS.EMPTY)
                {
                    Tuple<int, int> lastLoc = enemy.GetCoords();
                    Map[nextMove.Item1, nextMove.Item2] = (char)Constants.MAP_CHARS.ENEMY;
                    Map[lastLoc.Item1, lastLoc.Item2] = (char)Constants.MAP_CHARS.ENEMY;
                    enemy.SetCoords(nextMove);
                }
                else if (player.GetCoords().Item1 == nextMove.Item1 && player.GetCoords().Item2 == nextMove.Item2)
                {
                    player.TakeDamage(enemy.GetDamage());
                }
            }

            return moved;
        }

        public void ResetCell(Tuple<int, int> cell)
        {
            Map[cell.Item1, cell.Item2] = (char)Constants.MAP_CHARS.EMPTY;
        }
    }
}