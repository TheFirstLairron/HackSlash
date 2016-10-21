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

        public bool Alive()
        {
            return Health > 0;
        }

        public void TakeDamage(int potentialDamage)
        {
            int trueDamage = 0;

            if(potentialDamage - Defense > 0)
            {
                trueDamage = potentialDamage - Defense;
            }

            Health -= trueDamage;
        }

        public void Heal(int amount)
        {
            Health += amount;
        }

        public int GetDamage()
        {
            int damage = Damage;

            if (Weapon != null)
            {
                damage += Weapon.Strength;
            }

            return damage;
        }

        public void Equip(Weapon weapon)
        {
            Weapon = weapon;
        }

        public Tuple<int, int> GetCoords()
        {
            return Tuple.Create(XCoord, YCoord);
        }

        public void SetCoords(Tuple<int, int> coords)
        {
            XCoord = coords.Item1;
            YCoord = coords.Item2;
        }

        public void Move(Map map, Constants.DIRECTION dir)
        {
            Tuple<int, int> posToCheck = GetCoords();
            int xToCheck = posToCheck.Item1;
            int yToCheck = posToCheck.Item2;

            switch (dir)
            {
                case Constants.DIRECTION.NORTH:
                    xToCheck--;
                    break;

                case Constants.DIRECTION.EAST:
                    yToCheck++;
                    break;

                case Constants.DIRECTION.SOUTH:
                    xToCheck++;
                    break;

                case Constants.DIRECTION.WEST:
                    yToCheck--;
                    break;
            }

            if(map.Board[xToCheck, yToCheck] == ' ')
            {
                map.Board[posToCheck.Item1, posToCheck.Item2] = ' ';
                map.Board[xToCheck, yToCheck] = '@';
                this.SetCoords(new Tuple<int, int>(xToCheck, yToCheck));
            }
        }

        public void Attack(Map map, List<Enemy> enemies)
        {
            foreach(Enemy enemy in enemies)
            {
                if (isEnemyNeighbor(enemy))
                {
                    enemy.TakeDamage(GetDamage(), map);
                }
            }
        }

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

        public Player(int x, int y, int health = 30)
        {
            Health = health;
            Defense = 5;
            Damage = 5;
            XCoord = x;
            YCoord = y;
            Inventory = new Inventory();
        }
    }
}
