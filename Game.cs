using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    class Game
    {
        public static int HEIGHT = 40;
        public static int WIDTH = 80;
        public static Snake snake;
        public static Food food;
        public static Wall wall;
        public static int score, level;
        public static Thread t;
        public static bool alive;
        public static int curscore;

        public static Random rnd;
         
        static void Main(string[] args)
        {

            Console.SetBufferSize(WIDTH, HEIGHT + 1);
            Console.SetWindowSize(WIDTH, HEIGHT + 1);
 
            int ptr = 0;
            
            while (true) {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(WIDTH / 2, HEIGHT / 2);
                Console.WriteLine("SNAKE");


                Console.SetCursorPosition(WIDTH / 2, HEIGHT / 2 + 1);
                Console.ForegroundColor = ConsoleColor.White; if (ptr == 0) Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("1.Play game");

                Console.SetCursorPosition(WIDTH / 2, HEIGHT / 2 + 2);
                Console.ForegroundColor = ConsoleColor.White; if (ptr == 1) Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("2.Load game");

                Console.SetCursorPosition(WIDTH / 2, HEIGHT / 2 + 3);
                Console.ForegroundColor = ConsoleColor.White; if (ptr == 2) Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("3.Settings");

                Console.SetCursorPosition(WIDTH / 2, HEIGHT / 2 + 4);
                Console.ForegroundColor = ConsoleColor.White; if (ptr == 3) Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("4.Exit");

                ConsoleKeyInfo pressedButtin = Console.ReadKey();
                switch (pressedButtin.Key)
                {
                    case ConsoleKey.UpArrow:
                        ptr = (ptr - 1 + 4) % 4;
                        break;
                    case ConsoleKey.DownArrow:
                        ptr = (ptr + 1 + 4) % 4;
                        break;
                    case ConsoleKey.Enter:

                        Console.Clear();
                        if (ptr == 0)
                        {
                            PlayGame(true);

                        }
                        else if (ptr == 1)
                        {

                            LoadGame();
                            


                        }
                        else if (ptr == 2)
                        {
                            Settings();
                        }
                        if (ptr == 3) {
                            return;
                        }
                        break;
                }
            }

            
           

        }

        private static void LoadGame()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(WIDTH / 2, HEIGHT / 2);
            Console.WriteLine("Enter name:");

            Console.SetCursorPosition(WIDTH / 2, HEIGHT / 2 + 1);
            string name = Console.ReadLine();
            Deserializeation(name);
            
            PlayGame(false);
            return;
            

        }

        static void PlayGame(bool isnew)
        {

            Console.SetBufferSize(WIDTH, HEIGHT + 1);
            Console.SetWindowSize(WIDTH, HEIGHT + 1);
            rnd = new Random();
            curscore = 0;
            if (isnew) { 
                snake = new Snake();
                wall = new Wall();
                food = new Food(getEmptyCell());
               
                for (int i = 1; i <= 10; i++)
                {
                    wall.points.Add(getEmptyCell());
                }

                snake.generate();

                score = 0;
                level = 1;
            }
           

            alive = true;

                Console.CursorVisible = false;
                t = new Thread(Draw);
                t.Start();
                
                while (alive)
                {
                    ConsoleKeyInfo pressedButton = Console.ReadKey();

                    switch (pressedButton.Key)
                    {
                        case ConsoleKey.UpArrow:
                            snake.changeDirection(Snake.UP);
                            break;
                        case ConsoleKey.DownArrow:
                            snake.changeDirection(Snake.DOWN);
                            break;
                        case ConsoleKey.RightArrow:
                            snake.changeDirection(Snake.RIGHT);
                            break;
                        case ConsoleKey.LeftArrow:
                            snake.changeDirection(Snake.LEFT);
                            break;
                      case ConsoleKey.Escape:
                            pause();

                            break;

                    }


                }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(WIDTH / 2, HEIGHT / 2);
            Console.WriteLine("Game over");

            Console.SetCursorPosition(WIDTH / 2 - 2, HEIGHT / 2 + 1);
            Console.Write("Your score: {0}", score);
            Console.ReadKey();
            

        }
        static void pause()
        {
            t.Abort();

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(WIDTH / 2, HEIGHT / 2);
            Console.Write("Paused");
            Console.SetCursorPosition(WIDTH / 2, HEIGHT / 2 + 1);
            Console.Write("Press S to save");


            ConsoleKeyInfo pressedButton = Console.ReadKey();
            if (pressedButton.Key == ConsoleKey.S)
            {
                Save();

            } else if (pressedButton.Key == ConsoleKey.Q) {
                alive = false;
            }
            t = new Thread(Draw);
            t.Start();
        }
        static void Settings() {
            

        }
        static void Serializeation(string name)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(name + ".wall", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            bf.Serialize(fs, wall);
            fs.Close();
            fs = new FileStream(name + ".snake", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            bf.Serialize(fs, snake);
            fs.Close();
            fs = new FileStream(name + ".food", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            bf.Serialize(fs, food);
            fs.Close();
            Point tmp_point = new Point(score, level);
            fs = new FileStream(name + ".score", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            bf.Serialize(fs, tmp_point);
            fs.Close();
        }

        static void Deserializeation(string name)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(name + ".wall", FileMode.Open, FileAccess.Read);
            Wall tmp_wall = bf.Deserialize(fs) as Wall;
            fs.Close();
            fs = new FileStream(name + ".snake", FileMode.Open, FileAccess.Read);
            Snake tmp_snake =  bf.Deserialize(fs) as Snake;
            fs.Close();

            fs = new FileStream(name + ".food", FileMode.Open, FileAccess.Read);
            Food tmp_food = bf.Deserialize(fs) as Food;
            fs.Close();
            food = tmp_food;
            wall = tmp_wall;
            snake = tmp_snake;

            fs = new FileStream(name + ".score", FileMode.Open, FileAccess.Read);
            Point tmp_point = bf.Deserialize(fs) as Point;
            score = tmp_point.x;
            level = tmp_point.y;
            fs.Close();
           // Console.WriteLine(score + " " + level);
          //  Console.ReadKey();

        }
        static void Save()
        {
            Console.SetCursorPosition(WIDTH / 2, HEIGHT / 2 + 2);
            Console.Write("Enter name:");
            string name = Console.ReadLine();
            Serializeation(name);


        }

        static void Draw()
        {
            Console.Clear();
            
            snake.Draw();
            food.Draw();
            wall.Draw();
            while (alive)
            {
                Thread.Sleep(250);
                snake.Clear();
                
                snake.Move();
                if (snake.intersect((GameObject)food)) {
                    snake.upgrade();
                    food.Clear();
                    food.setPoint(getEmptyCell());
                    food.Draw();
                    score++;
                    curscore++;
                }
                if (wall.intersect(snake.points[0])) {
                   
                    alive = false;
                    break;       
                }


                snake.Draw();
              
                Console.SetCursorPosition(0, HEIGHT);
                Console.Write("Score: {0}   Level: {1}", score, level);
                if (curscore == 5)
                {
                    curscore = 0;
                    level++;
                    Console.Clear();
                    wall = new Wall();
                    for (int i = 1; i <= 10 * (level + 1); i++) {
                        wall.points.Add(getEmptyCell());
                    }

                    snake.Draw();
                    food.Draw();
                    wall.Draw();

                    Thread.Sleep(1000);

                }

            }
        }

        private static Point getEmptyCell()
        {

           
            while (true)
            {

                Point cur_point = new Point(rnd.Next(0, WIDTH), rnd.Next(0, HEIGHT));
                if (snake.intersect(cur_point) == false && wall.intersect(cur_point) == false) {
                    return cur_point;
                }
            }
        }
    }
}
