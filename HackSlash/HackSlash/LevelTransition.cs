using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class LevelTransition
    {
        public string LevelFrom { get; set; }
        public string LevelTo { get; set; }
        public Tuple<int, int> ExitLocation { get; set; }
        public Tuple<int, int> NewLocation { get; set; }
    }
}
