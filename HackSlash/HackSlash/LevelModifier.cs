using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class LevelModifier
    {
        public string Name { get; set; }
        public bool Activated { get; set; }
        public Action<LevelModifier, Player, Level> Modifier { get; set; }

        public LevelModifier(string name, bool active, Action<LevelModifier, Player, Level> mod)
        {
            Name = name;
            Activated = active;
            Modifier = mod;
        }
    }
}
