using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Snake
    {
        public Snake(int w, int h)
        {
            width = w;
            height = h;
            parts = new MyLinkedList<Part>();
            parts.Add(new Part(w / 2, h / 2, direction.none, true));
        }

        public bool checkCollision()
        {
            for (int i = 0; i < parts.Count; i++)
            {
                if (!parts[i].head)
                    if (parts[i].x == parts[0].x && parts[i].y == parts[0].y)
                        return true;
            }
            return false;
        }

        public bool checkCollision(Apple a)
        {
            if (parts[0].x == a.x && parts[0].y == a.y) return true;
            return false;
        }

        public bool containsPoint(int x, int y)
        {
            for (int i = 0; i < parts.Count; i++){
                if (parts[i].x == x && parts[i].y == y) return true;
            }
            return false;
        }

        public void addPart()
        {
            Part p = parts[parts.Count - 1];
            p.tail = false;
            parts.Add(new Part(p.x - Part.getDirDeltaX(p.dir), p.y - Part.getDirDeltaY(p.dir), p.dir, false, true));
        }

        public void changeDirection(direction d)
        {
            if (parts.Count > 1 && (
                Part.getDirDeltaX(d) == -Part.getDirDeltaX(parts[0].dir) && 
                Part.getDirDeltaY(d) == Part.getDirDeltaY(parts[0].dir) ||
                Part.getDirDeltaX(d) == Part.getDirDeltaX(parts[0].dir) &&
                Part.getDirDeltaY(d) == -Part.getDirDeltaY(parts[0].dir)))
                return;
            parts[0].dir = d;
        }

        public Part move()
        {
            Part old = parts[parts.Count - 1];
            direction currd = parts[0].dir;
            for (int i = 0; i < parts.Count; i++)
            {
                if (i == parts.Count - 1)
                {
                    Console.SetCursorPosition(parts[i].x, parts[i].y);
                    Console.Write(" ");
                    Console.SetCursorPosition(width - 2, height - 1);
                }
                direction tempd = parts[i].dir;
                parts[i].move();
                parts[i].dir = currd;
                currd = tempd;
            }
            return old;
        }

        public void render()
        {
            for (int i = 0; i < parts.Count; i++)
            {
                Part p = parts[i];
                Console.SetCursorPosition(p.x, p.y);
                if (p.head)
                {
                    Console.Write("@");
                }
                else if (p.tail)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write("0");
                }
            }
        }

        public bool isOffScreen()
        {
            if (parts[0].x >= width || parts[0].y >= height || parts[0].x < 0 || parts[0].y < 0)
                return true;
            return false;
        }


        int width, height;
        public MyLinkedList<Part> parts;

    }

    enum direction
    {
        none,
        up,
        down,
        left,
        right
    }

    class Part
    {
        public Part(int _x, int _y, direction d = direction.none, bool h = false, bool t = false)
        {
            x = _x;
            y = _y;
            dir = d;
            head = h;
            tail = t;
        }

        public static int getDirDeltaX(direction x)
        {
            switch (x)
            {
                case direction.left:
                    return -1;
                    
                case direction.right:
                    return 1;
                    
                default:
                    return 0;
                    
            }
        }

        public static int getDirDeltaY(direction y)
        {
            switch (y)
            {
                case direction.up:
                    return -1;

                case direction.down:
                    return 1;

                default:
                    return 0;

            }
        }

        public void move(int _x, int _y)
        {
            this.x += _x;
            this.y += _y;
        }

        public void move()
        {
            this.x += getDirDeltaX(dir);
            this.y += getDirDeltaY(dir);
        }

        public int x, y;
        public direction dir;
        public bool head, tail;
    }
}
