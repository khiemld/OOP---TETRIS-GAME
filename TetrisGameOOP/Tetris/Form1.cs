using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Controllers;

namespace Tetris
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            this.KeyUp += new KeyEventHandler(keyFunc);
            Init();
        }

        public void Init()
        {
            TetrisControl.size = 25;
            TetrisControl.score = 0;
            TetrisControl.linesRemoved = 0;
            TetrisControl.currentShape = new Shape(3, 0);
            TetrisControl.Interval = 300;
            lbScore.Text = TetrisControl.score.ToString();
            lbLine.Text = TetrisControl.linesRemoved.ToString();

           

            timer1.Interval = TetrisControl.Interval;
            timer1.Tick += new EventHandler(update);
            timer1.Start();
            

            Invalidate();
        }

        private void keyFunc(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:

                    if (!TetrisControl.IsIntersects())
                    {
                        TetrisControl.ResetArea();
                        TetrisControl.currentShape.RotateShape();
                        TetrisControl.Merge();
                        Invalidate();
                    }
                    break;
                case Keys.Space:
                    timer1.Interval = 10;
                    break;
                case Keys.Right:
                    if (!TetrisControl.CollideHor(1))
                    {
                        TetrisControl.ResetArea();
                        TetrisControl.currentShape.MoveRight();
                        TetrisControl.Merge();
                        Invalidate();
                    }
                    break;
                case Keys.Left:
                    if (!TetrisControl.CollideHor(-1))
                    {
                        TetrisControl.ResetArea();
                        TetrisControl.currentShape.MoveLeft();
                        TetrisControl.Merge();
                        Invalidate();
                    }
                    break;
            }
        }

        
        private void update(object sender, EventArgs e)
        {
            TetrisControl.ResetArea();
            if (!TetrisControl.Collide())
            {
                TetrisControl.currentShape.MoveDown();
            }
            else
            {
                TetrisControl.Merge();
                TetrisControl.SliceMap(lbScore,lbLine);
                timer1.Interval = TetrisControl.Interval;
                TetrisControl.currentShape.ResetShape(3,0);
                if (TetrisControl.Collide())
                {
                    TetrisControl.ClearMap();
                    timer1.Tick -= new EventHandler(update);
                    timer1.Stop();
                    MessageBox.Show("Score: " + TetrisControl.score);
                    Init();
                }
            }
            TetrisControl.Merge();
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            TetrisControl.DrawGrid(e.Graphics);
            TetrisControl.DrawMap(e.Graphics);
            SolidBrush blueBrush = new SolidBrush(Color.White);
            TetrisControl.ShowNextShape(e.Graphics);
        }

        private void OnAgainButtonClick(object sender, EventArgs e)
        {
            timer1.Tick -= new EventHandler(update);
            timer1.Stop();
            TetrisControl.ClearMap();
            Init();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
