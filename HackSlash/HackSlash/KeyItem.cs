using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class KeyItem : Item
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public KeyItem(string name, string desc)
        {
            Name = name;
            Description = desc;
        }
    }
}
