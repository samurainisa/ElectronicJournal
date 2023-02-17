using ElectronicJournal.Model;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectronicJournal.Формы.Формы_для_добавления;
using Excel = Microsoft.Office.Interop.Excel;
using MaterialSkin.Controls;
using System.Data;

namespace ElectronicJournal.Формы
{
    public partial class MainForm : MaterialForm
    {
        InstDBEntities1 db = new InstDBEntities1();

        public MainForm()
        {
            InitializeComponent();
            violations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            violations_resolution.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            employee_violatuion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            employee.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            trainings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            addresses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            employee_training.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            users.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public async Task PreloadFromDatabaseAsync()
        {
            users.DataSource = await Task.Run(() => db.USERS.ToList());
            violations.DataSource = await Task.Run(() => db.violations.ToList());
            violations_resolution.DataSource = await Task.Run(() => db.violation_resolution.ToList());
            employee_violatuion.DataSource = await Task.Run(() => db.employee_violation.ToList());
            employee.DataSource = await Task.Run(() => db.employees.ToList());
            trainings.DataSource = await Task.Run(() => db.trainings.ToList());
            addresses.DataSource = await Task.Run(() => db.addresses.ToList());
            employee_training.DataSource = await Task.Run(() => db.employee_training.ToList());
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //вы уверены что хотите выйти?
            DialogResult result = MessageBox.Show("Вы уверены что хотите выйти?", "Выход", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
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
            if (tableName == "Нарушения")
            {
                ExportToExcel(violations);
            }
            else if (tableName == "Устранение нарушений")
            {
                ExportToExcel(violations_resolution);
            }
            else if (tableName == "Мероприятия")
            {
                ExportToExcel(employee_violatuion);
            }
            else if (tableName == "Сотрудники")
            {
                ExportToExcel(employee);
            }
            else if (tableName == "Инструктажи")
            {
                ExportToExcel(trainings);
            }
            else if (tableName == "Адреса")
            {
                ExportToExcel(addresses);
            }
            else if (tableName == "Обучение персонала")
            {
                ExportToExcel(employee_training);
            }
            else if (tableName == "Пользователи")
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

            if (tableName == "Нарушения")
            {
                ExportToWord(violations);
            }
            else if (tableName == "Устранение нарушений")
            {
                ExportToWord(violations_resolution);
            }
            else if (tableName == "Мероприятия")
            {
                ExportToWord(employee_violatuion);
            }
            else if (tableName == "Сотрудники")
            {
                ExportToWord(employee);
            }
            else if (tableName == "Инструктажи")
            {
                ExportToWord(trainings);
            }
            else if (tableName == "Адреса")
            {
                ExportToWord(addresses);
            }
            else if (tableName == "Обучение персонала")
            {
                ExportToWord(employee_training);
            }
            else if (tableName == "Пользователи")
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
            if (tableName == "Нарушения")
            {
                PrintDataGridView(violations);
            }
            else if (tableName == "Устранение нарушений")
            {
                PrintDataGridView(violations_resolution);
            }
            else if (tableName == "Мероприятия")
            {
                PrintDataGridView(employee_violatuion);
            }
            else if (tableName == "Сотрудники")
            {
                PrintDataGridView(employee);
            }
            else if (tableName == "Инструктажи")
            {
                PrintDataGridView(trainings);
            }
            else if (tableName == "Адреса")
            {
                PrintDataGridView(addresses);
            }
            else if (tableName == "Обучение персонала")
            {
                PrintDataGridView(employee_training);
            }
            else if (tableName == "Пользователи")
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

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void пользовательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUserForm userform = new AddUserForm();
            userform.Show();
        }

        private void нарушениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddViolenceForm violenceform = new AddViolenceForm();
            violenceform.Show();
        }

        private void работникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEmployeesForm employeesform = new AddEmployeesForm();
            employeesform.Show();
        }

        private void адресаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAddressform frm = new AddAddressform();
            frm.Show();
        }

        private void инструктажиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddInstructForm frm = new AddInstructForm();
            frm.Show();
        }

        private void устранениенарушенийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddViolationResolutionForm frm = new AddViolationResolutionForm();
            frm.Show();
        }

        private void работникнарушениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addEmployeeTrainingForm frm = new addEmployeeTrainingForm();
            frm.Show();
        }

        private void обучениеперсоналаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTrainingEmployeeForm frm = new AddTrainingEmployeeForm();
            frm.Show();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await PreloadFromDatabaseAsync();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void users_Click(object sender, EventArgs e)
        {

        }

        private void users_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRowID = -1;
            if (e.RowIndex >= 0 && e.RowIndex < users.Rows.Count)
            {
                DataGridViewRow selectedRow = users.Rows[e.RowIndex];
                selectedRowID = (int)selectedRow.Cells["ID"].Value; // предполагается, что в таблице есть столбец с именем "ID"
            }

            // Удаление строки из базы данных
            if (selectedRowID > 0)
            {
                try
                {
                    var rowToDelete = db.USERS.Find(selectedRowID); // предполагается, что в базе данных есть таблица с именем "MyTable"
                    db.USERS.Remove(rowToDelete);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось удалить строку из базы данных. Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Обновление источника данных для DataGridView
                users.DataSource = db.USERS.ToList();
            }
        }
    }
}