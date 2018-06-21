using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    [Serializable]
    class Food:GameObject
    {

        public Food()
        {
            sign = '$';
            color = ConsoleColor.Green;
        }
        public Food(Point point)
        {
            sign = '$';
            color = ConsoleColor.Green;
            points.Add(point);
        }
        public void setPoint(Point point)
        {
            points[0] = point;
        }
    }
}
