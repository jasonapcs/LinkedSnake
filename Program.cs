﻿using System;
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

            int appleX, appleY;

            Console.Title = "Snake";

        begin:;
            score = 0;
            Console.Clear();
            Snake snake = new Snake(width, height);

            timer.Start();
            nextTick = timer.ElapsedMilliseconds;

            do
            {
                appleX = rand.Next(0, width);
                appleY = rand.Next(0, height);
            } while (snake.containsPoint(appleX, appleY));
            apple = new Apple(appleX, appleY);

            while (running)
            {
                while (nextTick < timer.ElapsedMilliseconds)
                {
                    snake.move();
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

                            default:
                                break;
                        }
                    }

                    if (snake.checkCollision(apple))
                    {
                        snake.addPart();
                        score++;
                        do
                        {
                            appleX = rand.Next(0, width);
                            appleY = rand.Next(0, height);
                        } while (snake.containsPoint(appleX, appleY));

                        apple = new Apple(appleX, appleY);
                    }

                    Console.Clear();
                    snake.render();
                    apple.render();

                    Console.SetCursorPosition(0, height - 1);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Score: " + score);
                    Console.ResetColor();
                    Console.SetCursorPosition(width - 2, height - 1);

                    nextTick += 100;
                    
                }

            }

            timer.Stop();

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