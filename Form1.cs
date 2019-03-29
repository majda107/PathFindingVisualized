using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PathFindV2
{
    public partial class Form1 : Form
    {
        private Maze maze;
        private int mult = 14;

        private int speed = 100;
        public Form1()
        {
            InitializeComponent();

            string[] maze1map = new string[]
            {
                "########",
                "#S     #",
                "#      #",
                "#      #",
                "# #    #",
                "# ###  #",
                "#   #  #",
                "#      #",
                "# ##   #",
                "#     C#",
                "########",
            };

            this.maze = Maze.GetMazeFromString(maze1map);

            //var g = maze.GetBimap(30);
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            maze.CreateGraphics(e.Graphics, mult);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //mult += 10;
            //this.pictureBox1.Invalidate();

            this.maze.PathFinder(speed, this.pictureBox1);
        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            this.mult = this.trackBar1.Value;
            this.pictureBox1.Invalidate();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.maze.Clear();
            this.pictureBox1.Invalidate();
        }

        private void TrackBar2_ValueChanged(object sender, EventArgs e)
        {
            this.speed = trackBar2.Value;
        }
    }
}
