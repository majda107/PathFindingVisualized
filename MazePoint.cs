using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PathFindV2
{
    class MazePoint
    {
        public Point p { get; private set; }
        public int X => p.X;
        public int Y => p.Y;
        public int count { get; private set; }
        public MazePoint(Point p, int count)
        {
            this.p = p;
            this.count = count;
        }
    }
}
