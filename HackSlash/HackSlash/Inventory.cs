using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class Inventory
    {
        public List<Weapon> Weapons { get; private set; }
        public List<UsableItem> Items { get; private set; }

        // Add a weapon to the players inventory
        public void AddWeapon(Weapon weapon)
        {
            Weapons.Add(weapon);
        }

        // Add a consumable item to the players inventory
        public void AddItem(UsableItem item)
        {
            Items.Add(item);
        }

        // Check that all items have a valid amount(more than 0) and remove them if they dont
        public void VerifyItemCounts()
        {
            foreach(var item in Items)
            {
                if(item.Amount <= 0)
                {
                    RemoveItem(item.Name);
                }
            }
        }

        // Remove an item from the players inventory
        public void RemoveItem(string name)
        {
            UsableItem item = Items.Where(x => x.Name == name).First();
            if(item != null)
            {
                Items.Remove(item);
            }
        }

        public Inventory()
        {
            Weapons = new List<Weapon>();
            Items = new List<UsableItem>();
        }
    }
}
