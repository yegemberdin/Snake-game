using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    [Serializable]
    public abstract class GameObject
    {
        public char sign;
        public ConsoleColor color;
        public List <Point> points = new List<Point>();

        public void Draw()
        {
            Console.ForegroundColor = color;
            for (int i = 0; i < points.Count; i++)
            {
                Console.SetCursorPosition(points[i].x, points[i].y);
                Console.Write(sign);
            }
        }

        public void Clear()
        {
            
            for (int i = 0; i < points.Count; i++)
            {
                Console.SetCursorPosition(points[i].x, points[i].y);
                Console.Write(' ');
            }
        }
        public bool intersect(GameObject obj)
        {
            for (int i = 0; i < obj.points.Count; i++)
            {
                for (int j = 0; j < points.Count; j++)
                {
                    if (points[j].Equals(obj.points[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool intersect(Point P)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Equals(P)) {
                    return true;
                }
            }
            return false;
        }

    }
}
