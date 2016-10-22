﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class Game
    {
        private Dictionary<string, List<Enemy>> Enemies { get; set; }
        private Dictionary<string, Map> Maps { get; set; }
        private Dictionary<string, Level> Levels { get; set; }
        private Dictionary<string, Weapon> Weapons { get; set; }
        private Dictionary<string, UsableItem> UsableItems { get; set; }
        private Player Player { get; set; }
        private bool Running { get; set; }
        private Constants Constants { get; set; }
        private Map CurrentMap { get; set; }
        private Level CurrentLevel { get; set; }

        public void Play()
        {
            Player.Inventory.AddWeapon(Weapons["TestingWeapon"]);
            Player.Inventory.AddWeapon(Weapons["Mega"]);
            Player.Inventory.AddItem(UsableItems["Testing Item"]);
            Player.Inventory.AddItem(UsableItems["Testing Item 2"]);

            do
            {
                DisplayUI();
                if (HandleInput(GetUserInput()))
                {

                    foreach (Enemy enemy in Enemies[CurrentMap.Name])
                    {
                        if (enemy.Alive)
                        {
                            enemy.Move(CurrentMap, Player, Enemies[CurrentMap.Name]);
                        }
                    }
                }

                if (!Player.Alive())
                {
                    Running = false;
                    DisplayGameOver();
                }  

            } while (Running);

        }

        public void AddLevel(string name, Level level)
        {
            Levels[name] = level;
        }

        public void TransitionToLevel(string name)
        {

        }

        public void AddMap(string name, Map map)
        {
            Maps[name] = map;
        }

        public void SetMap(string name)
        {
            CurrentMap = Maps[name];
            foreach(Enemy enemy in Enemies[CurrentMap.Name])
            {
                if (enemy.Alive)
                {
                    CurrentMap.SetEnemies(enemy.GetCoords());
                }
            }
        }

        public ConsoleKeyInfo GetUserInput()
        {
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            do
            {
                cki = Console.ReadKey(true);

            } while (!Constants.allowedKeys.Contains(cki.Key));
            return cki;
        }

        public bool HandleInput(ConsoleKeyInfo key)
        {
            bool ShouldEnemyMove = true;

            if (Constants.allowedKeys.Contains(key.Key))
            {
                switch (key.Key)
                {
                    case ConsoleKey.W:
                        Player.Move(CurrentMap, Constants.DIRECTION.NORTH);
                        break;
                    case ConsoleKey.A:
                        Player.Move(CurrentMap, Constants.DIRECTION.WEST);
                        break;
                    case ConsoleKey.S:
                        Player.Move(CurrentMap, Constants.DIRECTION.SOUTH);
                        break;
                    case ConsoleKey.D:
                        Player.Move(CurrentMap, Constants.DIRECTION.EAST);
                        break;
                    case ConsoleKey.Spacebar:
                        Player.Attack(CurrentMap, Enemies[CurrentMap.Name].Where(x => x.Alive).ToList());
                        break;
                    case ConsoleKey.Escape:
                        DisplayMenu();
                        ShouldEnemyMove = false;
                        break;
                }
            }

            return ShouldEnemyMove;
        }

        public void RegisterEnemy(string name, Enemy enemy)
        {
            if(!Enemies.Keys.Contains(name))
            {
                Enemies[name] = new List<Enemy>();
            }

            Enemies[name].Add(enemy);
        }

        public void RegisterWeapon(string name, Weapon weapon)
        {
            Weapons[name] = weapon;
        }

        public void RegisterItem(string name, UsableItem item)
        {
            UsableItems[name] = item;
        }

        public void DisplayGameOver()
        {
            Console.Clear();

            Console.WriteLine("GAME OVER!");
        }

        public void DisplayMap()
        {
            Console.Clear();
            for(int i = 0; i < CurrentMap.Board.GetLength(0); i++)
            {
                for(int j = 0; j < CurrentMap.Board.GetLength(1); j++)
                {
                    Console.Write(CurrentMap.Board[i, j]);
                }

                Console.WriteLine();
            }
        }

        public void DisplayStats()
        {
            Console.WriteLine("Health: " + this.Player.Health);
            Console.WriteLine("Attack: " + this.Player.GetDamage());
            if (Player.Weapon != null)
            {
                Console.WriteLine("Weapon: " + this.Player.Weapon.Name);
            }
            else
            {
                Console.WriteLine("Weapon: None");
            }
        }

        public void DisplayUI()
        {
            DisplayMap();
            DisplayStats();
        }

        public void DisplayMenu()
        {
            bool loop = true;

            while (loop)
            {
                Console.Clear();

                foreach (string item in Constants.mainMenuOptions)
                {
                    Console.WriteLine(item);
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.R:
                        loop = false;
                        break;
                    case ConsoleKey.I:
                        DisplayItems();
                        break;
                    case ConsoleKey.E:
                        DisplayEquipment();
                        break;
                    case ConsoleKey.Q:
                        loop = false;
                        this.Running = false;
                        break;
                }
            }
        }

        public void DisplayItems()
        {
            Console.Clear();
            if(Player.Inventory.Items.Count > 0)
            {
                Player.Inventory.VerifyItemCounts();

                int index = 0;
                UsableItem item = Player.Inventory.Items.ElementAt(index);
                bool loop = true;

                do
                {
                    Console.Clear();
                    Console.WriteLine("Item: " + item.Name);
                    Console.WriteLine("Description: " + item.Description);
                    Console.WriteLine("Amount: " + item.Amount);

                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.N:
                            index++;
                            if(index >= Player.Inventory.Items.Count)
                            {
                                index = 0;
                            }
                            break;
                        case ConsoleKey.P:
                            index--;
                            if (index < 0)
                            {
                                index = Player.Inventory.Items.Count - 1;
                            }
                            break;
                        case ConsoleKey.U:
                            item.UseItem(Player);
                            loop = false;
                            break;
                        case ConsoleKey.Enter:
                            item.UseItem(Player);
                            loop = false;
                            break;
                        case ConsoleKey.Q:
                            loop = false;
                            break;
                        default:
                            break;
                    }

                    item = Player.Inventory.Items.ElementAt(index);

                } while (loop);
            }
            else
            {
                Console.WriteLine("You don't have any items in your bag...");
                Console.ReadKey(true);
            }
        }

        public void DisplayEquipment()
        {
            Console.Clear();
            if (Player.Inventory.Weapons.Count > 0)
            {
                int index = 0;
                Weapon weapon = Player.Inventory.Weapons.ElementAt(index);
                bool loop = true;

                do
                {
                    Console.Clear();
                    Console.WriteLine("Weapon: " + weapon.Name);
                    Console.WriteLine("Description: " + weapon.Description);
                    Console.WriteLine("Power: " + weapon.Strength);

                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.N:
                            index++;
                            if (index >= Player.Inventory.Weapons.Count)
                            {
                                index = 0;
                            }
                            break;
                        case ConsoleKey.P:
                            index--;
                            if (index < 0)
                            {
                                index = Player.Inventory.Weapons.Count - 1;
                            }
                            break;
                        case ConsoleKey.E:
                            Player.Equip(weapon);
                            loop = false;
                            break;
                        case ConsoleKey.Enter:
                            Player.Equip(weapon);
                            loop = false;
                            break;
                        case ConsoleKey.Q:
                            loop = false;
                            break;
                        default:
                            break;
                    }

                    weapon = Player.Inventory.Weapons.ElementAt(index);
                } while (loop);
            }
            else
            {
                Console.WriteLine("You don't have any weapons in your bag...");
                Console.ReadKey(true);
            }
        }

        public Game()
        {
            Enemies = new Dictionary<string, List<Enemy>>();
            Maps = new Dictionary<string, Map>();
            Constants = new Constants();
            Player = new Player(Constants.START_POINT.Item1, Constants.START_POINT.Item2);
            Weapons = new Dictionary<string, Weapon>();
            UsableItems = new Dictionary<string, UsableItem>();
            Running = true;
        }
    }
}
