using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris.Controllers
{
    public static class TetrisControl
    {
        public static Shape currentShape;
        public static int size;
        public static int[,] map = new int[16, 8];
        public static int linesRemoved;
        public static int score;
        public static int Interval;
        public static void ShowNextShape(Graphics e) //Hiển thị Tetromino tiếp theo
        {
            for (int i = 0; i < currentShape.SizeNextMatrix; i++)
            {
                for (int j = 0; j < currentShape.SizeNextMatrix; j++)
                {
                    if (currentShape.NextMatrix[i, j] == 1)
                    {
                       e.FillRectangle(Brushes.Red, new Rectangle(300 + j * (size) + 1, 60 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (currentShape.NextMatrix[i, j] == 2)
                    {
                        e.FillRectangle(Brushes.Yellow, new Rectangle(300 + j * (size) + 1, 60 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (currentShape.NextMatrix[i, j] == 3)
                    {
                        e.FillRectangle(Brushes.Green, new Rectangle(350 + j * (size) + 1, 60 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (currentShape.NextMatrix[i, j] == 4)
                    {
                        e.FillRectangle(Brushes.Blue, new Rectangle(350 + j * (size) + 1, 60 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (currentShape.NextMatrix[i, j] == 5)
                    {
                        e.FillRectangle(Brushes.Purple, new Rectangle(350 + j * (size) + 1, 60 + i * (size) + 1, size - 1, size - 1));
                    }
                }
            }
        }

        public static void ClearMap()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    map[i, j] = 0;
                }
            }
        }

        public static void DrawMap(Graphics e) //Hàm tô màu cho ô
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (map[i, j] == 1)
                    {
                        e.FillRectangle(Brushes.Red, new Rectangle(50 + j * (size) + 1, 50 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (map[i, j] == 2)
                    {
                        e.FillRectangle(Brushes.Yellow, new Rectangle(50 + j * (size) + 1, 50 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (map[i, j] == 3)
                    {
                        e.FillRectangle(Brushes.Green, new Rectangle(50 + j * (size) + 1, 50 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (map[i, j] == 4)
                    {
                        e.FillRectangle(Brushes.Blue, new Rectangle(50 + j * (size) + 1, 50 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (map[i, j] == 5)
                    {
                        e.FillRectangle(Brushes.Purple, new Rectangle(50 + j * (size) + 1, 50 + i * (size) + 1, size - 1, size - 1));
                    }
                }
            }
        }

        public static void DrawGrid(Graphics g) //Vẽ gameboard
        {
            SolidBrush blueBrush = new SolidBrush(Color.White);

            // Create rectangle.
            Rectangle rect = new Rectangle(50, 50, 8 * 25, 16 * 25);

            // Fill rectangle to screen.
            g.FillRectangle(blueBrush, rect);

            for (int i = 0; i <= 16; i++)
            {
                g.DrawLine(Pens.White, new Point(50, 50 + i * size), new Point(50 + 8 * size, 50 + i * size));
            }
            for (int i = 0; i <= 8; i++)
            {
                g.DrawLine(Pens.White, new Point(50 + i * size, 50), new Point(50 + i * size, 50 + 16 * size));
            }  
        }

        public static void SliceMap(Label label1,Label label2) //Hàm tính điểm
        {
            int count = 0;
            int curRemovedLines = 0;
            for (int i = 0; i < 16; i++)
            {
                count = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (map[i, j] != 0)
                        count++;
                }
                if (count == 8)
                {
                    curRemovedLines++;
                    for (int k = i; k >= 1; k--)
                    {
                        for (int o = 0; o < 8; o++)
                        {
                            map[k, o] = map[k - 1, o];
                        }
                    }
                }
            }
            for (int i = 0; i < curRemovedLines; i++)
            {
                score += 10 * (i + 1);
            }
            linesRemoved += curRemovedLines;

            if (linesRemoved % 5 == 0)
            {
                if (Interval > 60)
                    Interval -= 10;
            }

            label1.Text = score.ToString();
            label2.Text = linesRemoved.ToString();
        }

        public static bool IsIntersects() //hàm kiểm tra khi xoay có va chạm hay không
        {
            for (int i = currentShape.Y; i < currentShape.Y + currentShape.SizeMatrix; i++)
            {
                for (int j = currentShape.X; j < currentShape.X + currentShape.SizeMatrix; j++)
                {
                    if (j >= 0 && j <= 7)
                    {
                        if (map[i, j] != 0 && currentShape.Matrix[i - currentShape.Y, j - currentShape.X] == 0)
                            return true;
                    }
                }
            }
            return false;
        }

        public static void Merge() //Chuyển trạng thái qua ô tiếp theo
        {
            for (int i = currentShape.Y; i < currentShape.Y + currentShape.SizeMatrix; i++)
            {
                for (int j = currentShape.X; j < currentShape.X + currentShape.SizeMatrix; j++)
                {
                    if (currentShape.Matrix[i - currentShape.Y, j - currentShape.X] != 0)
                        map[i, j] = currentShape.Matrix[i - currentShape.Y, j - currentShape.X];
                }
            }
        }

        public static bool Collide() //hàm kiểm tra va chạm với gameBoard
        {
            for (int i = currentShape.Y + currentShape.SizeMatrix - 1; i >= currentShape.Y; i--)
            {
                for (int j = currentShape.X; j < currentShape.X + currentShape.SizeMatrix; j++)
                {
                    if (currentShape.Matrix[i - currentShape.Y, j - currentShape.X] != 0)
                    {
                        if (i + 1 == 16)
                            return true;
                        if (map[i + 1, j] != 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool CollideHor(int dir) //hàm kiểm tra va chạm với Tetromino
        {
            for (int i = currentShape.Y; i < currentShape.Y + currentShape.SizeMatrix; i++)
            {
                for (int j = currentShape.X; j < currentShape.X + currentShape.SizeMatrix; j++)
                {
                    if (currentShape.Matrix[i - currentShape.Y, j - currentShape.X] != 0)
                    {
                        if (j + 1 * dir > 7 || j + 1 * dir < 0)
                            return true;

                        if (map[i, j + 1 * dir] != 0)
                        {
                            if (j - currentShape.X + 1 * dir >= currentShape.SizeMatrix || j - currentShape.X + 1 * dir < 0)
                            {
                                return true;
                            }
                            if (currentShape.Matrix[i - currentShape.Y, j - currentShape.X + 1 * dir] == 0)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        public static void ResetArea() //hàm xóa bỏ trạng thái cũ
        {
            for (int i = currentShape.Y; i < currentShape.Y + currentShape.SizeMatrix; i++)
            {
                for (int j = currentShape.X; j < currentShape.X + currentShape.SizeMatrix; j++)
                {
                    if (i >= 0 && j >= 0 && i < 16 && j < 8)
                    {
                        if (currentShape.Matrix[i - currentShape.Y, j - currentShape.X] != 0)
                        {
                            map[i, j] = 0;
                        }
                    }
                }
            }
        }



    }
}
