using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class Map
    {
        public char[,] Board { get; private set; }
        public string Name { get; private set; }
        public Tuple<int, int> StartPosition { get; private set; }

        public void SetPlayer(Tuple<int, int> location)
        {
            Board[location.Item1, location.Item2] = '@';
        }

        public void SetEnemies(Tuple<int, int> enemy)
        {
            Board[enemy.Item1, enemy.Item2] = '*';
        }

        public void ResetCell(Tuple<int, int> cell)
        {
            Board[cell.Item1, cell.Item2] = ' ';
        }

        public Map(string name, char[,] board, Tuple<int, int> start)
        {
            Name = name;
            Board = board;
            StartPosition = start;

            SetPlayer(start);
        }
    }
}
