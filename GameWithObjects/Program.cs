using System.Collections.Generic;

namespace GameWithObjects
{
    internal class Program
    {
        static Player player;
        //static Enemy enemy;
        static List<Enemy> enemies;

        static Random random = new Random();
        static bool isRunning = true;
        static DateTime lastUpdateTime = DateTime.Now;
        static double deltaTime;
        static double gameTimeElapsed;

        static int windowWidth = 40;
        static int windowHeight = 20;
        private static int numberOfEnemies = 5;



        static void Main(string[] args)
        {
            Console.BufferHeight = Console.WindowHeight * 2;
            Console.CursorVisible = false;

            player = new Player(0, 0, "@", 100);

            //enemy = new Enemy(10, 10, "X", 4);

            enemies = new List<Enemy>();
            for (int i = 0; i < numberOfEnemies; i++)
            {
                int speed = random.Next(2, 7);
                int x = random.Next(0, windowWidth);
                int y = random.Next(0, windowHeight);
                enemies.Add(new Enemy(x, y, "X", speed));
            }

            while (isRunning)
            {
                // Update the time
                DateTime currentTime = DateTime.Now;
                deltaTime = (currentTime - lastUpdateTime).TotalMilliseconds;
                lastUpdateTime = currentTime;
                gameTimeElapsed += deltaTime;
                Update();
                Draw();
                Thread.Sleep(100); // 100 milliseconds delay
            }
        }

 

        static void Update()
        {
            // Handle input
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Q:
                        isRunning = false;
                        break;

                    case ConsoleKey.UpArrow:
                        player.Move(0, -1, windowWidth, windowHeight);
                        break;
                    case ConsoleKey.DownArrow:
                        player.Move(0, 1, windowWidth, windowHeight);
                        break;
                    case ConsoleKey.LeftArrow:
                        player.Move(-1, 0, windowWidth, windowHeight);
                        break;
                    case ConsoleKey.RightArrow:
                        player.Move(1, 0, windowWidth, windowHeight);
                        break;                     
                    default:
                        break;
                }
            }
            foreach(Enemy enemy in enemies)
            {
                enemy.Update(player, windowWidth, windowHeight);
                player.CheckCollisionWith(enemy);
            }
            if (player.HP < 1)
            {
                Console.WriteLine("Game Over!");
                
                isRunning = false;
            }

        }
        static void Draw()
        {
            Console.Clear();

            player.Draw();
            foreach(Enemy enemy in enemies)
            {
                enemy.Draw();
            }
            

            Console.SetCursorPosition(0, 21);
            Console.WriteLine($"Time elapsed (ms): {Math.Round(gameTimeElapsed /1000)}");
            Console.WriteLine($"Player HP: {player.HP}");
        }
       }

    }