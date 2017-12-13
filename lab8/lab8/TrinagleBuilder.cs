using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab8
{
    internal class TrinagleBuilder
    {
        public Point A { get; set; }
        public Point B { get; set; }
        public Point C { get; set; }
        public Point InscribedCircleCenter { get; set; }
        public int InscribedCircleRadius { get; set; }
        public Point CircumscribedCircleCenter { get; set; }
        public int CircumscribedCircleRadius { get; set; }

        public TrinagleBuilder(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            A = new Point(x1, y1);
            B = new Point(x2, y2);
            C = new Point(x3, y3);
        }

        public TrinagleBuilder(Control x1, Control y1, Control x2, Control y2, Control x3, Control y3)
        {
            A = new Point(Convert.ToInt16(x1.Text), Convert.ToInt16(y1.Text));
            B = new Point(Convert.ToInt16(x2.Text), Convert.ToInt16(y2.Text));
            C = new Point(Convert.ToInt16(x3.Text), Convert.ToInt16(y3.Text));
            InitializeInscribedCircleCenter();
            InitializeInscribedCircleRadius();
            InitializeCircumscribedCircleCenter();
            InitializeCircumscribedCircleRadius();

        }

        private void InitializeInscribedCircleRadius()
        {
            var abDist = EvalDistance(A, B);
            var bcDist = EvalDistance(B, C);
            var acDist = EvalDistance(A, C);
            InscribedCircleRadius = Convert.ToInt16(2 * EvalArea(A, B, C) / (abDist + acDist + bcDist));
        }

        private void InitializeInscribedCircleCenter()
        {
            var abDist = EvalDistance(A, B);
            var bcDist = EvalDistance(B, C);
            var acDist = EvalDistance(A, C);
            var x = Convert.ToInt16((abDist * C.X + bcDist * A.X + acDist * B.X) / (abDist + bcDist + acDist));
            var y = Convert.ToInt16((abDist * C.Y + bcDist * A.Y + acDist * B.Y) / (abDist + bcDist + acDist));
            InscribedCircleCenter = new Point(x, y);
        }

        private void InitializeCircumscribedCircleCenter()
        {
            var aX = A.X;
            var bX = B.X;
            var cX = C.X;
            var aY = A.Y;
            var bY = B.Y;
            var cY = C.Y;
            var x = Convert.ToInt16(0.5 * ((aX * aX + aY * aY) * (bY - cY) + (bX * bX + bY * bY) * (cY - aY) + (cX * cX + cY * cY) * (aY - bY))
                                               / (aX * (bY - cY) + bX * (cY - aY) + cX * (aY - bY)));
            var y = Convert.ToInt16(0.5 * ((aX * aX + aY * aY) * (cX - bX) + (bX * bX + bY * bY) * (aX - cX) + (cX * cX + cY * cY) * (bX - aX))
                                               / (aX * (bY - cY) + bX * (cY - aY) + cX * (aY - bY)));
            CircumscribedCircleCenter = new Point(x, y);
            
        }

        private void InitializeCircumscribedCircleRadius()
        {
            var abDist = EvalDistance(A, B);
            var bcDist = EvalDistance(B, C);
            var acDist = EvalDistance(A, C);
            CircumscribedCircleRadius = Convert.ToInt16(abDist * bcDist * acDist / 4 / EvalArea(A, B, C));
        }

        private double EvalDistance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        private double EvalArea(Point a, Point b, Point c)
        {
            var abDist = EvalDistance(a, b);
            var bcDist = EvalDistance(b, c);
            var acDist = EvalDistance(a, c);
            var halfPerimeter = (abDist + acDist + bcDist) / 2;
            return Math.Sqrt(halfPerimeter * (halfPerimeter - abDist) * (halfPerimeter - bcDist) *
                             (halfPerimeter - acDist));
        }
    }
}
