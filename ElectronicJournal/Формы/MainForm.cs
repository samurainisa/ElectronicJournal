using ElectronicJournal.Model;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using ElectronicJournal.Формы.Формы_для_добавления;
using Excel = Microsoft.Office.Interop.Excel;
using MaterialSkin.Controls;

namespace ElectronicJournal.Формы
{
    public partial class MainForm : MaterialForm
    {
        InstDBEntities1 db = new InstDBEntities1();

        public MainForm()
        {
            InitializeComponent();

            users.DataSource = db.USERS.ToList();
            violations.DataSource = db.violations.ToList();
            violations_resolution.DataSource = db.violation_resolution.ToList();
            employee_violatuion.DataSource = db.employee_violation.ToList();
            employee.DataSource = db.employees.ToList();
            trainings.DataSource = db.trainings.ToList();
            addresses.DataSource = db.addresses.ToList();
            employee_training.DataSource = db.employee_training.ToList();

            violations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            violations_resolution.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            employee_violatuion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            employee.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            trainings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            addresses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            employee_training.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            users.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //
            // //добавление двух столбцов с кнопками для удаления и редактирования
            // AddButtons(violations);
            // AddButtons(violations_resolution);
            // AddButtons(employee_violatuion);
            // AddButtons(employee);
            // AddButtons(trainings);
            // AddButtons(addresses);
            // AddButtons(employee_training);
            // AddButtons(users);
        }
        //universal method for add 2 col and buttons for edit and delete
        private void AddButtons(DataGridView dataGridView)
        {
            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
            deleteButton.HeaderText = "Удалить";
            deleteButton.Text = "Удалить";
            deleteButton.UseColumnTextForButtonValue = true;
            dataGridView.Columns.Add(deleteButton);

            DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
            editButton.HeaderText = "Редактировать";
            editButton.Text = "Редактировать";
            editButton.UseColumnTextForButtonValue = true;
            dataGridView.Columns.Add(editButton);
        }   

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //вы уверены что хотите выйти?
            DialogResult result = MessageBox.Show("Вы уверены что хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                AuthForm authForm = new AuthForm();
                authForm.Show();
                this.Close();
            }
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelTableConfirmForm excelTableConfirmForm = new ExcelTableConfirmForm();
            excelTableConfirmForm.ShowDialog();
            string tableName = excelTableConfirmForm.tableName;

            if (tableName == "violations")
            {
                ExportToExcel(violations);
            }
            else if (tableName == "violation_resolution")
            {
                ExportToExcel(violations_resolution);
            }
            else if (tableName == "employee_violation")
            {
                ExportToExcel(employee_violatuion);
            }
            else if (tableName == "employees")
            {
                ExportToExcel(employee);
            }
            else if (tableName == "trainings")
            {
                ExportToExcel(trainings);
            }
            else if (tableName == "addresses")
            {
                ExportToExcel(addresses);
            }
            else if (tableName == "employee_training")
            {
                ExportToExcel(employee_training);
            }
            else if (tableName == "USERS")
            {
                ExportToExcel(users);
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
                if (MessageBox.Show("Файл успешно сохранен. Открыть файл?", "Уведомление", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    excel.Visible = true;
                }

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


            if (tableName == "violations")
            {
                ExportToWord(violations);
            }
            else if (tableName == "violation_resolution")
            {
                ExportToWord(violations_resolution);
            }
            else if (tableName == "employee_violation")
            {
                ExportToWord(employee_violatuion);
            }
            else if (tableName == "employees")
            {
                ExportToWord(employee);
            }
            else if (tableName == "trainings")
            {
                ExportToWord(trainings);
            }
            else if (tableName == "addresses")
            {
                ExportToWord(addresses);
            }
            else if (tableName == "employee_training")
            {
                ExportToWord(employee_training);
            }
            else if (tableName == "USERS")
            {
                ExportToWord(users);
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
                if (MessageBox.Show("Файл успешно сохранен. Открыть файл?", "Уведомление", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    word.Visible = true;
                }
                // Закрываем приложение Word
                word.Quit();
            }
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintTablesFormSelect print = new PrintTablesFormSelect();
            print.ShowDialog();
            string tableName = print.tableName;
            if (tableName == "violations")
            {
                PrintDataGridView(violations);
            }
            else if (tableName == "violation_resolution")
            {
                PrintDataGridView(violations_resolution);
            }
            else if (tableName == "employee_violation")
            {
                PrintDataGridView(employee_violatuion);
            }
            else if (tableName == "employees")
            {
                PrintDataGridView(employee);
            }
            else if (tableName == "trainings")
            {
                PrintDataGridView(trainings);
            }
            else if (tableName == "addresses")
            {
                PrintDataGridView(addresses);
            }
            else if (tableName == "employee_training")
            {
                PrintDataGridView(employee_training);
            }
            else if (tableName == "USERS")
            {
                PrintDataGridView(users);
            }
        }

        public static void PrintDataGridView(DataGridView dtg)
        {
            // Создаем объект PrintDocument
            PrintDocument printDocument = new PrintDocument();

            // Обработчик события PrintPage для печати содержимого DataGridView
            printDocument.PrintPage += (sender, e) =>
            {
                // Определяем область печати
                Rectangle printArea = e.MarginBounds;

                // Рисуем заголовок таблицы
                string headerText = dtg.Name;
                Font headerFont = new Font("Arial", 14, FontStyle.Bold);
                SizeF headerSize = e.Graphics.MeasureString(headerText, headerFont);
                RectangleF headerRect =
                    new RectangleF(printArea.Left, printArea.Top, printArea.Width, headerSize.Height);
                e.Graphics.DrawString(headerText, headerFont, Brushes.Black, headerRect, StringFormat.GenericDefault);

                // Рисуем содержимое таблицы
                int rowHeight = dtg.RowTemplate.Height;
                int y = (int)headerRect.Bottom + 10;
                foreach (DataGridViewRow row in dtg.Rows)
                {
                    int x = printArea.Left;
                    for (int i = 0; i < dtg.Columns.Count; i++)
                    {
                        DataGridViewCell cell = row.Cells[i];
                        Rectangle cellRect = new Rectangle(x, y, cell.Size.Width, rowHeight);
                        e.Graphics.DrawRectangle(Pens.Black, cellRect);
                        e.Graphics.DrawString(cell.FormattedValue.ToString(), dtg.Font, Brushes.Black, cellRect,
                            StringFormat.GenericDefault);
                        x += cell.Size.Width;
                    }

                    y += rowHeight;
                }
            };
            if (printDocument.DefaultPageSettings.Landscape)
            {
                printDocument.DefaultPageSettings.Landscape = false;
            }
            else
            {
                printDocument.DefaultPageSettings.Landscape = true;
            }
            // Открываем диалог печати и печатаем содержимое таблицы
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void пользовательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUserForm userform = new AddUserForm();
            userform.Show();

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}