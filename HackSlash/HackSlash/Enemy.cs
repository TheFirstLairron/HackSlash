using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class Enemy
    {
        public int Health { get; private set; }
        private int Defense { get; set; }
        private int XCoord { get; set; }
        private int YCoord { get; set; }
        public bool Alive { get; set; }

        public void TakeDamage(int amount, Map map)
        {
            int trueDamage = 0;

            if(amount - Defense >= 0)
            {
                trueDamage = amount - Defense;
            }

            Health -= trueDamage;

            if(Health <= 0)
            {
                Kill(map);
            }
        }

        public void Kill(Map map)
        {
            map.ResetCell(GetCoords());
            Alive = false;
        }

        public Tuple<int, int> GetCoords()
        {
            return Tuple.Create(XCoord, YCoord);
        }

        public void SetCoords(Tuple<int, int> coords)
        {
            XCoord = coords.Item1;
            YCoord = coords.Item2;
        }

        public void Move(Map map, Player player, List<Enemy> enemies)
        {
            BreadthFirstSearch bfs = new BreadthFirstSearch();

            Tuple<int, int> selfCoords = this.GetCoords();
            Tuple<int, int> nextMove = null;

            var graph = bfs.GenerateMap(map.Board, selfCoords, player.GetCoords());

            var path = bfs.GatherPath(graph, player.GetCoords());

            if(path.Count > 1)
            {
                nextMove = path.ElementAt(1);
                if (CanMove(nextMove, map))
                {
                    this.SetCoords(nextMove);
                    map.Board[selfCoords.Item1, selfCoords.Item2] = ' ';
                    map.Board[nextMove.Item1, nextMove.Item2] = '*';
                }
            }
            else if(path.Count == 1)
            {
                player.TakeDamage(10);
            }


        }

        bool CanMove(Tuple<int, int> pos, Map map)
        {
            bool canMove = true;
            if (map.Board[pos.Item1, pos.Item2] != ' ')
            {
                canMove = false;
            }

            return canMove;
        }

        public Enemy(int health = 10, int defense = 10, int x = 0, int y = 0)
        {
            Health = health;
            Defense = defense;
            XCoord = x;
            YCoord = y;
            Alive = true;
        }
    }
}
