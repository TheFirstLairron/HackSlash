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
            Game game = new Game();
            game.RegisterWeapon("TestingWeapon", new Weapon("TestingWeapon", "A Testing Weapon", 5));
            game.RegisterWeapon("Mega", new Weapon("Mega", "The Mega Weapon", 500));

            HealingItem item = new HealingItem(1, "An Item that heals 10 health", false, "Testing Item", (Player) =>
            {
                Player.Heal(10);
            });

            HealingItem item2 = new HealingItem(1, "An Item that heals 5 health", false, "Testing Item 2", (Player) =>
            {
                Player.Heal(5);
            });

            HealingItem item3 = new HealingItem(1, "An Item that heals stuff", false, "Testing Item 2", (Player) =>
            {
                Player.Heal(5);
            });

            #region Level1
            List<LevelTransition> levelOneExits = new List<LevelTransition>();
            levelOneExits.Add(new LevelTransition("First Level", "Second Level", Tuple.Create(0, 9), Tuple.Create(5, 1)));

            List<Enemy> level1Enemies = new List<Enemy>();
            level1Enemies.Add(new Enemy(0, 10, 0, 9, 9));
            level1Enemies.Add(new Enemy(10, 10, 0, 8, 8));

            List<ItemBox> items = new List<ItemBox>();
            items.Add(new ItemBox(item3, 7, 1));

            Level level1 = new Level("First Level", (char[,])Constants.firstMap.Clone(), levelOneExits, level1Enemies, items);
            #endregion

            #region level2
            List<LevelTransition> levelTwoExits = new List<LevelTransition>();
            levelTwoExits.Add(new LevelTransition("Second Level", "First Level", Tuple.Create(0, 9), Tuple.Create(5, 1)));

            List<Enemy> level2Enemies = new List<Enemy>();
            level2Enemies.Add(new Enemy(10, 5, 0, 9, 1));

            Level level2 = new Level("Second Level", (char[,])Constants.secondMap.Clone(), levelTwoExits, level2Enemies);
            #endregion

            game.RegisterItem(item.Name, item);
            game.RegisterItem(item2.Name, item2);

            game.AddLevel(level1.Name, level1);
            game.AddLevel(level2.Name, level2);

            game.TransitionToLevel(new LevelTransition("none", "First Level", null, Tuple.Create(5, 1)));

            game.Play();
        }
    }
}