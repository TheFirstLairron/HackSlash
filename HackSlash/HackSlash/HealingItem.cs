using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    class HealingItem : UsableItem
    {
        public int Amount { get; set; }
        public string Description { get; set; }
        public bool Kept { get; set; }
        public string Name { get; set; }
        public Action<Player> Use { get; set; }

        // Use the item if there are remaining uses in it, and remove if there arent any remaining uses
        public void UseItem(Player player)
        {
            if(Amount > 0)
            {
                Use(player);

                if(!Kept)
                {
                    Amount--;
                    if(Amount <= 0)
                    {
                        player.Inventory.RemoveItem(Name);
                    }
                }
            }
        }

        public HealingItem(int amount, string desc, bool kept, string name, Action<Player> use)
        {
            Amount = amount;
            Description = desc;
            Kept = kept;
            Name = name;
            Use = use;
        }
    }
}
