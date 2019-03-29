using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace PathFindV2
{
    class Maze
    {
        public int[,] map { get; private set; }
        public int width { get => map.GetLength(1); }
        public int height { get => map.GetLength(0); }
        public Maze(int[,] map)
        {
            this.map = map;
        }

        public void Clear()
        {
            for(int i = 0; i < this.height; i++)
            {
                for(int j = 0; j < this.width; j++)
                {
                    if (map[i, j] > 0) map[i, j] = 0;
                }
            }
        }

        public Graphics CreateGraphics(Graphics g, int multipiler)
        {
            Font thisFont = new Font(FontFamily.GenericMonospace, (float)(multipiler * 0.35));
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    Pen color = Pens.Transparent;
                    switch (map[j, i])
                    {
                        case -1:
                            g.FillRectangle(Brushes.Gray, new Rectangle(i * multipiler, j * multipiler, (int)(multipiler * 0.8), (int)(multipiler * 0.8)));
                            break;
                        case -10:
                            color = Pens.Green;
                            break;
                        case -11:
                            color = Pens.Orange;
                            break;
                        case 0:
                            color = Pens.Transparent;
                            break;
                        default:
                            color = Pens.Red;
                            break;
                    }

                    g.DrawRectangle(color, new Rectangle(i * multipiler, j * multipiler, (int)(multipiler * 0.8), (int)(multipiler * 0.8)));

                    if(map[j, i] > 0)
                    {
                        g.DrawString(map[j, i].ToString(), thisFont, Brushes.Black, i * multipiler + (int)(multipiler * 0.1), j * multipiler + (int)(multipiler * 0.1));
                    }
                }
            }
            g.Flush();
            return g;
        }
        public Bitmap GetBimap(int multipiler)
        {
            Bitmap bmp = new Bitmap(height * multipiler, width * multipiler);
            var g = Graphics.FromImage(bmp);

            g = this.CreateGraphics(g, multipiler);

            return bmp;
        }

        private Point FindStartPoint()
        {
            for(int i = 0; i < this.height; i++)
            {
                for(int j = 0; j < this.width; j++)
                {
                    if (map[i, j] == -10) return new Point(j, i);
                }
            }
            return new Point(1, 1);
        }

        private void ProcessPoint(MazePoint p, ref Queue<MazePoint> toProcess)
        {
            Point[] points =
            {
                new Point(p.X - 1, p.Y),
                new Point(p.X + 1, p.Y),
                new Point(p.X, p.Y - 1),
                new Point(p.X, p.Y + 1),
            };
            
            foreach(Point v in points)
            {
                if(map[v.Y, v.X] == 0)
                {
                    map[v.Y, v.X] = p.count;
                    toProcess.Enqueue(new MazePoint(v, p.count + 1));
                }
            }
        }
        public void PathFinder(int delay, PictureBox box)
        {
            MazePoint start = new MazePoint(FindStartPoint(), 1);

            Queue<MazePoint> pointsToProcess = new Queue<MazePoint>();
            pointsToProcess.Enqueue(start);


            new Thread(() =>
            {
                while (pointsToProcess.Count > 0)
                {
                    var p = pointsToProcess.Dequeue();

                    ProcessPoint(p, ref pointsToProcess);

                    box.Invoke((MethodInvoker)delegate {
                        box.Invalidate();
                    });
                    System.Threading.Thread.Sleep(delay);
                }
            }).Start();
        }

        public static Maze GetMazeFromString(string[] maze)
        {
            int[,] map = new int[maze.Length, maze[0].Length];
            for(int i = 0; i < maze.Length; i++)
            {
                for(int j = 0; j < maze[i].Length; j++)
                {
                    var num = 0;
                    switch(maze[i][j])
                    {
                        case '#':
                            num = -1;
                            break;
                        case 'S':
                            num = -10;
                            break;
                        case 'C':
                            num = -11;
                            break;
                        default:
                            num = 0;
                            break;
                    }
                    map[i, j] = num;
                }
            }
            return new Maze(map);
        }
    }
}
