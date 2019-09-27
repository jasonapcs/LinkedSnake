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
            Console.Write("A");
        }
        public int x, y;

    }
}
