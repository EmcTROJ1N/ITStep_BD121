using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sapper
{
    class Box : Button
    {
        public bool IsBomb { get; set; }
        public Point Idx { get; set; }
        public Box(Random random, int x, int y)
        {
            Idx = new Point(x, y);
            IsBomb = random.Next(0, 6) == 0 ? true : false;
        } 
    }
}
