using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    [Serializable]
    public class Snake:GameObject
    {
        public const int UP = 0;
        public const int DOWN = 1;
        public const int LEFT = 3;
        public const int RIGHT = 4;
        public int direction;

           
        public Snake() {
            sign = '*';
            direction = 0;
            color = ConsoleColor.Red;
        }
        public void Move() {
            for (int i = this.points.Count - 1; i > 0; i--)
            {
                this.points[i].x = this.points[i - 1].x;
                this.points[i].y = this.points[i - 1].y;
            }
            switch (direction) {
                case UP:
                    this.points[0].y--;
                    break;
                case DOWN:
                    this.points[0].y++;
                    break;
                case LEFT:
                    this.points[0].x--;
                    break;
                case RIGHT:
                    this.points[0].x++;
                    break;

            }
            this.points[0].y = (this.points[0].y + Game.HEIGHT) % Game.HEIGHT;
            this.points[0].x = (this.points[0].x + Game.WIDTH)  % Game.WIDTH;
        }
        public void generate() {
            Point tmp = new Point(Game.WIDTH / 2, Game.WIDTH / 2);
            this.points.Add(tmp);

        }
        public void upgrade() {
            Point lastPoint = points[points.Count - 1];
            this.points.Add(new Point(lastPoint.x, lastPoint.y));
        }
        public void changeDirection(int new_direction)
        {
            direction = new_direction;
        }
    }
}
