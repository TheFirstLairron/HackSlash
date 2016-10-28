using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class Game
    {
        private Dictionary<string, Level> Levels { get; set; }
        private Dictionary<string, Weapon> Weapons { get; set; }
        private Dictionary<string, UsableItem> UsableItems { get; set; }
        private Dictionary<string, KeyItem> KeyItems { get; set; }
        private Player Player { get; set; }
        private bool Running { get; set; }
        private Constants Constants { get; set; }
        private Level CurrentLevel { get; set; }

        // This method will initiate play and manage the game logic
        public void Play()
        {
            // Adding weapons and usable items for testing
            Player.Inventory.AddWeapon(Weapons["TestingWeapon"]);
            Player.Inventory.AddWeapon(Weapons["Mega"]);
            Player.Inventory.AddItem(UsableItems["Testing Item"]);
            Player.Inventory.AddItem(UsableItems["Testing Item 2"]);

            // Main game loop
            do
            {
                DisplayUI();
                HandleInput(GetUserInput());

                if (!Player.Alive())
                {
                    Running = false;
                    DisplayGameOver();
                }  

            } while (Running);

        }

        // Register a level to be transitioned to
        public void AddLevel(string name, Level level)
        {
            Levels[name] = level;
        }

        // Move from one level to another
        public void TransitionToLevel(LevelTransition level)
        {
            CurrentLevel = Levels[level.LevelTo];
            CurrentLevel.PlacePlayer(level.NewLocation, Player);
        }

        // Gather input from the console
        public ConsoleKeyInfo GetUserInput()
        {
            ConsoleKeyInfo cki = new ConsoleKeyInfo();

            do
            {
                cki = Console.ReadKey(true);

            } while (!Constants.allowedKeys.Contains(cki.Key));

            return cki;
        }

        // Handle user input and transition to new level if needed
        public void HandleInput(ConsoleKeyInfo key)
        {
            LevelTransition newLevel = null;

            // Different user actions
            switch (key.Key)
            {
                case ConsoleKey.W:
                    newLevel = CurrentLevel.MovePlayer(Player, Constants.DIRECTION.NORTH);
                    break;
                case ConsoleKey.A:
                    newLevel = CurrentLevel.MovePlayer(Player, Constants.DIRECTION.WEST);
                    break;
                case ConsoleKey.S:
                    newLevel = CurrentLevel.MovePlayer(Player, Constants.DIRECTION.SOUTH);
                    break;
                case ConsoleKey.D:
                    newLevel = CurrentLevel.MovePlayer(Player, Constants.DIRECTION.EAST);
                    break;
                case ConsoleKey.Spacebar:
                    Player.Attack(CurrentLevel);
                    break;
                case ConsoleKey.Escape:
                    DisplayMenu();
                    break;
                default:
                    break;
            }

            if(newLevel != null)
            {
                TransitionToLevel(newLevel);
            }
        }

        // Register a weapon to be used by the player
        public void RegisterWeapon(string name, Weapon weapon)
        {
            Weapons[name] = weapon;
        }

        // Register a consumable item for the player
        public void RegisterItem(string name, UsableItem item)
        {
            UsableItems[name] = item;
        }

        #region UIMethods
        public void DisplayGameOver()
        {
            Console.Clear();

            Console.WriteLine("GAME OVER!");
        }

        public void DisplayMap()
        {
            Console.Clear();
            for(int i = 0; i < CurrentLevel.Map.GetLength(0); i++)
            {
                for(int j = 0; j < CurrentLevel.Map.GetLength(1); j++)
                {
                    Console.Write(CurrentLevel.Map[i, j]);
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
                    Console.WriteLine($"{index + 1} / {Player.Inventory.Items.Count}");
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

                    if (loop)
                    {
                        item = Player.Inventory.Items.ElementAt(index);
                    }

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
                    Console.WriteLine($"{index + 1} / {Player.Inventory.Weapons.Count}");
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

                    if (loop)
                    {
                        weapon = Player.Inventory.Weapons.ElementAt(index);
                    }

                } while (loop);
            }
            else
            {
                Console.WriteLine("You don't have any weapons in your bag...");
                Console.ReadKey(true);
            }
        }
        #endregion

        public Game()
        {
            Levels = new Dictionary<string, Level>();
            Constants = new Constants();
            Player = new Player();
            Weapons = new Dictionary<string, Weapon>();
            UsableItems = new Dictionary<string, UsableItem>();
            KeyItems = new Dictionary<string, KeyItem>();
            Running = true;
        }
    }
}
