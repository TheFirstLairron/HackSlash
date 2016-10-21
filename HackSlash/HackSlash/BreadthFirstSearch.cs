using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class BreadthFirstSearch
    {
        public BreadthFirstSearch() { }

        public Dictionary<Tuple<int, int>, Tuple<int, int>> GenerateMap(char[,] map, Tuple<int, int> start, Tuple<int, int> end)
        {
            Queue<Tuple<int, int>> frontier = new Queue<Tuple<int, int>>();

            Dictionary<Tuple<int, int>, Tuple<int, int>> cameFrom = new Dictionary<Tuple<int, int>, Tuple<int, int>>();

            Tuple<int, int> current;

            frontier.Enqueue(start);

            while (frontier.Count != 0)
            {
                current = frontier.Dequeue();

                if (current == end)
                {
                    break;
                }

                foreach (var item in this.GetNeighbors(current))
                {
                    if (!item.Equals(start))
                    {
                        if (!cameFrom.ContainsKey(item))
                        {
                            if (item.Item1 < map.GetLength(0) && item.Item1 >= 0 && item.Item2 < map.GetLength(1) && item.Item2 >= 0)
                            {
                                if (map[item.Item1, item.Item2] == ' ' || map[item.Item1, item.Item2] == '*' || map[item.Item1, item.Item2] == '@')
                                {
                                    frontier.Enqueue(item);
                                    cameFrom[item] = current;
                                }
                            }
                        }
                    }
                }
            }

            return cameFrom;
        }

        public List<Tuple<int, int>> GatherPath(Dictionary<Tuple<int, int>, Tuple<int, int>> cameFrom, Tuple<int, int> end)
        {
            List<Tuple<int, int>> path = new List<Tuple<int, int>>();

            Tuple<int, int> current = end;

            while (cameFrom.ContainsKey(current))
            {
                path.Add(cameFrom[current]);
                current = cameFrom[current];
            }

            path.Reverse();

            return path;
        }

        public List<Tuple<int, int>> GetNeighbors(Tuple<int, int> pos)
        {
            List<Tuple<int, int>> results = new List<Tuple<int, int>>(4);

            results.Add(new Tuple<int, int>(pos.Item1 + 1, pos.Item2));
            results.Add(new Tuple<int, int>(pos.Item1 - 1, pos.Item2));
            results.Add(new Tuple<int, int>(pos.Item1, pos.Item2 + 1));
            results.Add(new Tuple<int, int>(pos.Item1, pos.Item2 - 1));

            return results;
        }
    }
}
