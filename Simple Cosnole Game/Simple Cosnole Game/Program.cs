using System;
using System.Threading;

namespace Simple_Console_Game
{
    static class Loading
    {
        public static void Load()
        {
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(275);
                Console.Write(". ");
                Thread.Sleep(275);
            }
        }
    }

    class Character
    {
        private string name;
        private int health;

        public int GetHealth()
        {
            return this.health;
        }


        public Character(string name, int health)
        {
            this.name = name;
            this.health = health;
        }

        public void Attack(Character target)
        {
            var rndDamage = new Random();
            int attack = rndDamage.Next(5, 16);
            target.health -= attack;
            Console.WriteLine($"\n\t\t{name} attacked {target.name} for {attack} damage");
            Loading.Load();
        }
        public void Heal(Character target)
        {
            var rndHealth = new Random();
            int healed = rndHealth.Next(0, 20);
            this.health += healed;
            Console.WriteLine($"\n\t\t{name} healed and gained {healed} health");
            Loading.Load();
        }

        public bool IsAlive()
        {
            return health > 0;
        }
        public void ShowHealth()
        {
            Console.WriteLine("\t\tName: " + name);
            Console.WriteLine("\t\tHealth: " + health);
        }
    }

    internal class Program
    {
        static void GameMenu()
        {

            Console.WriteLine("\t\tWelcome to the game!");

            Console.WriteLine("\t\tPress Enter to start");

            var inputKey = Console.ReadKey().Key;

            switch (inputKey)
            {
                case ConsoleKey.Enter:
                    Console.Clear();
                    Loading.Load();
                    break;
                default:
                    Console.Clear();
                    GameMenu();
                    break;
            }
        }

        static public void Main()
        {
            GameMenu();

            var player = new Character("Player", 100);

            var enemy = new Character("Enemy", 100);

        repeatGame:

            Console.Clear();

            Console.WriteLine();
            player.ShowHealth();
            Console.WriteLine();
            enemy.ShowHealth();

            Console.WriteLine("\nChoose your action");

            Console.WriteLine($"1. Attack (5-15)\n2. Heal (0-20)");

            var inputKey = Console.ReadKey().Key;

            switch (inputKey)
            {
                case ConsoleKey.D1:

                    Console.Clear();

                    player.Attack(enemy);

                    break;

                case ConsoleKey.D2:

                    Console.Clear();

                    player.Heal(player);

                    break;

                default:

                    Console.Clear();

                    Console.WriteLine("Invalid input");

                    Thread.Sleep(1500);

                    goto repeatGame;
            }

            Console.Clear();

            int enemyHealth = enemy.GetHealth();
            if (enemyHealth <= 0)
            {
                Console.WriteLine("\n\t\tPlayer wins");
                Console.WriteLine();
                return;
            }
            else
            {
                Console.WriteLine("\n\t\tEnemy's turn");

                Loading.Load();

                Console.Clear();

                var enemyChoise = new Random().Next(1, 3);

                if (enemyChoise == 1)
                {
                    enemy.Attack(player);
                }
                else
                {
                    enemy.Heal(enemy);
                }
            }

            if (player.IsAlive() && enemy.IsAlive()) // if both are alive
            {
                goto repeatGame;
            }
            else
            {
                Console.Clear();
                Console.WriteLine(player.IsAlive() ? "\n\t\tPlayer wins" : "\n\t\tEnemy wins");
            }
        }
    }
}
