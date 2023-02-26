using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using ElectronicJournal.Model;
using MaterialSkin.Controls;

namespace ElectronicJournal.Формы.Формы_для_редактирования
{
    public partial class ChangeTrainingEmployeeInfo : MaterialForm
    {
        public employee_training usersdb { get; set; }
        InstDBEntities1 db = new InstDBEntities1();

        public ChangeTrainingEmployeeInfo(employee_training emptr)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.usersdb = emptr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedEmployee = comboBox1.SelectedItem.ToString();
            var selectedTraining = comboBox2.SelectedItem.ToString();
            var employeeId = selectedEmployee.Substring(3, selectedEmployee.IndexOf("Имя") - 4);
            var trainingId = selectedTraining.Substring(3, selectedTraining.IndexOf("Инструктаж") - 4);

            usersdb.employee_id = Convert.ToInt32(employeeId);
            usersdb.completion_date = Convert.ToDateTime(textBox3.Text);
            usersdb.employee_code = Convert.ToInt32(textBox4.Text);
            usersdb.training_id = Convert.ToInt32(trainingId);

            db.Entry(usersdb).State = EntityState.Modified;
            db.SaveChanges();

            MessageBox.Show("Изменения сохранены");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private async void ChangeTrainingEmployeeInfo_Load(object sender, EventArgs e)
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

            textBox3.Text = usersdb.completion_date.ToString();
            textBox4.Text = usersdb.employee_code.ToString();
        }

        private void ChangeTrainingEmployeeInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //передать в главную форму
            var updatedinst = db.employee_training.ToList();

            var mainForm = (MainForm)Application.OpenForms["MainForm"];
            mainForm.EmployeeTrainings = updatedinst;
        }
    }
}