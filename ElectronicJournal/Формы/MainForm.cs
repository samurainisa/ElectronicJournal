using ElectronicJournal.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ElectronicJournal.Формы
{
    public partial class MainForm : Form
    {
        InstDBEntities1 db = new InstDBEntities1();

        public MainForm()
        {
            InitializeComponent();

            dataGridView8.DataSource = db.USERS.ToList();
            dataGridView1.DataSource = db.violations.ToList();
            dataGridView2.DataSource = db.violation_resolution.ToList();
            dataGridView3.DataSource = db.employee_violation.ToList();
            dataGridView4.DataSource = db.employees.ToList();
            dataGridView5.DataSource = db.trainings.ToList();
            dataGridView6.DataSource = db.addresses.ToList();
            dataGridView7.DataSource = db.employee_training.ToList();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView6.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView7.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView8.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelTableConfirmForm excelTableConfirmForm = new ExcelTableConfirmForm();
            excelTableConfirmForm.ShowDialog();
            string tableName = excelTableConfirmForm.tableName;
            MessageBox.Show(tableName);

            if (tableName == "violations")
            {
                ExportToExcel(dataGridView1);
            }
            else if (tableName == "violation_resolution")
            {
                ExportToExcel(dataGridView2);
            }
            else if (tableName == "employee_violation")
            {
                ExportToExcel(dataGridView3);
            }
            else if (tableName == "employees")
            {
                ExportToExcel(dataGridView4);
            }
            else if (tableName == "trainings")
            {
                ExportToExcel(dataGridView5);
            }
            else if (tableName == "addresses")
            {
                ExportToExcel(dataGridView6);
            }
            else if (tableName == "employee_training")
            {
                ExportToExcel(dataGridView7);
            }
            else if (tableName == "USERS")
            {
                ExportToExcel(dataGridView8);
            }
        }

        private void ExportToExcel(DataGridView dataGridView)
        {
            // Создаем диалоговое окно для сохранения файла Excel
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Создаем приложение Excel
                Excel.Application excel = new Excel.Application();
                excel.Visible = false;

                // Создаем новую книгу и лист
                Excel.Workbook workbook = excel.Workbooks.Add();
                Excel.Worksheet worksheet = workbook.Sheets[1];

                // Заполняем ячейки заголовками столбцов
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView.Columns[i].HeaderText;
                }

                // Заполняем ячейки данными
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value;
                    }
                }

                // Автоматически подгоняем ширину столбцов
                worksheet.Columns.AutoFit();

                // Сохраняем книгу
                workbook.SaveAs(saveFileDialog.FileName);

                // Закрываем книгу и приложение Excel
                workbook.Close();
                excel.Quit();
            }
        }

        private void wordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WordExportForm wordExportForm = new WordExportForm();
            wordExportForm.ShowDialog();
            string tableName = wordExportForm.tableName;
            MessageBox.Show(tableName);

            if (tableName == "violations")
            {
                ExportToWord(dataGridView1);
            }
            else if (tableName == "violation_resolution")
            {
                ExportToWord(dataGridView2);
            }
            else if (tableName == "employee_violation")
            {
                ExportToWord(dataGridView3);
            }
            else if (tableName == "employees")
            {
                ExportToWord(dataGridView4);
            }
            else if (tableName == "trainings")
            {
                ExportToWord(dataGridView5);
            }
            else if (tableName == "addresses")
            {
                ExportToWord(dataGridView6);
            }
            else if (tableName == "employee_training")
            {
                ExportToWord(dataGridView7);
            }
            else if (tableName == "USERS")
            {
                ExportToWord(dataGridView8);
            }
        }

        private void ExportToWord(DataGridView dataGridView)
        {
            // Создаем экземпляр диалога выбора файла для сохранения
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Задаем параметры диалога выбора файла
            saveFileDialog.Filter = "Word Documents (*.docx)|*.docx";
            saveFileDialog.FileName = "Exported_Data.docx";
            saveFileDialog.Title = "Сохранить в Word";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Если пользователь выбрал место сохранения и подтвердил сохранение
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Создаем экземпляр приложения Word
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();

                // Создаем экземпляр документа Word
                Microsoft.Office.Interop.Word.Document document = word.Documents.Add();

                // Создаем экземпляр таблицы Word
                Microsoft.Office.Interop.Word.Table table = document.Tables.Add(document.Range(),
                    dataGridView.Rows.Count + 1, dataGridView.Columns.Count);

                // Заполняем первую строку названиями столбцов
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    table.Cell(1, i + 1).Range.Text = dataGridView.Columns[i].HeaderText;
                }

                // Заполняем все остальные ячейки
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        table.Cell(i + 2, j + 1).Range.Text = dataGridView.Rows[i].Cells[j].Value.ToString();
                    }
                }

                // Сохраняем документ в выбранное место
                document.SaveAs(saveFileDialog.FileName);

                // Закрываем документ Word
                document.Close();

                // Закрываем приложение Word
                word.Quit();
            }
        }
    }
}