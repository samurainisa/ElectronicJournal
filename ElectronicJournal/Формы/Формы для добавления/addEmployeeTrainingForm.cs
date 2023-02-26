using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectronicJournal.Model;
using MaterialSkin.Controls;

namespace ElectronicJournal.Формы.Формы_для_добавления
{
    public partial class addEmployeeTrainingForm : MaterialForm
    {
        public addEmployeeTrainingForm()
        {
            InitializeComponent();
        }

        InstDBEntities1 db = new InstDBEntities1();

        private async void addEmployeeTrainingForm_Load(object sender, EventArgs e)
        {
            var employees = await Task.Run(() => db.employees.ToList());
            var trainings = await Task.Run(() => db.trainings.ToList());
            foreach (var employee in employees)
            {
                comboBox1.Items.Add($"Id {employee.id} " + $"Имя {employee.name}");
            }

            foreach (var training in trainings)
            {
                comboBox2.Items.Add($"Id {training.id} " + $"Инструктаж {training.name}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var employee = comboBox1.SelectedItem.ToString();
                var training = comboBox2.SelectedItem.ToString();

                string[] parts = employee.Split(' ');
                var employeeId = Convert.ToInt32(parts[1]);

                string[] parts2 = training.Split(' ');
                var trainingId = Convert.ToInt32(parts2[1]);

                var date = textBox3.Text;
                var code = textBox4.Text;

                if (employee == "" || training == "" || date == "" || code == "")
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }

                employee_training employeeTraining = new employee_training();
                employeeTraining.employee_id = employeeId;
                employeeTraining.training_id = trainingId;
                employeeTraining.completion_date = Convert.ToDateTime(date);
                employeeTraining.employee_code = Convert.ToInt32(code);

                db.employee_training.Add(employeeTraining);
                db.SaveChanges();
                MessageBox.Show("Мероприятие добавлено");
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void addEmployeeTrainingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form1 = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is MainForm) as MainForm;
            if (form1 != null)
            {
                form1.employee_training.DataSource = db.employee_training.ToList();
            }
        }
    }
}