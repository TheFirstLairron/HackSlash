using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class Weapon
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Strength { get; private set; }
        public bool Equipped { get; private set; }

        public Weapon(string name, string desc, int strength)
        {
            Name = name;
            Description = desc;
            Strength = strength;
            Equipped = true;
        }
    }
}
