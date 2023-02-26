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
using System.Net;
using System.Collections.Generic;
using ElectronicJournal.Формы.Формы_для_редактирования;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using ElectronicJournal.Формы;
using System.Data.Entity;

namespace ElectronicJournal.Формы
{
    public partial class MainForm : MaterialForm
    {
        InstDBEntities1 db = new InstDBEntities1();
        public List<USERS> Users { get; set; }
        public List<violations> Violations { get; set; }
        public List<violation_resolution> ViolationsResolution { get; set; }
        public List<employee_violation> EmployeeViolations { get; set; }
        public List<employees> Employees { get; set; }
        public List<trainings> Trainings { get; set; }
        public List<addresses> Addresses { get; set; }
        public List<employee_training> EmployeeTrainings { get; set; }


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

        private async void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if ((users.CurrentRow != null && users.CurrentRow.Selected) && users.SelectedRows.Count == 1)
            {
                int rowIndex = users.CurrentRow.Index;
                int id = (int)users.Rows[rowIndex].Cells[0].Value;
                DeleteRecord<USERS>(id);
                // Обновляем источник данных для DataGridView
                users.DataSource = await GetTableAsync<USERS>();
                MessageBox.Show("Запись удалена");
            }

            if ((employee_training.CurrentRow != null && employee_training.CurrentRow.Selected) &&
                employee_training.SelectedRows.Count == 1)
            {
                int rowIndex = employee_training.CurrentRow.Index;
                int id = (int)employee_training.Rows[rowIndex].Cells[0].Value;
                DeleteRecord<employee_training>(id);
                // Обновляем источник данных для DataGridView
                employee_training.DataSource = await GetTableAsync<employee_training>();
                MessageBox.Show("Запись удалена");
            }

            if ((violations.CurrentRow != null && violations.CurrentRow.Selected) && violations.SelectedRows.Count == 1)
            {
                int rowIndex = violations.CurrentRow.Index;
                int id = (int)violations.Rows[rowIndex].Cells[0].Value;

                using (var db = new InstDBEntities1())
                {
                    var entity = db.violations.Find(id);
                    if (entity != null)
                    {
                        using (var transaction = db.Database.BeginTransaction())
                        {
                            try
                            {
                                db.Entry(entity).State = EntityState.Deleted;
                                db.SaveChanges();
                                transaction.Commit();
                                MessageBox.Show("Запись удалена");
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show($"Ошибка удаления записи: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Запись не найдена в базе данных.");
                    }
                }

                // Обновляем источник данных для DataGridView
                violations.DataSource = await GetTableAsync<violations>();
            }


            if ((violations_resolution.CurrentRow != null && violations_resolution.CurrentRow.Selected) &&
                violations_resolution.SelectedRows.Count == 1)
            {
                int rowIndex = violations_resolution.CurrentRow.Index;
                int id = (int)violations_resolution.Rows[rowIndex].Cells[0].Value;
                DeleteRecord<violation_resolution>(id);
                // Обновляем источник данных для DataGridView
                violations_resolution.DataSource = await GetTableAsync<violation_resolution>();
                MessageBox.Show("Запись удалена");
            }

            // employee_violatuion

            if ((employee_violatuion.CurrentRow != null && employee_violatuion.CurrentRow.Selected) &&
                employee_violatuion.SelectedRows.Count == 1)
            {
                int rowIndex = employee_violatuion.CurrentRow.Index;
                int id = (int)employee_violatuion.Rows[rowIndex].Cells[0].Value;
                DeleteRecord<employee_violation>(id);
                // Обновляем источник данных для DataGridView
                employee_violatuion.DataSource = await GetTableAsync<employee_violation>();
                MessageBox.Show("Запись удалена");
            }

            //employee

            if ((employee.CurrentRow != null && employee.CurrentRow.Selected) &&
                employee.SelectedRows.Count == 1)
            {
                int rowIndex = employee.CurrentRow.Index;
                int id = (int)employee.Rows[rowIndex].Cells[0].Value;

                using (var db = new InstDBEntities1())
                {
                    var entity = db.employees.Find(id);
                    if (entity != null)
                    {
                        using (var transaction = db.Database.BeginTransaction())
                        {
                            try
                            {
                                // Удалить связанные записи из связанных таблиц
                                var addresses = db.addresses.Where(a => a.employee_id == id);
                                foreach (var address in addresses)
                                {
                                    db.Entry(address).State = EntityState.Deleted;
                                }

                                var employeeViolations = db.employee_violation.Where(ev => ev.employee_id == id);
                                foreach (var employeeViolation in employeeViolations)
                                {
                                    db.Entry(employeeViolation).State = EntityState.Deleted;
                                }

                                var employeeTrainings = db.employee_training.Where(et => et.employee_id == id);
                                foreach (var employeeTraining in employeeTrainings)
                                {
                                    db.Entry(employeeTraining).State = EntityState.Deleted;
                                }

                                // Удалить запись из таблицы
                                db.Entry(entity).State = EntityState.Deleted;
                                db.SaveChanges();

                                transaction.Commit();
                                MessageBox.Show("Запись удалена");
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show($"Ошибка удаления записи: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Запись не найдена в базе данных.");
                    }
                }

                // Обновляем источник данных для DataGridView
                employee.DataSource = await GetTableAsync<employees>();
            }


            //trainings
            if ((trainings.CurrentRow != null && trainings.CurrentRow.Selected) &&
                trainings.SelectedRows.Count == 1)
            {
                int rowIndex = trainings.CurrentRow.Index;
                int id = (int)trainings.Rows[rowIndex].Cells[0].Value;
                DeleteRecord<trainings>(id);
                // Обновляем источник данных для DataGridView
                trainings.DataSource = await GetTableAsync<trainings>();
                MessageBox.Show("Запись удалена");
            }

            // addresses

            if ((addresses.CurrentRow != null && addresses.CurrentRow.Selected) &&
                addresses.SelectedRows.Count == 1)
            {
                int rowIndex = addresses.CurrentRow.Index;
                int id = (int)addresses.Rows[rowIndex].Cells[0].Value;
                DeleteRecord<addresses>(id);
                // Обновляем источник данных для DataGridView
                addresses.DataSource = await GetTableAsync<addresses>();
                MessageBox.Show("Запись удалена");
            }
        }

        private async Task<BindingSource> GetTableAsync<T>() where T : class
        {
            var table = await Task.Run(() => db.Set<T>().ToList());

            return new BindingSource(table, null);
        }

        private void DeleteRecord<T>(int id) where T : class
        {
            using (var db = new InstDBEntities1())
            {
                var entity = db.Set<T>().Find(id);
                if (entity != null)
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            // Удалить связанные записи из связанных таблиц
                            var properties = typeof(T).GetProperties()
                                .Where(p => p.PropertyType.Name.Contains("ICollection"));
                            foreach (var property in properties)
                            {
                                var relatedEntities = db.Entry(entity).Collection(property.Name).CurrentValue;
                                foreach (var relatedEntity in (IEnumerable<object>)relatedEntities)
                                {
                                    db.Entry(relatedEntity).State = EntityState.Deleted;
                                }
                            }

                            // Удалить запись из таблицы
                            db.Set<T>().Remove(entity);
                            db.SaveChanges();

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Ошибка удаления записи: {ex.Message}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Запись не найдена в базе данных.");
                }
            }
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            if ((users.CurrentRow != null && users.CurrentRow.Selected) && users.SelectedRows.Count == 1)
            {
                int rowIndex = users.CurrentRow.Index;
                int id = (int)users.Rows[rowIndex].Cells[0].Value;
                string username = (string)users.Rows[rowIndex].Cells[1].Value;
                string password = (string)users.Rows[rowIndex].Cells[2].Value;
                var access_level = Convert.ToInt32(users.Rows[rowIndex].Cells[3].Value);

                USERS user = new USERS();
                user.id = id;
                user.username = username;
                user.password = password;
                user.access_level = Convert.ToInt32(access_level);

                ChangeUserInfo changeUserInfo = new ChangeUserInfo(user);
                changeUserInfo.ShowDialog();

                if (Users != null)
                {
                    users.DataSource = Users;
                }
            }

            //violations
            if ((violations.CurrentRow != null && violations.CurrentRow.Selected) &&
                violations.SelectedRows.Count == 1)
            {
                int rowIndex = violations.CurrentRow.Index;
                int id = (int)violations.Rows[rowIndex].Cells[0].Value;
                string description = (string)violations.Rows[rowIndex].Cells[1].Value;
                var control_level = Convert.ToInt32(violations.Rows[rowIndex].Cells[2].Value);
                var date = Convert.ToDateTime(violations.Rows[rowIndex].Cells[3].Value);

                violations violation = new violations();
                violation.id = id;
                violation.description = description;
                violation.control_level = Convert.ToInt32(control_level);
                violation.due_date = Convert.ToDateTime(date);

                ChangeViolationInfo changeViolationInfo = new ChangeViolationInfo(violation);
                changeViolationInfo.ShowDialog();

                if (Violations != null)
                {
                    violations.DataSource = Violations;
                }
            }

            //violations_resolution
            if ((violations_resolution.CurrentRow != null && violations_resolution.CurrentRow.Selected) &&
                violations_resolution.SelectedRows.Count == 1)
            {
                // Получаем данные из таблицы
                int rowIndex = violations_resolution.CurrentRow.Index;
                int id = (int)violations_resolution.Rows[rowIndex].Cells[0].Value;
                int violation_id = (int)violations_resolution.Rows[rowIndex].Cells[1].Value;
                string resolution = (string)violations_resolution.Rows[rowIndex].Cells[2].Value;
                var date = Convert.ToDateTime(violations_resolution.Rows[rowIndex].Cells[3].Value);
                var employee_code = Convert.ToInt32(violations_resolution.Rows[rowIndex].Cells[4].Value);

                // Создаем экземпляр класса violation_resolution
                var violation = new violation_resolution
                {
                    id = id,
                    violation_id = violation_id,
                    resolution = resolution,
                    resolution_date = date,
                    employee_code = employee_code
                };

                // Открываем форму редактирования
                var changeViolationResolutionInfo = new ChangeViolationResolutionInfo(violation);
                var result = changeViolationResolutionInfo.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Получаем обновленные данные из формы
                    var updatedViolation = changeViolationResolutionInfo.usersdb;

                    // Находим в списке ViolationsResolution существующую сущность и обновляем ее значения
                    if (ViolationsResolution != null && ViolationsResolution.Count > 0)
                    {
                        var existingViolation = ViolationsResolution.FirstOrDefault(v => v.id == updatedViolation.id);
                        if (existingViolation != null)
                        {
                            // Обновляем существующую сущность
                            existingViolation.violation_id = updatedViolation.violation_id;
                            existingViolation.resolution = updatedViolation.resolution;
                            existingViolation.resolution_date = updatedViolation.resolution_date;
                            existingViolation.employee_code = updatedViolation.employee_code;
                        }
                        else
                        {
                            // Добавляем новую сущность
                            ViolationsResolution.Add(updatedViolation);
                        }
                    }

                    // Обновляем данные в таблице ViolationsResolution
                    violations_resolution.DataSource = null;
                    violations_resolution.DataSource = ViolationsResolution;
                }
            }

            //employee_violatuion
            if ((employee_violatuion.CurrentRow != null && employee_violatuion.CurrentRow.Selected) &&
                employee_violatuion.SelectedRows.Count == 1)
            {
                int rowIndex = employee_violatuion.CurrentRow.Index;
                int id = (int)employee_violatuion.Rows[rowIndex].Cells[0].Value;
                int employee_code = (int)employee_violatuion.Rows[rowIndex].Cells[1].Value;
                int violation_id = (int)employee_violatuion.Rows[rowIndex].Cells[2].Value;
                var date = Convert.ToDateTime(employee_violatuion.Rows[rowIndex].Cells[3].Value);

                employee_violation employeeViolation = new employee_violation();
                employeeViolation.id = id;
                employeeViolation.employee_id = employee_code;
                employeeViolation.violation_id = violation_id;
                employeeViolation.violation_date = date;

                ChangeTrainingEmployeeForm changeEmployeeViolationInfo =
                    new ChangeTrainingEmployeeForm(employeeViolation);
                changeEmployeeViolationInfo.ShowDialog();

                if (EmployeeViolations != null)
                {
                    employee_violatuion.DataSource = EmployeeViolations;
                }
            }

            //employee
            if ((employee.CurrentRow != null && employee.CurrentRow.Selected) &&
                employee.SelectedRows.Count == 1)
            {
                int rowIndex = this.employee.CurrentRow.Index;
                int id = (int)this.employee.Rows[rowIndex].Cells[0].Value;
                string name = (string)this.employee.Rows[rowIndex].Cells[1].Value;
                string position = (string)this.employee.Rows[rowIndex].Cells[2].Value;

                employees employee = new employees();
                employee.id = id;
                employee.name = name;
                employee.position = position;

                ChangeEmployeesInfo changeEmployeeInfo = new ChangeEmployeesInfo(employee);
                changeEmployeeInfo.ShowDialog();

                if (Employees != null)
                {
                    this.employee.DataSource = Employees;
                }
            }

            //trainings
            if ((trainings.CurrentRow != null && trainings.CurrentRow.Selected) &&
                trainings.SelectedRows.Count == 1)
            {
                int rowIndex = trainings.CurrentRow.Index;
                int id = (int)trainings.Rows[rowIndex].Cells[0].Value;
                string name = (string)trainings.Rows[rowIndex].Cells[1].Value;
                string description = (string)trainings.Rows[rowIndex].Cells[2].Value;

                trainings training = new trainings();
                training.id = id;
                training.name = name;
                training.description = description;

                ChangeInstInfo changeTrainingInfo = new ChangeInstInfo(training);
                changeTrainingInfo.ShowDialog();

                if (Trainings != null)
                {
                    this.trainings.DataSource = Trainings;
                }
            }

            //adresses
            if ((addresses.CurrentRow != null && addresses.CurrentRow.Selected) &&
                addresses.SelectedRows.Count == 1)
            {
                int rowIndex = addresses.CurrentRow.Index;
                int id = (int)addresses.Rows[rowIndex].Cells[0].Value;
                string adresss = (string)addresses.Rows[rowIndex].Cells[1].Value;
                var emp_id = (int)addresses.Rows[rowIndex].Cells[2].Value;

                addresses adress = new addresses();
                adress.id = id;
                adress.address = adresss;
                adress.employee_id = emp_id;

                ChangeAdressInfo changeAdressInfo = new ChangeAdressInfo(adress);
                changeAdressInfo.ShowDialog();

                if (Addresses != null)
                {
                    this.addresses.DataSource = Addresses;
                }
            }

            //employee_training
            if ((employee_training.CurrentRow != null && employee_training.CurrentRow.Selected) &&
                employee_training.SelectedRows.Count == 1)
            {
                int rowIndex = employee_training.CurrentRow.Index;
                int id = (int)employee_training.Rows[rowIndex].Cells[0].Value;
                int employee_id = (int)employee_training.Rows[rowIndex].Cells[1].Value;
                int training_id = (int)employee_training.Rows[rowIndex].Cells[2].Value;
                var date = Convert.ToDateTime(employee_training.Rows[rowIndex].Cells[3].Value);
                var empcode = Convert.ToInt32(employee_training.Rows[rowIndex].Cells[4].Value);


                employee_training employeeTraining = new employee_training();
                employeeTraining.id = id;
                employeeTraining.employee_id = employee_id;
                employeeTraining.training_id = training_id;
                employeeTraining.completion_date = date;
                employeeTraining.employee_code = empcode;


                ChangeTrainingEmployeeInfo changeEmployeeTrainingInfo =
                    new ChangeTrainingEmployeeInfo(employeeTraining);
                changeEmployeeTrainingInfo.ShowDialog();

                if (EmployeeTrainings != null)
                {
                    employee_training.DataSource = EmployeeTrainings;
                }
            }
        }
    }
}