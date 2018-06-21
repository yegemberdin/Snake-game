using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    [Serializable]
    class Wall:GameObject
    {
        public Wall() {
            sign = '#';
            color = ConsoleColor.Yellow;
        }

    }
}
