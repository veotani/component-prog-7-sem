using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public static class Program
    {
        public static Form1 frm1;
        public static Form2 frm2;
        public static Form3 frm3;
        public static Form4 frm4;
        public static Form5 frm5;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            frm1 = new Form1();
            frm1.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(frm1);
        }
    }
}
