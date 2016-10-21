using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public interface UsableItem
    {
        string Name { get; set; }
        string Description { get; set; }
        int Amount { get; set; }
        bool Kept { get; set; }
        Action<Player> Use { get; set; }
        void UseItem(Player player);
    }
}
