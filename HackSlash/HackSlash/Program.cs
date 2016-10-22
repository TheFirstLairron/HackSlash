using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    class Program
    {
        static void Main(string[] args)
        {
            Constants constants = new Constants();
            Game game = new Game();
            game.RegisterWeapon("TestingWeapon", new Weapon("TestingWeapon", "A Testing Weapon", 5));
            game.RegisterWeapon("Mega", new Weapon("Mega", "The Mega Weapon", 500));

            HealingItem item = new HealingItem(4, "An Item that heals 10 health", false, "Testing Item", (Player) =>
            {
                Player.Heal(10);
            });

            HealingItem item2 = new HealingItem(4, "An Item that heals 5 health", false, "Testing Item 2", (Player) =>
            {
                Player.Heal(5);
            });

            game.RegisterItem(item.Name, item);
            game.RegisterItem(item2.Name, item2);

            game.RegisterEnemy(Constants.FIRST_MAP_NAME, new Enemy(0, 0, 9, 9));

            game.RegisterEnemy(Constants.FIRST_MAP_NAME, new Enemy(10, 0, 8, 8));

            game.AddMap(Constants.FIRST_MAP_NAME, new Map(Constants.FIRST_MAP_NAME, constants.firstMap, Constants.START_POINT));

            game.SetMap(Constants.FIRST_MAP_NAME);

            game.Play();
        }
    }
}