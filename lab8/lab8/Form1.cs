using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private double f2(double x)
        {
            return x*x*x*x - 6*x*x*x + 1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var graph = new graphiс {graph = tabPage1.CreateGraphics(), MyPen = new Pen(Color.Blue, 1)};
            graph.graph.Clear(Color.White);

            int X, Y, Xmax, Ymax;
            Xmax = tabPage1.Size.Width; Ymax = tabPage1.Size.Height;
            X = Xmax / 2; Y = Ymax / 2;

            graph.graph.DrawLine(graph.MyPen, 0, Y, Xmax, Y);
            graph.graph.DrawLine(graph.MyPen, X, 0, X, Ymax);
            double h = 0.1, Xdec = -5, Ydec =f2(Xdec);
            int d = 10, Xp = Convert.ToInt16(Xdec * d) + X, Yp = -Convert.ToInt16(Ydec * d) + Y;

            int Xp1, Yp1;
            while (Xdec < 5)
            {
                Xp1 = Convert.ToInt16((Xdec + h) * d) + X;
                Yp1 = -Convert.ToInt16(f2(Xdec+h) * d) + Y;
                graph.graph.DrawLine(graph.MyPen, Xp, Yp, Xp1, Yp1);
                //  graphic.graph.DrawLine(graphic.MyPen, 90, 190, 180, 185);
                Xdec = Xdec + h; Ydec = f2(Xdec);
                Xp = Convert.ToInt16(Xdec * d) + X; Yp = -Convert.ToInt16(Ydec * d) + Y;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var graph = new graphiс { graph = tabPage1.CreateGraphics(), MyPen = new Pen(Color.Blue, 1) };
            graph.graph.Clear(Color.White);

            var xmax = tabPage1.Size.Width;
            var ymax = tabPage1.Size.Height;
            var x = xmax / 2;
            var y = ymax / 2;

            graph.graph.DrawLine(graph.MyPen, 0, y, xmax, y);
            graph.graph.DrawLine(graph.MyPen, x, 0, x, ymax);

            double h = 0.1;
            double xdec = -2;
            double ydec = xdec*xdec*xdec/((xdec - 2) * (xdec - 2));

            int d = 10;
            int xp = Convert.ToInt16(xdec * d) + x;
            int yp = -Convert.ToInt16(ydec * d) + y;

            int xp1;
            int yp1;

            while (xdec < 8)
            {
                if (xdec == 2 || xdec+h == 2)
                {
                    xdec += h;
                    ydec = xdec*xdec*xdec/((xdec - 2)*(xdec - 2));
                    xp = Convert.ToInt16(xdec*d) + x;

                    try
                    {
                        yp = -Convert.ToInt16(ydec*d) + y;
                    }
                    catch (Exception)
                    {
                        yp = -1;
                    }
                }
                else
                {
                    xp1 = Convert.ToInt16((xdec + h)*d) + x;

                    try
                    {
                        yp1 = -Convert.ToInt16((xdec + h)*(xdec + h)*(xdec + h)/((xdec + h - 2)*(xdec + h - 2))*d) + y;
                    }
                    catch (Exception)
                    {
                        yp1 = -1;
                    }

                    graph.graph.DrawLine(graph.MyPen, xp, yp, xp1, yp1);
                    xdec = xdec + h;
                    ydec = xdec*xdec*xdec/((xdec - 2)*(xdec - 2));
                    xp = Convert.ToInt16(xdec*d) + x;

                    try
                    {
                        yp = -Convert.ToInt16(ydec*d) + y;
                    }
                    catch (Exception)
                    {
                        yp = -1;
                    }

                }
            }

        }
    }
}
