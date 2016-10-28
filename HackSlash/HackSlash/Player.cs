using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class Player
    {
        public int Health { get; private set; }
        public Inventory Inventory { get; private set; }
        private int Damage { get; set; }
        private int Defense { get; set; }
        public Weapon Weapon { get; private set; }
        private int XCoord { get; set; }
        private int YCoord { get; set; }

        // Determine if the player is still alive
        public bool Alive()
        {
            return Health > 0;
        }

        // Damage the player
        public void TakeDamage(int potentialDamage)
        {
            int trueDamage = 0;

            trueDamage = Math.Max(potentialDamage - Defense, 0);

            Health -= trueDamage;
        }

        // Heal the player
        public void Heal(int amount)
        {
            Health += amount;
        }

        // Get the damage the player would deal, taking weapon into account
        public int GetDamage()
        {
            int damage = Damage;

            if (Weapon != null)
            {
                damage += Weapon.Strength;
            }

            return damage;
        }

        // Allow the player to equip a weapon
        public void Equip(Weapon weapon)
        {
            Weapon = weapon;
        }

        // Remove a key item from the inventory
        public void RemoveKeyItem(KeyItem item)
        {
            Inventory.RemoveKeyItem(item);
        }

        // Get the players XY coordinates
        public Tuple<int, int> GetCoords()
        {
            return Tuple.Create(XCoord, YCoord);
        }

        // Set the players XY coordinates
        public void SetCoords(Tuple<int, int> coords)
        {
            XCoord = coords.Item1;
            YCoord = coords.Item2;
        }

        // Attack adjacent enemies
        public void Attack(Level level)
        {
            foreach(Enemy enemy in level.Enemies)
            {
                if (isEnemyNeighbor(enemy))
                {
                    enemy.TakeDamage(GetDamage(), level);

                    if (!enemy.Alive)
                    {
                        if (enemy.Reward != null)
                        {
                            ConsumeItemBox(enemy.Reward);
                        }
                    }
                }
            }

            level.MoveEnemies(this);
        }

        // Detemine if an enemy is in range for attacking
        private bool isEnemyNeighbor(Enemy enemy)
        {
            bool isNeighbor = false;

            if (XCoord - enemy.GetCoords().Item1 == 1 && YCoord - enemy.GetCoords().Item2 == 0)
            {
                isNeighbor = true;
            }

            if (XCoord - enemy.GetCoords().Item1 == -1 && YCoord - enemy.GetCoords().Item2 == 0)
            {
                isNeighbor = true;
            }

            if (XCoord - enemy.GetCoords().Item1 == 0 && YCoord - enemy.GetCoords().Item2 == 1)
            {
                isNeighbor = true;
            }

            if (XCoord - enemy.GetCoords().Item1 == 0 && YCoord - enemy.GetCoords().Item2 == -1)
            {
                isNeighbor = true;
            }

            return isNeighbor;
        }

        // Process the contents of an item box
        public void ConsumeItemBox(ItemBox box)
        {
            if(box.Reward is UsableItem)
            {
                Inventory.AddItem(box.Reward as UsableItem);
            }
            else if(box.Reward is KeyItem)
            {
                Inventory.AddKeyItem(box.Reward as KeyItem);
            }
            else if(box.Reward is Weapon)
            {
                Inventory.AddWeapon(box.Reward as Weapon);
            }
        }

        public Player(int health = 30)
        {
            Health = health;
            Defense = 5;
            Damage = 5;
            Inventory = new Inventory();
        }
    }
}
