using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double a = 0;
            double b = 0;
            bool bothOk = true;

            try
            {
                a = Convert.ToDouble(textBox1.Text);
            }
            catch (Exception)
            {
                bothOk = false;
                var errorIndexes = NumberParser.GetNumberInterval(textBox1.Text);
                var dialogResult = MessageBox.Show("Некорректные данные!", "Ошибка ввода чисел",
                    MessageBoxButtons.RetryCancel);
                if (dialogResult == DialogResult.Retry)
                {
                    textBox1.Focus();
                    textBox1.SelectionStart = errorIndexes.Item1;
                    textBox1.SelectionLength = errorIndexes.Item2;
                    return;
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    return;
                }
            }
            try
            {
                b = Convert.ToDouble(textBox2.Text);
            }
            catch (Exception)
            {
                bothOk = false;
                var dialogResult = MessageBox.Show("Некорректные данные!", "Ошибка ввода чисел",
                    MessageBoxButtons.RetryCancel);
                if (dialogResult == DialogResult.Retry)
                {
                    var errorIndexes = NumberParser.GetNumberInterval(textBox2.Text);
                    textBox2.Focus();
                    textBox2.SelectionStart = errorIndexes.Item1;
                    textBox2.SelectionLength = errorIndexes.Item2;
                    return;
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    return;
                }
            }
            if (bothOk)
            {
                try
                {
                    var res = OperationApplier.ApplyOperation(a, b, comboBox1.Text);
                    textBox5.Text = res.ToString();
                }
                catch (Exception ex)
                {
                    label10.Text = ex.Message;
                }

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "pi")
            {
                textBox6.Text = OperationApplier.ApplyOperation(0, 0, comboBox2.Text).ToString();
                return;
            }
            double a = 0;
            double b = 0;
            bool bothOk = true;

            try
            {
                a = Convert.ToDouble(textBox3.Text);
            }
            catch (Exception)
            {
                bothOk = false;
                var errorIndexes = NumberParser.GetNumberInterval(textBox3.Text);
                var dialogResult = MessageBox.Show("Некорректные данные!", "Ошибка ввода чисел",
                    MessageBoxButtons.RetryCancel);
                if (dialogResult == DialogResult.Retry)
                {
                    textBox3.Focus();
                    textBox3.SelectionStart = errorIndexes.Item1;
                    textBox3.SelectionLength = errorIndexes.Item2;
                    return;
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    textBox3.Text = "";
                    textBox3.Text = "";
                    return;
                }
            }
            var op = comboBox2.Text;
            if (op == "Абсолютная величина" || op == "Arccos" || op == "Arctg" || op == "Cos" || op == "Tg" ||
                op == "Exp" || op == "log10" || op == "Округление" || op == "sqrt")
            {
                try
                {
                    textBox6.Text = OperationApplier.ApplyOperation(a, 0, op).ToString();
                    return;
                }
                catch (Exception ex)
                {
                    label10.Text = ex.Message;
                }
            }

            try
            {
                b = Convert.ToDouble(textBox4.Text);
            }
            catch (Exception)
            {
                bothOk = false;
                var dialogResult = MessageBox.Show("Некорректные данные!", "Ошибка ввода чисел",
                    MessageBoxButtons.RetryCancel);
                if (dialogResult == DialogResult.Retry)
                {
                    var errorIndexes = NumberParser.GetNumberInterval(textBox4.Text);
                    textBox4.Focus();
                    textBox4.SelectionStart = errorIndexes.Item1;
                    textBox4.SelectionLength = errorIndexes.Item2;
                    return;
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    textBox3.Text = "";
                    textBox4.Text = "";
                    return;
                }
            }
            if (bothOk)
            {
                try
                {
                    var res = OperationApplier.ApplyOperation(a, b, comboBox2.Text);
                    if (Double.IsNaN(res))
                    {
                        MessageBox.Show("Некорректные данные!", "Ошибка ввода чисел",
                            MessageBoxButtons.RetryCancel);

                    }
                    textBox6.Text = res.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
                }

            }

        }
    }
}
