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

            Weapon rustyScythe = new Weapon(Constants.RustyScytheName, Constants.RustyScytheDesc, Constants.RustyScytheDamage);
            game.RegisterWeapon(rustyScythe.Name, rustyScythe);

            Weapon mega = new Weapon(Constants.MegaScytheName, Constants.MegaScytheDesc, Constants.MegaScytheDamage);
            game.RegisterWeapon(mega.Name, mega);

            Weapon boxTest = new Weapon(Constants.BoxScytheName, Constants.BoxScytheDesc, Constants.BoxScytheDamage);
            game.RegisterWeapon(boxTest.Name, boxTest);

            HealingItem basicPotion = new HealingItem(1, Constants.BasicPotionDesc, false, Constants.BasicPotionName, (Player) =>
            {
                Player.Heal(Constants.BasicPotionHeal);
            });
            game.RegisterItem(basicPotion.Name, basicPotion);

            HealingItem weakenedPotion = new HealingItem(6, Constants.WeakenedPotionDesc, false, Constants.WeakenedPotionName, (Player) =>
            {
                Player.Heal(Constants.WeakenedPotionHeal);
            });
            game.RegisterItem(weakenedPotion.Name, weakenedPotion);

            HealingItem superPotion = new HealingItem(1, Constants.SuperPotionDesc, false, Constants.SuperPotionName, (Player) =>
            {
                Player.Heal(Constants.SuperPotionHeal);
            });
            game.RegisterItem(superPotion.Name, superPotion);

            KeyItem EntrywayDoorKey = new KeyItem(Constants.EntrywayKeyName, Constants.EntrywayKeyDesc);
            game.RegisterKeyItem(EntrywayDoorKey.Name, EntrywayDoorKey);

            KeyItem JaildoorKey = new KeyItem(Constants.JaildoorKeyName, Constants.JaildoorKeyDesc);
            game.RegisterKeyItem(JaildoorKey.Name, JaildoorKey);

            #region Level1
            List<LevelTransition> levelOneExits = new List<LevelTransition>();
            levelOneExits.Add(new LevelTransition(Constants.Level1Name, Constants.Level2Name, Tuple.Create(0, 5), Tuple.Create(1, 5), false));

            List<Enemy> level1Enemies = new List<Enemy>();
            level1Enemies.Add(new Enemy(10, 10, 0, 1, 1));
            level1Enemies.Add(new Enemy(10, 10, 0, 1, 9));

            List<LevelModifier> level1Mods = new List<LevelModifier>();

            LevelModifier level1Mod = new LevelModifier("Clear All Enemies", false, (mod, player, level) =>
            {
                if(level.Enemies.Where(x => x.Alive).ToList().Count <= 0)
                { 
                    LevelTransition exit = level.Exits.Where(x => x.LevelFrom == Constants.Level1Name).FirstOrDefault();

                    if (exit != null)
                    {
                        exit.ShouldDisplay = true;
                        level.PlaceExits();
                        mod.Activated = true;
                    }
                }
            });

            level1Mods.Add(level1Mod);

            Level level1 = new Level(Constants.Level1Name, (char[,])Constants.firstMap.Clone(), levelOneExits, 
                enemies: level1Enemies, mods: level1Mods);

            game.AddLevel(level1.Name, level1);

            #endregion

            #region level2
            List<LevelTransition> level2Exits = new List<LevelTransition>();
            level2Exits.Add(new LevelTransition(Constants.Level2Name, Constants.Level1Name, Tuple.Create(0, 5), Tuple.Create(1, 5)));
            level2Exits.Add(new LevelTransition(Constants.Level2Name, Constants.Level3Name, Tuple.Create(10, 1), Tuple.Create(1, 5)));

            List<Enemy> level2Enemies = new List<Enemy>();
            level2Enemies.Add(new Enemy(10, 5, 0, 9, 1));

            List<ItemBox> level2Items = new List<ItemBox>();
            level2Items.Add(new ItemBox(JaildoorKey, 1, 9));

            List<LevelModifier> level2Mods = new List<LevelModifier>();
            LevelModifier level2Mod = new LevelModifier("OpenCageDoor", false, (mod, player, level) =>
            {
                if(player.Inventory.CheckIfKeyItemExists(Constants.JaildoorKeyName))
                {
                    level.ResetCell(Tuple.Create(7, 3));
                    mod.Activated = true;
                }
            });

            level2Mods.Add(level2Mod);

            Level level2 = new Level(Constants.Level2Name, (char[,])Constants.secondMap.Clone(), level2Exits,
                enemies: level2Enemies, items: level2Items, mods: level2Mods);

            game.AddLevel(level2.Name, level2);

            #endregion

            #region level3
            List<LevelTransition> level3Exits = new List<LevelTransition>();
            level3Exits.Add(new LevelTransition(Constants.Level3Name, Constants.Level2Name, Tuple.Create(0, 5), Tuple.Create(9, 1)));
            level3Exits.Add(new LevelTransition(Constants.Level3Name, Constants.Level4Name, Tuple.Create(10, 5), Tuple.Create(1, 5)));

            List<Enemy> level3Enemies = new List<Enemy>();
            Enemy level3Enemy1 = new Enemy(10, 5, 5, 7, 3);
            level3Enemy1.Reward = new ItemBox(weakenedPotion);
            level3Enemies.Add(level3Enemy1);

            Enemy level3Enemy2 = new Enemy(10, 5, 5, 7, 7);
            level3Enemy2.Reward = new ItemBox(boxTest);
            level3Enemies.Add(level3Enemy2);

            Level level3 = new Level(Constants.Level3Name, (char[,])Constants.thirdMap.Clone(), level3Exits, level3Enemies);
            game.AddLevel(level3.Name, level3);
            #endregion

            #region level4
            List<LevelTransition> level4Exits = new List<LevelTransition>();
            level4Exits.Add(new LevelTransition(Constants.Level4Name, Constants.Level3Name, Tuple.Create(0, 5), Tuple.Create(9, 5)));

            List<Enemy> level4Enemies = new List<Enemy>();
            Enemy Boss = new Enemy(40, 15, 10, 9, 5);
            Boss.Reward = new ItemBox(mega);
            level4Enemies.Add(Boss);

            Level level4 = new Level(Constants.Level4Name, (char[,])Constants.fourthMap, level4Exits, level4Enemies);
            game.AddLevel(level4.Name, level4);
            #endregion


            game.TransitionToLevel(new LevelTransition("none", Constants.Level1Name, null, Tuple.Create(9, 5)));

            game.Play();
        }
    }
}