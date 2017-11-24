using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace lab7
{
    public partial class Form1 : Form
    {
        public List<string> ErrorFilesList;

        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.LabelEdit = false;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("Имя файла", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Номер плана", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Количество семестров", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Ступенька", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Общее число часов", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Сам.ч.", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Ауд.ч.", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Лекции", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Max экз.", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Оценка", -2, HorizontalAlignment.Center);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                item.Remove();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ErrorFilesList == null)
            {
                ErrorFilesList = new List<string>();
            }
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = openFileDialog1.FileNames.Length;
                progressBar1.Value = 0;
                var ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                var semPattern = new Regex("(\\d+)сем");
                // Читаем файлы 
                foreach (String file in openFileDialog1.FileNames)
                {
                    toolStripStatusLabel1.Text = file.Split('\\').Last();
                    ListViewItem sheet = new ListViewItem(file.Split('\\').Last(), 0);
                    
                    var ObjBook = ObjExcel.Workbooks.Open(file);
                    var ObjSheet = (Worksheet) ObjBook.Sheets[1];
                    try
                    {
                        var planNumber = new Regex("\\d{3}");
                        if (planNumber.Match(file.Split('\\').Last()).Value.ToString() == "")
                            throw new Exception();
                        sheet.SubItems.Add(planNumber.Match(file.Split('\\').Last()).Value.ToString());
                        for (int i = 1; i < 20; i++)
                        {
                            if (ObjSheet.Cells[i, 1].Value != null)
                            {
                                if (semPattern.IsMatch(ObjSheet.Cells[i, 1].Value))
                                {
                                    var numberOfSemesters =
                                        Regex.Match(ObjSheet.Cells[i, 1].Value.ToString(), semPattern.ToString()).Groups
                                            [1]
                                            .Value;
                                    sheet.SubItems.Add(numberOfSemesters.ToString());
                                    break;
                                }
                                else
                                {
                                    var numberOfSemesters = 0;
                                }
                            }
                        }

                        if (ObjSheet.Cells[1, 1].Value != null)
                        {
                            var degree = ObjSheet.Cells[1, 1].Value.ToString().Split(' ')[0];
                            sheet.SubItems.Add(degree);
                        }

                        int k = 40;
                        while (k < 150)
                        {
                            for (int l = 1; l < 10; l++)
                            {
                                if (ObjSheet.Cells[k, l].Value != null)
                                {
                                    if (ObjSheet.Cells[k, l].Value.ToString() == "Общ.ч.")
                                    {
                                        sheet.SubItems.Add(ObjSheet.Cells[k, l + 2].Value.ToString());
                                        sheet.SubItems.Add(ObjSheet.Cells[k + 1, l + 2].Value.ToString());
                                        sheet.SubItems.Add(ObjSheet.Cells[k + 2, l + 2].Value.ToString());
                                        sheet.SubItems.Add(ObjSheet.Cells[k + 3, l + 2].Value.ToString());
                                        for (int z = 1; z <= 10; z++)
                                        {
                                            if (ObjSheet.Cells[k + z, l].Value != null)
                                            {
                                                if (ObjSheet.Cells[k + z, l].Value.ToString().ToLower() == "экз")
                                                {
                                                    int max = 0;
                                                    for (int x = 1; x <= int.Parse(sheet.SubItems[2].Text); x++)
                                                    {
                                                        if (ObjSheet.Cells[k + z, l + 2 + x].Value != null)
                                                        {
                                                            if (
                                                                int.Parse(
                                                                    ObjSheet.Cells[k + z, l + 2 + x].Value.ToString()) >
                                                                max)
                                                            {
                                                                max =
                                                                    int.Parse(
                                                                        ObjSheet.Cells[k + z, l + 2 + x].Value.ToString());
                                                            }
                                                        }
                                                    }
                                                    sheet.SubItems.Add(max.ToString());
                                                    break;

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (k == 90)
                                k = 110;
                            else
                                k = k + 1;
                        }

                        //У бакалавров число лекций не должно быть больше 50 % от числа аудиторной нагрузки.
                        // У магистров число лекций не должно быть больше 30 % от числа аудиторной нагрузки.
                        //  У бакалавров максимальное число  экзаменов в семестре не должно быть больше 4 - х.
                        //  У магистров максимальное число экзаменов в семестре не должно быть больше 3 - х.

                        int lec = int.Parse(sheet.SubItems[7].Text);
                        int aud = int.Parse(sheet.SubItems[6].Text);
                        string deg = sheet.SubItems[3].Text;
                        int maxEx = int.Parse(sheet.SubItems[8].Text);

                        if (
                            lec <= aud*0.5 && deg[0] == 'Б' && maxEx <= 4 ||
                            lec <= aud*0.3 && deg[0] == 'М' && maxEx <= 3)
                        {
                            sheet.SubItems.Add("ВЕРНО");
                        }
                        else
                        {
                            sheet.SubItems.Add("НЕВЕРНО");
                        }

                        listView1.Items.Add(sheet);
                        ObjBook.Close(false);
                        ObjBook = null;
                        progressBar1.Value = progressBar1.Value + 1;
                    }
                    catch (Exception ex)
                    {
                        ErrorFilesList.Add(file);
                        sheet.SubItems.Add("ОШИБКА ЧТЕНИЯ");
                        listView1.Items.Add(sheet);
                        ObjBook.Close(false);
                        Marshal.ReleaseComObject(ObjBook);
                        Marshal.ReleaseComObject(ObjSheet);
                        ObjBook = null;
                    }
                }
                ObjExcel.Quit();
                Marshal.ReleaseComObject(ObjExcel);
                ObjExcel = null;
            }

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamWriter fs = new StreamWriter("badFiles.txt");
            foreach (var item in ErrorFilesList)
            {
                fs.WriteLine(item);
            }
            fs.Close();

            Microsoft.Office.Interop.Excel.Application oExcel;
            Workbook oBook;
            Worksheet oSheet;

            oExcel = new Microsoft.Office.Interop.Excel.Application();
            saveFileDialog1.ShowDialog();
            if (System.IO.File.Exists(saveFileDialog1.FileName))
                oBook = oExcel.Workbooks.Open(saveFileDialog1.FileName);
            else
                oBook = oExcel.Workbooks.Add(Type.Missing);

            oSheet = (Worksheet)oBook.ActiveSheet;
            int ind1 = 1;
            int ind2 = 1;
            foreach (ListViewItem item in listView1.Items)
            {
                var flag = true;
                foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                {
                    if (subItem.Text == "ОШИБКА ЧТЕНИЯ")
                    {
                        flag = false;
                    }
                }
                ind2 = 1;
                if (flag)
                    foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                    {
                        oSheet.Cells[ind1, ind2] = subItem.Text;
                        ind2 = ind2 + 1;
                    }
                ind1 = ind1 + 1;
            }

            oBook.Close(true, saveFileDialog1.FileName, Type.Missing);
            Marshal.ReleaseComObject(oSheet);
            Marshal.ReleaseComObject(oBook);
            oExcel.Quit();
            Marshal.ReleaseComObject(oExcel);
            
            oBook = null;
            oExcel = null;
        }
    }
}
