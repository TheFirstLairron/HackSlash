using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class Enemy
    {
        public int Health { get; private set; }
        private int Attack { get; set; }
        private int Defense { get; set; }
        private int XCoord { get; set; }
        private int YCoord { get; set; }
        public bool Alive { get; set; }

        public void TakeDamage(int amount, Level level)
        {
            int trueDamage = 0;

            if(amount - Defense >= 0)
            {
                trueDamage = amount - Defense;
            }

            Health -= trueDamage;

            if(Health <= 0)
            {
                Kill(level);
            }
        }

        public int GetDamage()
        {
            return Attack;
        }

        public void Kill(Level level)
        {
            level.ResetCell(GetCoords());
            Alive = false;
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

        public Enemy(int health = 10, int attack = 5, int defense = 10, int x = 0, int y = 0)
        {
            Health = health;
            Attack = attack;
            Defense = defense;
            XCoord = x;
            YCoord = y;
            if (Health > 1)
            {
                Alive = true;
            }
            else
            {
                Alive = false;
            }
        }

    }
}
