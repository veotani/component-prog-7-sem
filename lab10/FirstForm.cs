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
using System.Windows.Forms.VisualStyles;
using LiteDB;

namespace lab10
{
    public partial class FirstForm : Form
    {
        public FirstForm()
        {
            InitializeComponent();
        }

        public void DeleteAllSymbolsButDigits(Control textBox)
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            textBox.Text = digitsOnly.Replace(textBox.Text, "");
        }

        private void pasportSerialTextBox_TextChanged(object sender, EventArgs e)
        {
            DeleteAllSymbolsButDigits(pasportSerialTextBox);
        }

        private void surnameTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void pasportNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            DeleteAllSymbolsButDigits(pasportNumberTextBox);
        }

        private void proceedButton_Click(object sender, EventArgs e)
        {
            // Open database (or create if not exits)
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var students = db.GetCollection<Student>("students");
                var thisStudent = students.Find(m =>
                    m.PasportNumber == Convert.ToInt32(pasportNumberTextBox.Text) &&
                    m.PasportSerial == Convert.ToInt32(pasportSerialTextBox.Text) && m.Surname == surnameTextBox.Text
                );
                if (thisStudent.LongCount() == 0)
                {
                    students.Insert(new Student()
                    {
                        PasportNumber = Convert.ToInt32(pasportNumberTextBox.Text),
                        PasportSerial = Convert.ToInt32(pasportSerialTextBox.Text),
                        Surname = surnameTextBox.Text
                    });
                }
                students.EnsureIndex(x => x.Surname);
                FormHandler.StudentId = students.Find(m =>
                    m.PasportNumber == Convert.ToInt32(pasportNumberTextBox.Text) &&
                    m.PasportSerial == Convert.ToInt32(pasportSerialTextBox.Text) && m.Surname == surnameTextBox.Text
                ).First().Id;
            }
            this.Hide();
            var testForm = new TestForm();
            testForm.Show();
            FormHandler.firstForm = this;
        }
    }
}
