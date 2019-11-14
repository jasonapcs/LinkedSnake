using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Apple
    {
        public Apple(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public void render()
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("A");
            Console.ResetColor();
        }
        public int x, y;

    }
}
