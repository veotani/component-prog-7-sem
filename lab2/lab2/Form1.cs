using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void form2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.frm2 == null)
            {
                Program.frm2 = new Form2();
                Program.frm2.Size = new Size(1920/2, 1080/2);
                Program.frm2.StartPosition = FormStartPosition.Manual;
                Program.frm2.Left = 0;
                Program.frm2.Top = 1080 - Program.frm2.Height;
                Program.frm2.Show();
            }
        }

        private void form3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.frm3 == null)
            {
                Program.frm3 = new Form3();
                Program.frm3.Size = new Size(1920/2, 1080/2);
                Program.frm3.StartPosition = FormStartPosition.Manual;
                Program.frm3.Top = 1080 - Program.frm3.Height;
                Program.frm3.Left = 1920 - Program.frm3.Width;
                Program.frm3.Show();

            }
        }

        private void form4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.frm4 == null)
            {
                Program.frm4 = new Form4();
                Program.frm4.MdiParent = this;
                Program.frm4.StartPosition = FormStartPosition.Manual;
                Program.frm4.Top = 0;
                Program.frm4.Left = 0;
                Program.frm4.Size = this.Size;
                Program.frm4.Show();
            }
        }

        private void form5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int D = 50;
            if (Program.frm5 == null)
            {
                Program.frm5 = new Form5();
                Program.frm5.MdiParent = this;
                Program.frm5.Size = new Size(Size.Width - D, Size.Height - D);
                Program.frm5.Top = D;
                Program.frm5.Left = D;
                Program.frm5.Show();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (Program.frm4 != null)
            {
                Program.frm4.Size = Size;
                Program.frm4.Top = 0;
                Program.frm4.Left = 0;
            }
            if (Program.frm5 != null)
            {
                Program.frm5.Size = new Size(Width - 20, Height - 20);
                Program.frm5.Top = 20;
                Program.frm5.Left = 20;
            }

        }
    }
}
