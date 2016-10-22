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
    }
}
