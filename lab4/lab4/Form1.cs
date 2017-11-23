using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            label4.Text = "";

            try
            {
                var size = DataParserService.ProceedButtonClick(textBox1.Text, textBox2.Text);
                dataGridView1.RowCount = size.Item1;
                dataGridView1.ColumnCount = size.Item2;

            }
            catch (Exception ex)
            {
                label4.Text = ex.Message;
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label4.Text = "";

            try
            {
                var size = DataParserService.ProceedButtonClick(textBox1.Text, textBox2.Text);
                dataGridView1.RowCount = size.Item1;
                dataGridView1.ColumnCount = size.Item2;
                Random r = new Random();
                for (int i =0; i < size.Item1; i++)
                    for (int j = 0; j < size.Item2; j++)
                        dataGridView1.Rows[i].Cells[j].Value = (r.Next(14) - 5).ToString();

            }
            catch (Exception ex)
            {
                label4.Text = ex.Message;
                return;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            int count = 0;
            bool flag;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                flag = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString() != "0")
                        flag = true;
                }
                count = flag ? count + 1 : count;
            }
            label5.Text = $@"Количество ненулевых строк: {count}";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label8.Text = "";

            try
            {
                var changing = DataParserService.ProceedButtonClick(textBox3.Text, textBox4.Text);
                dataGridView2.RowCount = dataGridView1.RowCount;
                dataGridView2.ColumnCount = dataGridView1.ColumnCount;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView2.Rows[i].Cells[changing.Item1 - 1].Value = dataGridView1.Rows[i].Cells[changing.Item2 - 1].Value;
                }

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView2.Rows[i].Cells[changing.Item2 - 1].Value = dataGridView1.Rows[i].Cells[changing.Item1 - 1].Value;
                }

                for (int i = 0; i < dataGridView1.RowCount; i++)
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        if (j != changing.Item1 - 1 && j != changing.Item2 - 1)
                            dataGridView2.Rows[i].Cells[j].Value = dataGridView1.Rows[i].Cells[j].Value;

            }
            catch (Exception ex)
            {
                label8.Text = ex.Message;
                return;
            }
        }
    }
}
