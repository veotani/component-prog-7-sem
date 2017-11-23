using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            toolStripStatusLabel1.Alignment = ToolStripItemAlignment.Right;
            this.toolStripStatusLabel1.Text = DateTime.Now.ToLongTimeString();
            toolTip1.SetToolTip(statusStrip1, DateTime.Now.ToShortDateString());
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
                for (int i = 0; i < size.Item1; i++)
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
                    dataGridView2.Rows[i].Cells[changing.Item1 - 1].Value =
                        dataGridView1.Rows[i].Cells[changing.Item2 - 1].Value;
                }

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView2.Rows[i].Cells[changing.Item2 - 1].Value =
                        dataGridView1.Rows[i].Cells[changing.Item1 - 1].Value;
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                dataGridView3.RowCount = dataGridView1.RowCount;
                dataGridView3.ColumnCount = dataGridView1.ColumnCount;
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    for (int j = 0; j < dataGridView1.RowCount; j++)
                    {
                        try
                        {
                            dataGridView3.Rows[j].Cells[i].Value =
                                int.Parse(dataGridView1.Rows[j].Cells[i].Value.ToString()) +
                                int.Parse(dataGridView2.Rows[j].Cells[i].Value.ToString());
                        }
                        catch (Exception ex)
                        {
                            label10.Text = ex.Message;
                        }
                    }
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    for (int j = 0; j < dataGridView1.RowCount; j++)
                    {
                        dataGridView1[i, j].Value = "";
                        dataGridView2[i, j].Value = "";
                        try
                        {
                            dataGridView3[i, j].Value = "";
                        }
                        catch (Exception)
                        {
                            
                        }
                    }
                }

                dataGridView1.RowCount = 0;
                dataGridView1.ColumnCount = 0;
                dataGridView2.RowCount = 0;
                dataGridView2.ColumnCount = 0;
                try
                {
                    dataGridView3.RowCount = 0;
                    dataGridView3.ColumnCount = 0;
                }
                catch (Exception)
                {
                    
                }

            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();

            // Создаем файловую переменную
            StreamWriter FS = new StreamWriter(saveFileDialog1.FileName);
            // или
            // StreamWriter FS = new StreamWriter(saveFileDialog1.FileName, true, System.Text.Encoding.GetEncoding(1251));
            // Записываем информацию в файл
            FS.Write("{0} {1}\n", dataGridView1.RowCount, dataGridView1.ColumnCount);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    FS.Write("{0} ", dataGridView1[i, j].Value);
                }
                FS.Write("\n");
            }
            FS.Write("\n");
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    FS.Write("{0} ", dataGridView2[i, j].Value);
                }
                FS.Write("\n");
            }
            // Закрываем файл
            FS.Close();

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            StreamReader FS = new StreamReader(openFileDialog1.FileName);
            var str = FS.ReadToEnd();
            var lines = str.Split('\n');
            dataGridView1.RowCount = int.Parse(lines[0].Split(' ')[0]);
            dataGridView2.RowCount = int.Parse(lines[0].Split(' ')[0]);
            dataGridView1.ColumnCount = int.Parse(lines[0].Split(' ')[1]);
            dataGridView2.ColumnCount = int.Parse(lines[0].Split(' ')[1]);
            for (int i = 1; i <= dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1.Rows[i-1].Cells[j].Value = lines[i].Split(' ')[j];
                    dataGridView2.Rows[i - 1].Cells[j].Value = lines[i + dataGridView1.RowCount + 1].Split(' ')[j];
                }
            }


            FS.Close();
        }

        private void клавиатураToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void матрицыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToLongTimeString();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
        }

        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
        }

        private void toolStripStatusLabel1_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.ToolTipText = DateTime.Now.Date.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {   
        }
    }
}
