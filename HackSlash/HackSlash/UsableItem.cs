using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public interface UsableItem : Item
    {
        int Amount { get; set; }
        bool Kept { get; set; }
        Action<Player> Use { get; set; }
        void UseItem(Player player);
    }
}
