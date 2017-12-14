using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiteDB;
using ZedGraph;

namespace lab10
{
    public partial class TestForm : Form
    {
        private DateTime startTime;
        private List<bool> answers;
        private bool waiting = true;
        private bool weStarted = false;
        private bool fromExternalFile = false;
        List<bool> likelyhood = new List<bool>();

        public TestForm()
        {
            InitializeComponent();
            progressBar2.Minimum = 0;
            progressBar2.Maximum = 60;
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            fromExternalFile = false;
            toolStripStatusLabel4.BorderSides = ToolStripStatusLabelBorderSides.All;
            toolStripStatusLabel2.BorderSides = ToolStripStatusLabelBorderSides.All;
            toolStripStatusLabel5.BorderSides = ToolStripStatusLabelBorderSides.All;
            toolStripStatusLabel4.Text = DateTime.Now.ToLongTimeString();
            toolStripStatusLabel2.Text = "Запуск теста";
            weStarted = true;
            startTime = DateTime.Now;
            
            Random rnd = new Random();
            checkedListBox1.Items.Clear();
            checkedListBox1.ItemHeight = 10;
            List<String> operations = new List<string>()
            {
                "+",
                "-",
                "*"
            };
            int numberOfTasks = rnd.Next(5, 10);
            answers = new List<bool>();
            checkedListBox1.ItemHeight = 0;
            for (int i = 0; i < numberOfTasks; i++)
            {
                bool resTrue = true;
                int firstNumber = rnd.Next(10, 20);
                int secNumber = firstNumber - rnd.Next(1, 10);
                int oper = rnd.Next(2);
                if (среднийToolStripMenuItem.Checked || toolStripComboBox1.Text == "Средний")
                {
                    firstNumber = firstNumber * 10 + rnd.Next(50);
                    secNumber = secNumber * 10 + rnd.Next(50);
                }
                if (сложныйToolStripMenuItem.Checked || toolStripComboBox1.Text == "Сложный")
                {
                    firstNumber = firstNumber * 100 + rnd.Next(300);
                    secNumber = secNumber * 100 + rnd.Next(300);
                }
                int res = 0;
                if (operations[oper] == "+")
                {
                    res = firstNumber + secNumber;
                    if (rnd.Next(2) > 0)
                    {
                        res = res + rnd.Next(1, Convert.ToInt32(res * 0.2)+2);
                        resTrue = false;
                    }
                }
                if (operations[oper] == "-")
                {
                    res = firstNumber - secNumber;
                    if (rnd.Next(2) > 0)
                    {
                        res = res + rnd.Next(1, Convert.ToInt32(res * 0.2)+2);
                        resTrue = false;
                    }
                }
                if (operations[oper] == "*")
                {
                    res = firstNumber * secNumber;
                    if (rnd.Next(2) > 0)
                    {
                        res = res + rnd.Next(1, Convert.ToInt32(res * 0.2)+2);
                        resTrue = false;
                    }
                    
                }
                checkedListBox1.Items.Add(firstNumber.ToString() + " " + operations[oper] + " " +
                                          secNumber.ToString() + " = " + res.ToString());
                answers.Add(resTrue);
            }
        }

        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormHandler.firstForm.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "Тек. Время: " + DateTime.Now.ToLongTimeString();
            toolStripStatusLabel1.Text = "Сегодня: " + DateTime.Now.ToLongDateString();
            if (weStarted)
            {
                var dif = DateTime.Now - startTime;
                var diffrence = StripMilliseconds(dif);
                toolStripStatusLabel5.Text = "Время работы: " + diffrence;
                progressBar2.Value = diffrence.Seconds;
                if (diffrence.Minutes == 1 && waiting && !fromExternalFile)
                {
                    waiting = false;
                    weStarted = false;
                    toolStripStatusLabel5.Text = "";
                    progressBar2.Value = 0;
                    toolStripStatusLabel2.Text = "Тест окончен!";
                    toolStripButton5_Click(sender, e);
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
        }
        public static TimeSpan StripMilliseconds(TimeSpan time)
        {
            return new TimeSpan(time.Days, time.Hours, time.Minutes, time.Seconds);
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void простойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            среднийToolStripMenuItem.Checked = false;
            сложныйToolStripMenuItem.Checked = false;
            toolStripComboBox1.Text = "Простой";
        }

        private void среднийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            простойToolStripMenuItem.Checked = false;
            сложныйToolStripMenuItem.Checked = false;
            toolStripComboBox1.Text = "Средний";
        }

        private void сложныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            простойToolStripMenuItem.Checked = false;
            среднийToolStripMenuItem.Checked = false;
            toolStripComboBox1.Text = "Сложный";
        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Text == "Простой")
            {
                сложныйToolStripMenuItem.Checked = false;
                среднийToolStripMenuItem.Checked = false;
                простойToolStripMenuItem.Checked = true;
            }
            if (toolStripComboBox1.Text == "Средний")
            {
                сложныйToolStripMenuItem.Checked = false;
                среднийToolStripMenuItem.Checked = true;
                простойToolStripMenuItem.Checked = false;
            }
            if (toolStripComboBox1.Text == "Сложный")
            {
                сложныйToolStripMenuItem.Checked = true;
                среднийToolStripMenuItem.Checked = false;
                простойToolStripMenuItem.Checked = false;
            }
        }

        private void панельИнстрмуентовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!панельИнстрмуентовToolStripMenuItem.Checked) toolStrip1.Hide();
            else toolStrip1.Show();
        }

        private void строкаСостоянияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!строкаСостоянияToolStripMenuItem.Checked) statusStrip1.Hide();
            else statusStrip1.Show();
        }

        private void времяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!времяToolStripMenuItem.Checked) statusStrip2.Hide();
            else statusStrip2.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (fromExternalFile)
            {
                var correctAnswers = 0;
                var totalAnswers = likelyhood.Count;
                for (int i = 0; i < likelyhood.Count(); i++)
                {
                    if (likelyhood[i] == checkedListBox1.GetItemChecked(i))
                    {
                        correctAnswers += 1;
                    }
                }
                using (var db = new LiteDatabase(@"MyData.db"))
                {
                    var collection = db.GetCollection<StudentAnswers>("StudentAnswers");
                    var results = collection.Find(m => m.StudentId == FormHandler.StudentId);

                    if (results.Count() == 0)
                    {
                        collection.Insert(new StudentAnswers()
                        {
                            CorrectAnswers = correctAnswers,
                            Id = collection.Count() + 1,
                            StudentId = FormHandler.StudentId,
                            TotalAnswers = totalAnswers
                        });

                        waiting = false;
                        weStarted = false;
                        toolStripStatusLabel5.Text = "";
                        progressBar2.Value = 0;
                        toolStripStatusLabel2.Text = "Тест окончен!";
                        db.Dispose();
                        return;
                    }


                    var orderedResults = results.OrderBy(m => m.Id);
                    var orderedResultsList = orderedResults.ToList();
                    orderedResultsList.Reverse();
                    PointPairList resultsTotal = new PointPairList();
                    PointPairList resultsCorrect = new PointPairList();


                    var testResults = new StudentAnswers()
                    {
                        Id = collection.Count() + 1,
                        CorrectAnswers = correctAnswers,
                        TotalAnswers = totalAnswers,
                        StudentId = FormHandler.StudentId
                    };

                    for (int i = 0; i < Math.Min(5, orderedResultsList.Count); i++)
                    {
                        resultsTotal.Add(i, orderedResultsList[i].TotalAnswers);
                        resultsCorrect.Add(i, orderedResultsList[i].CorrectAnswers);
                    }



                    zedGraphControl1.Visible = true;
                    zedGraphControl1.Refresh();
                    zedGraphControl1.GraphPane.Title.Text = "Предыдущие результаты";
                    GraphPane pane = zedGraphControl1.GraphPane;
                    pane.XAxis.Title.Text = "Тест";
                    pane.YAxis.Title.Text = "Правильных ответов";
                    pane.AddBar("Всего", resultsTotal, Color.Blue);
                    pane.AddBar("Правильно", resultsCorrect, Color.Black);
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Invalidate();
                    var mesShow = MessageBox.Show(correctAnswers + " из " + totalAnswers + ": сохранить?", "Вы опять тут?",
                        MessageBoxButtons.OKCancel);
                    if (mesShow == DialogResult.OK)
                    {
                        collection.Insert(testResults);
                    }
                    collection.EnsureIndex(m => m.Id);
                }
                waiting = false;
                weStarted = false;
                toolStripStatusLabel5.Text = "";
                progressBar2.Value = 0;
                toolStripStatusLabel2.Text = "Тест окончен!";
                return;
            }
            waiting = false;
            weStarted = false;
            toolStripStatusLabel5.Text = "";
            progressBar2.Value = 0;
            toolStripStatusLabel2.Text = "Тест окончен!";
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var marksCol = db.GetCollection<StudentAnswers>("StudentAnswers");
                var marks = marksCol.Find(m => m.StudentId == FormHandler.StudentId);
                var countMarks = marks.Count();
                if (countMarks == 0)
                {
                    int correctAnswers = 0;
                    int totalAnswers = answers.Count;
                    for (int i = 0; i < totalAnswers; i++)
                    {
                        if (answers[i] == checkedListBox1.GetItemChecked(i))
                            correctAnswers += 1;
                    }
                    marksCol.Insert(new StudentAnswers()
                    {
                        Id = marksCol.Count() + 1,
                        StudentId = FormHandler.StudentId,
                        CorrectAnswers = correctAnswers,
                        TotalAnswers = totalAnswers
                    });
                    marksCol.EnsureIndex(m => m.Id);
                }
                else
                {
                    int correctAnswers = 0;
                    int totalAnswers = answers.Count;
                    for (int i = 0; i < totalAnswers; i++)
                    {
                        if (answers[i] == checkedListBox1.GetItemChecked(i))
                            correctAnswers += 1;
                    }
                    var lastRes = marks.Max(m => m.Id);
                    var dialogRes = MessageBox.Show(
                        "Бывший скор: " + marks.FirstOrDefault(m => m.Id == lastRes).CorrectAnswers + " из " + marks.FirstOrDefault(m => m.Id == lastRes).TotalAnswers + ", а сейчас... " +
                        correctAnswers + " из " + totalAnswers + ".\nСохранить?", "Вам не впервой!", MessageBoxButtons.OKCancel);
                    if (dialogRes == DialogResult.OK)
                    {
                        marksCol.Insert(new StudentAnswers()
                        {
                            Id = marksCol.Count() + 1,
                            CorrectAnswers = correctAnswers,
                            TotalAnswers = totalAnswers,
                            StudentId = FormHandler.StudentId
                        });
                        marksCol.EnsureIndex(x => x.Id);

                        zedGraphControl1.Visible = true;
                        zedGraphControl1.Refresh();
                        zedGraphControl1.GraphPane.Title.Text = "Предыдущие результаты";
                        PointPairList pointsTotal = new PointPairList();
                        PointPairList pointsCorrect = new PointPairList();
                        GraphPane pane = zedGraphControl1.GraphPane;
                        pane.CurveList.Clear();
                        var marksSorted = marks.OrderBy(m => m.Id).ToList();
                        marksSorted.Reverse();
                        for (int i = 0; i < Math.Min(5, marksSorted.Count); i++)
                        {
                            pointsTotal.Add(i, marksSorted[i].TotalAnswers);
                            pointsCorrect.Add(i, marksSorted[i].CorrectAnswers);
                        }
                        pane.XAxis.Title.Text = "Тест";
                        pane.YAxis.Title.Text = "Правильных ответов";
                        pane.AddBar("Всего", pointsTotal, Color.Blue);
                        pane.AddBar("Правильно", pointsCorrect, Color.Black);
                        zedGraphControl1.AxisChange();
                        zedGraphControl1.Invalidate();
                    }
                }
            }
            checkedListBox1.Items.Clear();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.Items.Count > 0)
            {
                using (StreamWriter sw = new StreamWriter("savedtest.tes"))
                    foreach (var item in checkedListBox1.Items)
                    {
                        sw.WriteLine(item);
                    }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            weStarted = true;
            startTime = DateTime.Now;
            fromExternalFile = true;
            using (StreamReader sr = new StreamReader("savedtest.tes"))
            {
                var lines = sr.ReadToEnd();
                foreach (var line in lines.Split('\n'))
                {
                    var pattern = new Regex(@"(\d+)\s([\+\-\*])\s(\d+)\s=\s(\d+)");
                    if (pattern.IsMatch(line))
                    {
                        var firstNum = Convert.ToInt32(pattern.Match(line).Groups[1].Value);
                        var secNum = Convert.ToInt32(pattern.Match(line).Groups[3].Value);
                        var op = pattern.Match(line).Groups[2].Value;
                        var res = Convert.ToInt32(pattern.Match(line).Groups[4].Value);
                        if (op == "+")
                        {
                            var actualRes = firstNum + secNum;
                            if (actualRes == res)
                                likelyhood.Add(true);
                            else
                            {
                                likelyhood.Add(false);
                            }
                               
                        }
                        else if (op == "*")
                        {
                            var actualRes = firstNum - secNum;
                            if (actualRes == res)
                                likelyhood.Add(true);
                            else
                            {
                                likelyhood.Add(false);
                            }
                        }
                        else
                        {
                            var actualRes = firstNum * secNum;
                            if (actualRes == res)
                                likelyhood.Add(true);
                            else
                            {
                                likelyhood.Add(false);
                            }
                        }
                    }
                }
                checkedListBox1.Items.Clear();
                var pattern2 = new Regex(@"(\d+)\s([\+\-\*])\s(\d+)\s=\s(\d+)");
                foreach (var line in lines.Split('\n'))
                {
                    if (pattern2.IsMatch(line))
                    {
                        checkedListBox1.Items.Add(line);
                    }
                }
            }
        }
    }
}
