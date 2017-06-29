using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 连连看
{
    public partial class Form1 : Form
    {
        private int X1 = 0;
        private int X2 = 0;
        private int Y1 = 0;
        private int Y2 = 0;
        private int cishu = 0;//点击鼠标的次数
        private int[,] arr;
        private int time = 0;
        public Form1()
        {
            InitializeComponent();
            arr = new int[22, 22];
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 22; j++)
                {
                    arr[i, j] = -1;
                }
            }
        }
        private void paint()//画格子
        {
            Graphics g = CreateGraphics();
            g.Clear(Color.White);
            Pen mypen1 = new Pen(Color.Black);
            for (int i = 1; i < 22; i++)
            {
                g.DrawLine(mypen1, 50, 20 + 30 * i, 650, 20 + 30 * i);
                g.DrawLine(mypen1, 20 + 30 * i, 50, 20 + 30 * i, 650);
                if(i<21)
                {
                    for (int j = 1; j < 21; j++)
                    {
                        if (arr[i, j] != -1)
                            g.DrawImage(imageList1.Images[arr[i, j]], 21 + 30 * i, 21 + 30 * j, 29, 29);
                    }
                }
            }
        }
        private void paint1()//画空白
        {
            Graphics g = CreateGraphics();
            SolidBrush mybrush1 = new SolidBrush(Color.White);
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 22; j++)
                {
                    if (arr[i, j] == -1)
                    {
                        g.FillRectangle(mybrush1, 21 + 30 * i, 21 + 30 * j, 29, 29);
                    }
                }

            }
        }
        private void paigezi()//对格子进行排序
        {
            Random ran = new Random();
            for (int i = 0; i < 200; i++)
            {
                int x1 = ran.Next(1, 21);
                int y1 = ran.Next(1, 21);
                int x2 = ran.Next(1, 21);
                int y2 = ran.Next(1, 21);
                int z = ran.Next(0, 34);
                if (arr[x1, y1] == -1 && arr[x2, y2] == -1&&(x1!=x2||y1!=y2))
                {
                    arr[x1, y1] = z;
                    arr[x2, y2] = z;
                }
                else
                    i--;
            }
        }
        private void newgamebutton_Click(object sender, EventArgs e)
        {
            time = 0;
            progressBar1.Value = 0;
            timer1.Enabled = true;
            for (int i = 1; i < 21; i++)
            {
                for (int j = 1; j < 21; j++)
                {
                    arr[i, j] = -1;
                }
            }
            paigezi();
            paint();
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X >= 50 && e.X <= 650 && e.Y >= 50 && e.Y <= 650)
            {
                cishu++;
                if (cishu == 1)
                {
                    X1 = (e.X - 20) / 30;
                    Y1 = (e.Y - 20) / 30;
                    xiaochu();
                }
                else
                {
                    X2 = (e.X - 20) / 30;
                    Y2 = (e.Y - 20) / 30;
                    cishu = 0;
                    xiaochu();
                }
            }
        }
        private void xiaochu()
        {
            //从左到右
            for (int i = 0; i < 22; i++)
            {
                if (f1(i, X1, Y1) && f1(i, X2, Y2) && f4(i, Y1, Y2) && (X1 != X2 || Y1 != Y2) && arr[X1, Y1] == arr[X2, Y2] && arr[X1, Y1]!=-1)//三条路都通
                {
                    arr[X1, Y1] = -1;
                    arr[X2, Y2] = -1;
                    Graphics g = CreateGraphics();
                    Pen mypen1 = new Pen(Color.Black);
                    SolidBrush mybrush1 = new SolidBrush(Color.White);
                    g.FillRectangle(mybrush1, 21 + 30 * X1, 21 + 30 * Y1, 29, 29);
                    g.FillRectangle(mybrush1, 21 + 30 * X2, 21 + 30 * Y2, 29, 29);
                    g.DrawLine(mypen1, 35 + 30 * X1, 35 + 30 * Y1, 35 + 30 * i, 35 + 30 * Y1);
                    g.DrawLine(mypen1, 35 + 30 * X2, 35 + 30 * Y2, 35 + 30 * i, 35 + 30 * Y2);
                    g.DrawLine(mypen1, 35 + 30 * i, 35 + 30 * Y1, 35 + 30 * i, 35 + 30 * Y2);
                    progressBar1.Value += 3;
                    if (shibai() == false)
                        MessageBox.Show("没有可以连的了，重新再来吧！");
                }
            }
            //从上到下
            for (int i = 0; i < 22; i++)
            {
                if (f2(i, Y1, X1) && f2(i, Y2, X2) && f3(i, X1, X2) && (X1 != X2 || Y1 != Y2) && arr[X1, Y1] == arr[X2, Y2] && arr[X1, Y1] != -1)//三条路都通
                {
                    arr[X1, Y1] = -1;
                    arr[X2, Y2] = -1;
                    Graphics g = CreateGraphics();
                    Pen mypen1 = new Pen(Color.Black);
                    SolidBrush mybrush1 = new SolidBrush(Color.White);
                    g.FillRectangle(mybrush1, 21 + 30 * X1, 21 + 30 * Y1, 29, 29);
                    g.FillRectangle(mybrush1, 21 + 30 * X2, 21 + 30 * Y2, 29, 29);
                    g.DrawLine(mypen1, 35 + 30 * X1, 35 + 30 * Y1, 35 + 30 *X1, 35 + 30 * i);
                    g.DrawLine(mypen1, 35 + 30 * X2, 35 + 30 * Y2, 35 + 30 *X2, 35 + 30 * i);
                    g.DrawLine(mypen1, 35 + 30 * X1, 35 + 30 * i, 35 + 30 *X2, 35 + 30 * i);
                    progressBar1.Value += 3;
                    if (shibai() == false)
                        MessageBox.Show("没有可以连的了，重新再来吧！");
                }
            }
        }
        private bool f1(int i, int j, int k)//判断行路是否通
        {
            for (; i < j; )
            {
                if (arr[i, k] != -1)//该格不同
                    return false;
                else
                    i++;
            }
            for (; i > j; )
            {
                if (arr[i, k] != -1)//该格不同
                    return false;
                else
                    i--;
            }
            return true;
        }
        private bool f2(int i, int j, int k)//判断列路是否通
        {
            for (; i < j; )
            {
                if (arr[k, i] != -1)//该格不同
                    return false;
                else
                    i++;
            }
            for (; i > j; )
            {
                if (arr[k,i] != -1)//该格不同
                    return false;
                else
                    i--;
            }
            return true;
        }
        private bool f3(int i, int j,int k)//判断某行路是否通
        {
            for (; k < j-1; )
            {
                if (arr[k, i] != -1)//该格不同
                    return false;
                else
                    k++;
            }
            for (; k > j+1; )
            {
                if (arr[k, i] != -1)//该格不同
                    return false;
                else
                    k--;
            }
            return true;
        }
        private bool f4(int i, int j, int k)//判断某列路是否通
        {
            for (; j < k-1; )
            {
                if (arr[i, j] != -1)//该格不同
                    return false;
                else
                    j++;
            }
            for (; j > k+1; )
            {
                if (arr[i, j] != -1)//该格不同
                    return false;
                else
                    j--;
            }
            return true;
        }
        private bool shibai()
        {
            int n = 0;//消除的个数
            for (int i = 1; i < 21; i++)
            {
                for (int j = 1; j < 21; j++)
                {
                    if (arr[i, j] == -1)
                        n++;
                }
            }
            if (n == 400)
            {
                MessageBox.Show("游戏胜利，请继续保持");
                return true;
            }
            else
            {
                for (int x1 = 1; x1 < 21; x1++)
                {
                    for (int y1 = 1; y1 < 21; y1++)
                    {
                        for (int x2 = 1; x2 < 21; x2++)
                        {
                            for (int y2 = 1; y2 < 21; y2++)
                            {
                                for (int i = 0; i < 22; i++)
                                {

                                    if (f1(i, x1, y1) && f1(i, x2, y2) && f4(i, y1, y2) && (x1 != x2 || y1 != y2) && arr[x1, y1] == arr[x2, y2] && arr[x1, y1] != -1)//三条路都通
                                    {
                                        return true;
                                    }
                                }
                                for (int i = 0; i < 22; i++)
                                {
                                    if (f2(i, y1, x1) && f2(i, y2, x2) && f3(i, x1, x2) && (x1 != x2 || y1 != y2) && arr[x1, y1] == arr[x2, y2] && arr[x1, y1] != -1)//三条路都通
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            paint1();
            time++;
            timelabel.Text = time.ToString() + "s";
        }
        private void label2_Click(object sender, EventArgs e)
        {
            paint();
        }
    }
}