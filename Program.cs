using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Messaging;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            int height; int width;
            width = Console.WindowWidth;
            height = Console.WindowHeight;

            int score;
            long nextTick;
            Stopwatch timer = new Stopwatch();

            bool running = true;
            Apple apple;
            Random rand = new Random();
            Part OldPart;

            int appleX, appleY;

            begin:;
            Console.Clear();

            Console.Title = "Snake (Score: 0)";
            score = 0;
            Console.Clear();
            Snake snake = new Snake(width, height);

            timer.Start();
            nextTick = timer.ElapsedMilliseconds;

            do
            {
                appleX = rand.Next(1, width - 1);
                appleY = rand.Next(1, height - 1);
            } while (snake.containsPoint(appleX, appleY));
            apple = new Apple(appleX, appleY);

            while (running)
            {
                while (nextTick < timer.ElapsedMilliseconds)
                {
                    bool changeScore = false;
                    OldPart = snake.move();
                    if (snake.isOffScreen() || snake.checkCollision())
                    {
                        running = false;
                        Console.SetCursorPosition(0, 0);
                        Console.Write("You lose!");
                        break;
                    }

                    ConsoleKeyInfo key;
                    if (Console.KeyAvailable)
                    {
                        key = Console.ReadKey();
                        switch (key.Key)
                        {
                            case ConsoleKey.W:
                            case ConsoleKey.UpArrow:
                                snake.changeDirection(direction.up);
                                break;

                            case ConsoleKey.S:
                            case ConsoleKey.DownArrow:
                                snake.changeDirection(direction.down);
                                break;

                            case ConsoleKey.A:
                            case ConsoleKey.LeftArrow:
                                snake.changeDirection(direction.left);
                                break;

                            case ConsoleKey.D:
                            case ConsoleKey.RightArrow:
                                snake.changeDirection(direction.right);
                                break;

                            case ConsoleKey.P:
                                Console.ReadLine();
                                nextTick = timer.ElapsedMilliseconds;
                                Console.Clear();
                                break;

                            default:
                                break;
                        }
                    }

                    if (snake.checkCollision(apple))
                    {
                        snake.addPart();
                        score++;
                        changeScore = true;
                        Console.Title = "Snake (Score: " + score + ")";
                        do
                        {
                            appleX = rand.Next(1, width - 1);
                            appleY = rand.Next(1, height - 1);
                        } while (snake.containsPoint(appleX, appleY));

                        apple = new Apple(appleX, appleY);
                    }

                    snake.render();
                    apple.render();

                    Console.SetCursorPosition(width - 2, height - 1);

                    nextTick += (score < 15) ? 100 - score * 5 : 25;
                    
                }

            }

            timer.Stop();

            snake.render();
            apple.render();
            Console.SetCursorPosition(0, 0);
            Console.Write("Do you want to try again? (y/n) ");
            string s = Console.ReadLine();
            try
            {
                if (s != null && s.ToLower()[0] == 'y')
                {
                    running = true;
                    goto begin;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                ;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
