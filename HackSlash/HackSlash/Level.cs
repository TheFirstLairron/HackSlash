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
        public List<LevelTransition> Exits { get; set; }
        public List<Enemy> Enemies { get; set; }
        public List<ItemBox> ItemBoxs { get; set; }

        // Move the player, and return a new level if applicable
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

            if (Map[xToCheck, yToCheck] == (char)Constants.MAP_CHARS.EMPTY)
            {
                Map[xToCheck, yToCheck] = entity;
                Map[playerLoc.Item1, playerLoc.Item2] = (char)Constants.MAP_CHARS.EMPTY;
                player.SetCoords(Tuple.Create(xToCheck, yToCheck));
                MoveEnemies(player);
            }
            else if (Map[xToCheck, yToCheck] == (char)Constants.MAP_CHARS.EXIT)
            {
                newLevel = Exits.Where(x => x.ExitLocation.Item1 == xToCheck && x.ExitLocation.Item2 == yToCheck).FirstOrDefault();
                ResetCell(player.GetCoords());
            }
            else if(Map[xToCheck, yToCheck] == (char)Constants.MAP_CHARS.ITEMBOX)
            {
                ItemBox item = ItemBoxs.Where(x => x.XCoord == xToCheck && x.YCoord == yToCheck).First();

                if (item != null)
                {
                    player.ConsumeItemBox(item);
                    Map[playerLoc.Item1, playerLoc.Item2] = (char)Constants.MAP_CHARS.EMPTY;
                    Map[xToCheck, yToCheck] = (char)Constants.MAP_CHARS.CHARACTER;
                    player.SetCoords(Tuple.Create(xToCheck, yToCheck));
                    MoveEnemies(player);
                }
            }

            return newLevel;
        }

        // Move the enemies that are currently alive in the level
        public void MoveEnemies(Player player)
        {
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.Alive)
                {
                    if (AreNeighbors(player, enemy))
                    {
                        player.TakeDamage(enemy.GetDamage());
                    }
                    else
                    {
                        BreadthFirstSearch search = new BreadthFirstSearch();

                        var graph = search.GenerateMap(Map, enemy.GetCoords(), player.GetCoords());

                        var path = search.GatherPath(graph, player.GetCoords());

                        if (path.Count > 1)
                        {
                            Tuple<int, int> nextMove = path.ElementAt(1);
                            if (Map[nextMove.Item1, nextMove.Item2] == (char)Constants.MAP_CHARS.EMPTY)
                            {
                                Tuple<int, int> lastLoc = enemy.GetCoords();
                                Map[nextMove.Item1, nextMove.Item2] = (char)Constants.MAP_CHARS.ENEMY;
                                Map[lastLoc.Item1, lastLoc.Item2] = (char)Constants.MAP_CHARS.EMPTY;
                                enemy.SetCoords(nextMove);
                            }
                        }
                    }
                }
            }
        }

        // Place the player at a specific location
        public void PlacePlayer(Tuple<int, int> location, Player player)
        {
            Map[location.Item1, location.Item2] = (char)Constants.MAP_CHARS.CHARACTER;
            player.SetCoords(location);
        }

        // Place the enemies at their starting locations
        public void PlaceEnemies()
        {
            foreach(Enemy enemy in Enemies)
            {
                if (enemy.Alive)
                {
                    Map[enemy.GetCoords().Item1, enemy.GetCoords().Item2] = (char)Constants.MAP_CHARS.ENEMY;
                }
            }
        }

        // Place the exits at their proper location
        public void PlaceExits()
        {
            foreach(LevelTransition tran in Exits)
            {
                Map[tran.ExitLocation.Item1, tran.ExitLocation.Item2] = (char)Constants.MAP_CHARS.EXIT;
            }
        }

        // Place item boxes in their locations
        public void PlaceItemBoxes()
        {
            foreach(ItemBox item in ItemBoxs)
            {
                Map[item.XCoord, item.YCoord] = (char)Constants.MAP_CHARS.ITEMBOX;
            }
        }

        // Set a specific XY location to an empty cell
        public void ResetCell(Tuple<int, int> cell)
        {
            Map[cell.Item1, cell.Item2] = (char)Constants.MAP_CHARS.EMPTY;
        }

        // Determine if the player and an enemy are neighbors
        public bool AreNeighbors(Player player, Enemy enemy)
        {
            bool isNeighbor = false;

            Tuple<int, int> plLoc = player.GetCoords();
            Tuple<int, int> enLoc = enemy.GetCoords();

            if(plLoc.Item1 == enLoc.Item1 && plLoc.Item2 == enLoc.Item2 + 1)
            {
                isNeighbor = true;
            }
            else if(plLoc.Item1 == enLoc.Item1 && plLoc.Item2 == enLoc.Item2 - 1)
            {
                isNeighbor = true;
            }
            else if(plLoc.Item1 == enLoc.Item1 + 1 && plLoc.Item2 == enLoc.Item2)
            {
                isNeighbor = true;
            }
            else if(plLoc.Item1 == enLoc.Item1 - 1 && plLoc.Item2 == enLoc.Item2)
            {
                isNeighbor = true;
            }

            return isNeighbor;
        }

        public Level(string name, Char[,] map, List<LevelTransition> exits, List<Enemy> enemies = null, List<ItemBox> items = null)
        {
            Name = name;
            Map = map;
            Exits = exits;
            Enemies = enemies;
            ItemBoxs = items;

            if(Enemies == null)
            {
                Enemies = new List<Enemy>();
            }

            if(ItemBoxs == null)
            {
                ItemBoxs = new List<ItemBox>();
            }

            PlaceExits();
            PlaceEnemies();
            PlaceItemBoxes();
        }
    }
}
