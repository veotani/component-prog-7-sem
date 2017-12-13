using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace lab9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.RowCount = 2;
            dataGridView1.Rows[0].HeaderCell.Value = "Математика";
            dataGridView1.Rows[1].HeaderCell.Value = "Русский язык";
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            var rnd = new Random();
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                int mathMark = rnd.Next(35, 96);
                dataGridView1.Rows[0].Cells[i].Value = mathMark;
                int rusMark = rnd.Next(35, 96);
                dataGridView1.Rows[1].Cells[i].Value = rusMark;
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void графикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.Hide();
            button3.Hide();
            button1.Show();
            zedGraphControl1.GraphPane.XAxis.MajorGrid.IsVisible = true;
            zedGraphControl1.GraphPane.YAxis.MajorGrid.IsZeroLine = true;
            zedGraphControl1.GraphPane.Title.Text = графикToolStripMenuItem.Text;
            

            PointPairList points = new PointPairList();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                points.Add(Convert.ToInt64(dataGridView1.Columns[i].HeaderCell.Value),
                    (Convert.ToInt64(value: dataGridView1.Rows[0].Cells[i].Value) +
                     Convert.ToInt64(dataGridView1.Rows[1].Cells[i].Value)) / 2);
            }
            var minY = points.Min(m => m.Y);
            var min = points.First(m => m.Y == minY);
            var maxY = points.Max(m => m.Y);
            var max = points.First(m => m.Y == maxY);

            //ArrowObj arrowMin = new ArrowObj(min.X+0.1, min.Y - 5, min.X, min.Y - 0.05);
            //ArrowObj arrowMax = new ArrowObj(max.X + 0.1, max.Y + 5, max.X, max.Y + 0.05);
            TextObj text1 = new TextObj("Min", min.X, min.Y - 3);
            TextObj text2 = new TextObj("Max", max.X, max.Y + 3);

            text1.FontSpec.Border.IsVisible = false;
            text2.FontSpec.Border.IsVisible = false;
            text2.FontSpec.FontColor = Color.Blue;
            pane.GraphObjList.Add(text1);
            pane.GraphObjList.Add(text2);
            //pane.GraphObjList.Add(arrowMin);
            //pane.GraphObjList.Add(arrowMax);
            pane.XAxis.Title.Text = "Год";
            pane.YAxis.Title.Text = "Средний балл";
            var line = pane.AddCurve("Средний балл", points, Color.Blue, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void точкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.Hide();
            button3.Hide();
            button1.Show();
            zedGraphControl1.GraphPane.Title.Text = точкиToolStripMenuItem.Text;
            zedGraphControl1.GraphPane.XAxis.MajorGrid.IsVisible = true;
            zedGraphControl1.GraphPane.YAxis.MajorGrid.IsZeroLine = true;
            PointPairList points = new PointPairList();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                points.Add(Convert.ToInt64(dataGridView1.Columns[i].HeaderCell.Value),
                    (Convert.ToInt64(value: dataGridView1.Rows[0].Cells[i].Value) +
                     Convert.ToInt64(dataGridView1.Rows[1].Cells[i].Value)) / 2);
            }
            pane.XAxis.Title.Text = "Год";
            pane.YAxis.Title.Text = "Средний балл";
            var line = pane.AddCurve("Средний балл", points, Color.Blue, SymbolType.Circle);
            line.Line.IsVisible = false;
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            //PointPairList mathPoints = new PointPairList();
            //PointPairList rusPoints = new PointPairList(); 
            //GraphPane pane = zedGraphControl1.GraphPane;
            //pane.CurveList.Clear();
            //for (int i = 0; i < dataGridView1.ColumnCount; i++)
            //{
            //    rusPoints.Add(Convert.ToInt64(dataGridView1.Columns[i].HeaderCell.Value), Convert.ToInt64(dataGridView1.Rows[1].Cells[i].Value));
            //    mathPoints.Add(Convert.ToInt64(dataGridView1.Columns[i].HeaderCell.Value), Convert.ToInt64(dataGridView1.Rows[0].Cells[i].Value));
            //}
            //pane.XAxis.Title.Text = "Год";
            //pane.YAxis.Title.Text = "Средний балл";
            //var line1 = pane.AddCurve("Русский язык", rusPoints, Color.Blue, SymbolType.Circle);
            //var line2 = pane.AddCurve("Математика", mathPoints, Color.BlueViolet, SymbolType.Circle);
            //line1.Line.IsVisible = false;
            //line2.Line.IsVisible = false;
            //zedGraphControl1.AxisChange();
            //zedGraphControl1.Invalidate();
        }

        private void гистограммаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.Hide();
            button3.Hide();
            button1.Show();
            zedGraphControl1.GraphPane.Title.Text = гистограммаToolStripMenuItem.Text;
            PointPairList points = new PointPairList();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                points.Add(Convert.ToInt64(dataGridView1.Columns[i].HeaderCell.Value),
                    (Convert.ToInt64(value: dataGridView1.Rows[0].Cells[i].Value) +
                     Convert.ToInt64(dataGridView1.Rows[1].Cells[i].Value)) / 2);
            }
            pane.XAxis.Title.Text = "Год";
            pane.YAxis.Title.Text = "Средний балл";
            pane.AddBar("Средний балл", points, Color.Blue);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

        }

        private void дваГрафикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Hide();
            button2.Show();
            button3.Show();
            zedGraphControl1.GraphPane.Title.Text = дваГрафикаToolStripMenuItem.Text;
            PointPairList mathPoints = new PointPairList();
            PointPairList rusPoints = new PointPairList();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                rusPoints.Add(Convert.ToInt64(dataGridView1.Columns[i].HeaderCell.Value), Convert.ToInt64(dataGridView1.Rows[1].Cells[i].Value));
                mathPoints.Add(Convert.ToInt64(dataGridView1.Columns[i].HeaderCell.Value), Convert.ToInt64(dataGridView1.Rows[0].Cells[i].Value));
            }
            pane.XAxis.Title.Text = "Год";
            pane.YAxis.Title.Text = "Средний балл";
            pane.AddCurve("Русский язык", rusPoints, Color.Blue, SymbolType.None);
            pane.AddCurve("Математика", mathPoints, Color.BlueViolet, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void диапозонToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Hide();
            button2.Show();
            button3.Show();
            PointPairList mathPoints = new PointPairList();
            PointPairList rusPoints = new PointPairList();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                rusPoints.Add(Convert.ToInt64(dataGridView1.Columns[i].HeaderCell.Value), Convert.ToInt64(dataGridView1.Rows[1].Cells[i].Value));
                mathPoints.Add(Convert.ToInt64(dataGridView1.Columns[i].HeaderCell.Value), Convert.ToInt64(dataGridView1.Rows[0].Cells[i].Value));
            }
            pane.XAxis.Title.Text = "Год";
            pane.YAxis.Title.Text = "Средний балл";
            pane.AddBar("Русский язык", rusPoints, Color.Blue);
            pane.AddBar("Математика", mathPoints, Color.Black);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            zedGraphControl1.GraphPane.CurveList[0].Color = colorDialog1.Color;
            zedGraphControl1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            zedGraphControl1.GraphPane.CurveList[0].Color = colorDialog1.Color;
            zedGraphControl1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            zedGraphControl1.GraphPane.CurveList[1].Color = colorDialog1.Color;
            zedGraphControl1.Refresh();
        }
    }
}
