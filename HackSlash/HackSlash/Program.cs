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

            Weapon testing = new Weapon("TestingWeapon", "A Testing Weapon", 5);
            game.RegisterWeapon(testing.Name, testing);

            Weapon mega = new Weapon("Mega", "The Mega Weapon", 500);
            game.RegisterWeapon(mega.Name, mega);

            Weapon boxTest = new Weapon("Boxish", "The boxiest of all boxes", 15);
            game.RegisterWeapon(boxTest.Name, boxTest);

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

            KeyItem kItem = new KeyItem("Testing Key", "A testing key item");

            KeyItem OpenTrappedEnemy = new KeyItem("Cage Key", "A key for the cage");

            #region Level1
            List<LevelTransition> levelOneExits = new List<LevelTransition>();
            levelOneExits.Add(new LevelTransition("First Level", "Second Level", Tuple.Create(0, 9), Tuple.Create(5, 1), false));

            List<Enemy> level1Enemies = new List<Enemy>();
            Enemy firstEnemy = new Enemy(10, 10, 0, 8, 7);
            firstEnemy.Reward = new ItemBox(kItem);

            level1Enemies.Add(firstEnemy);
            level1Enemies.Add(new Enemy(10, 10, 0, 8, 8));

            List<LevelModifier> level1Mods = new List<LevelModifier>();

            LevelModifier level1Mod = new LevelModifier("Check for key items", false, (mod, player, level) =>
            {
                if(player.Inventory.CheckIfKeyItemExists(kItem.Name))
                { 
                    LevelTransition exit = level.Exits.Where(x => x.LevelFrom == "First Level").FirstOrDefault();

                    if (exit != null)
                    {
                        exit.ShouldDisplay = true;
                        level.PlaceExits();
                        mod.Activated = true;
                    }
                }
            });

            level1Mods.Add(level1Mod);

            Level level1 = new Level("First Level", (char[,])Constants.firstMap.Clone(), levelOneExits, 
                enemies: level1Enemies, mods: level1Mods);
            #endregion

            #region level2
            List<LevelTransition> levelTwoExits = new List<LevelTransition>();
            levelTwoExits.Add(new LevelTransition("Second Level", "First Level", Tuple.Create(0, 9), Tuple.Create(5, 1)));

            List<Enemy> level2Enemies = new List<Enemy>();
            level2Enemies.Add(new Enemy(10, 5, 0, 9, 1));

            List<ItemBox> level2Items = new List<ItemBox>();
            level2Items.Add(new ItemBox(OpenTrappedEnemy, 9, 9));

            List<LevelModifier> level2Mods = new List<LevelModifier>();
            LevelModifier level2Mod = new LevelModifier("OpenCageDoor", false, (mod, player, level) =>
            {
                if(player.Inventory.CheckIfKeyItemExists(OpenTrappedEnemy.Name))
                {
                    level.ResetCell(Tuple.Create(7, 3));
                    mod.Activated = true;
                }
            });

            level2Mods.Add(level2Mod);

            Level level2 = new Level("Second Level", (char[,])Constants.secondMap.Clone(), levelTwoExits,
                enemies: level2Enemies, items: level2Items, mods: level2Mods);
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