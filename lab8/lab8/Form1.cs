using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab8
{
    public partial class Form1 : Form
    {
        private Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private double f2(double x)
        {
            return x * x * x * x - 6 * x * x * x + 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var graph = new graphiс {graph = tabPage1.CreateGraphics(), MyPen = new Pen(Color.Blue, 1)};
            graph.graph.Clear(Color.White);

            int X, Y, Xmax, Ymax;
            Xmax = tabPage1.Size.Width;
            Ymax = tabPage1.Size.Height;
            X = Xmax / 2;
            Y = Ymax / 2;
            var penAxis = new Pen(Color.Black);
            graph.graph.DrawLine(penAxis, 0, Y, Xmax, Y);
            graph.graph.DrawLine(penAxis, X, 0, X, Ymax);
            double h = 0.1, Xdec = -5, Ydec = f2(Xdec);
            int d = 20, Xp = Convert.ToInt16(Xdec * d) + X, Yp = -Convert.ToInt16(Ydec * d) + Y;
            for (int i = X; i < Xmax; i += d)
            {
                graph.graph.DrawLine(penAxis, i, Y - 1, i, Y + 1);
                graph.graph.DrawLine(penAxis, i-X, Y - 1, i-X, Y + 1);
            }
            for (int i = Y; i < Ymax; i += d)
            {
                graph.graph.DrawLine(penAxis, X - 1, i - Y, X + 1, i - Y);
                graph.graph.DrawLine(penAxis, X - 1, i, X + 1, i);
            }
            int Xp1, Yp1;
            while (Xdec < 5)
            {
                Xp1 = Convert.ToInt16((Xdec + h) * d) + X;
                Yp1 = -Convert.ToInt16(f2(Xdec + h) * d) + Y;
                graph.graph.DrawLine(graph.MyPen, Xp, Yp, Xp1, Yp1);
                //  graphic.graph.DrawLine(graphic.MyPen, 90, 190, 180, 185);
                Xdec = Xdec + h;
                Ydec = f2(Xdec);
                Xp = Convert.ToInt16(Xdec * d) + X;
                Yp = -Convert.ToInt16(Ydec * d) + Y;
            }
            button2_Click(sender, e, d);
        }

        private void button2_Click(object sender, EventArgs e, int d)
        {
            var graph = new graphiс {graph = tabPage1.CreateGraphics(), MyPen = new Pen(Color.ForestGreen, 1)};
            var xmax = tabPage1.Size.Width;
            var ymax = tabPage1.Size.Height;
            var x = xmax / 2;
            var y = ymax / 2;

            double h = 0.1;
            double xdec = -2;
            double ydec = xdec * xdec * xdec / ((xdec - 2) * (xdec - 2));

            //int d = 10;
            int xp = Convert.ToInt16(xdec * d) + x;
            int yp = -Convert.ToInt16(ydec * d) + y;

            int xp1;
            int yp1;

            while (xdec < 8)
            {
                if (xdec == 2 || xdec + h == 2)
                {
                    xdec += h;
                    ydec = xdec * xdec * xdec / ((xdec - 2) * (xdec - 2));
                    xp = Convert.ToInt16(xdec * d) + x;

                    try
                    {
                        yp = -Convert.ToInt16(ydec * d) + y;
                    }
                    catch (Exception)
                    {
                        yp = -1;
                    }
                }
                else
                {
                    xp1 = Convert.ToInt16((xdec + h) * d) + x;

                    try
                    {
                        yp1 = -Convert.ToInt16((xdec + h) * (xdec + h) * (xdec + h) /
                                               ((xdec + h - 2) * (xdec + h - 2)) * d) + y;
                    }
                    catch (Exception)
                    {
                        yp1 = -1;
                    }

                    graph.graph.DrawLine(graph.MyPen, xp, yp, xp1, yp1);
                    xdec = xdec + h;
                    ydec = xdec * xdec * xdec / ((xdec - 2) * (xdec - 2));
                    xp = Convert.ToInt16(xdec * d) + x;

                    try
                    {
                        yp = -Convert.ToInt16(ydec * d) + y;
                    }
                    catch (Exception)
                    {
                        yp = -1;
                    }

                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var trinagleBuilder = new TrinagleBuilder(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6);
            var graphic = new graphiс {graph = tabPage2.CreateGraphics(), MyPen = new Pen(Color.Blue, 1)};
            graphic.graph.Clear(Color.White);
            graphic.graph.DrawEllipse(graphic.MyPen,
                trinagleBuilder.CircumscribedCircleCenter.X - trinagleBuilder.CircumscribedCircleRadius,
                trinagleBuilder.CircumscribedCircleCenter.Y - trinagleBuilder.CircumscribedCircleRadius,
                2 * trinagleBuilder.CircumscribedCircleRadius,
                2 * trinagleBuilder.CircumscribedCircleRadius);
            graphic.graph.DrawEllipse(new Pen(Color.Red, 1),
                trinagleBuilder.InscribedCircleCenter.X - trinagleBuilder.InscribedCircleRadius,
                trinagleBuilder.InscribedCircleCenter.Y - trinagleBuilder.InscribedCircleRadius,
                2 * trinagleBuilder.InscribedCircleRadius,
                2 * trinagleBuilder.InscribedCircleRadius);
            graphic.graph.DrawLine(graphic.MyPen, trinagleBuilder.A, trinagleBuilder.B);
            graphic.graph.DrawLine(graphic.MyPen, trinagleBuilder.C, trinagleBuilder.B);
            graphic.graph.DrawLine(graphic.MyPen, trinagleBuilder.A, trinagleBuilder.C);
        }

        private void tabPage3_MouseDown(object sender, MouseEventArgs e)
        {
            MouseControl.Indicator = 1;
            MouseControl.Graphics = this.tabPage3.CreateGraphics();
            MouseControl.Pen = new Pen(Color.Azure, 1);
            MouseControl.Point = new Point(); //центр пучка
            MouseControl.Point1 = new Point();
            MouseControl.Point2 = new Point();
            MouseControl.Point3 = new Point();

        }

        private void tabPage3_MouseUp(object sender, MouseEventArgs e)
        {
            MouseControl.Indicator = 0;
        }

        private void tabPage3_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseControl.Indicator == 1)
            {
                // Очищаем полотно
                MouseControl.Graphics.Clear(Color.White);
                //Считываем координаты мыши и присваиваем их координатам центра
                MouseControl.Point.X = e.X;
                MouseControl.Point.Y = e.Y;
                //Задаем цвет первой кривой
                MouseControl.Pen.Color = Color.Blue;
                // Задаем дополнительные точки
                MouseControl.Point1.X = 0;
                MouseControl.Point1.Y = 0;
                MouseControl.Point2.X = 0;
                MouseControl.Point2.Y = tabPage3.Height;
                MouseControl.Point3.X = tabPage3.Width;
                MouseControl.Point3.Y = tabPage3.Height;
                // Рисуем первую кривую
                MouseControl.Graphics.DrawBezier(MouseControl.Pen, MouseControl.Point, MouseControl.Point1,
                    MouseControl.Point2, MouseControl.Point3);
                // Вторая кривая
                MouseControl.Pen.Color = Color.Green;
                MouseControl.Point1.X = tabPage3.Width;
                MouseControl.Point1.Y = 0;
                MouseControl.Point2.X = tabPage3.Width;
                MouseControl.Point2.Y = tabPage3.Height;
                MouseControl.Point3.X = 0;
                MouseControl.Point3.Y = tabPage3.Height;
                MouseControl.Graphics.DrawBezier(MouseControl.Pen, MouseControl.Point, MouseControl.Point1,
                    MouseControl.Point2, MouseControl.Point3);
                // Третья кривая
                MouseControl.Pen.Color = Color.Red;
                MouseControl.Point1.X = tabPage3.Width / 3;
                MouseControl.Point1.Y = tabPage3.Height / 2;
                MouseControl.Point2.X = 2 * tabPage3.Width / 3;
                MouseControl.Point2.Y = tabPage3.Height / 2;
                MouseControl.Point3.X = MouseControl.Point.X;
                MouseControl.Point3.Y = MouseControl.Point.Y;
                MouseControl.Graphics.DrawBezier(MouseControl.Pen, MouseControl.Point, MouseControl.Point1,
                    MouseControl.Point2, MouseControl.Point3);
            }
        }

        private void tabPage3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MouseControl.Graphics.Clear(Color.White);
        }

        private void randomFiguresTab_MouseClick(object sender, MouseEventArgs e)
        {
            var hatchStyles = Enum.GetValues(typeof(HatchStyle));
            var drawer = new RandomFigDrawer()
            {
                Graph = randomFiguresTab.CreateGraphics(),
                ElipseBrush = new LinearGradientBrush(
                    new PointF(rnd.Next(1, randomFiguresTab.Width), rnd.Next(1, randomFiguresTab.Height)),
                    new PointF(rnd.Next(1, randomFiguresTab.Width), rnd.Next(1, randomFiguresTab.Height)),
                    Color.FromArgb(255, rnd.Next(256), rnd.Next(256),
                        rnd.Next(256)), Color.FromArgb(255, rnd.Next(256), rnd.Next(256), rnd.Next(256))),
                RectangleBrush = new HatchBrush((HatchStyle) hatchStyles.GetValue(rnd.Next(hatchStyles.Length)),
                    Color.FromArgb(255, rnd.Next(256), rnd.Next(256), rnd.Next(256))),
                PieBrush = new SolidBrush(Color.FromArgb(255, rnd.Next(256), rnd.Next(256), rnd.Next(256)))
            };
            drawer.Graph.FillEllipse(drawer.ElipseBrush, rnd.Next(randomFiguresTab.Width),
                rnd.Next(randomFiguresTab.Height), rnd.Next(randomFiguresTab.Width), rnd.Next(randomFiguresTab.Height));
            drawer.Graph.FillRectangle(drawer.RectangleBrush, rnd.Next(randomFiguresTab.Width),
                rnd.Next(randomFiguresTab.Height), rnd.Next(randomFiguresTab.Width), rnd.Next(randomFiguresTab.Height));
            drawer.Graph.FillPie(drawer.PieBrush, rnd.Next(randomFiguresTab.Width),
                rnd.Next(randomFiguresTab.Height), rnd.Next(randomFiguresTab.Width), rnd.Next(randomFiguresTab.Height), rnd.Next(361), rnd.Next(361));

        }

        private void randomFiguresTab_Click(object sender, EventArgs e)
        {

        }
    }
}
