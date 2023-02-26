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
using ElectronicJournal.Model;
using MaterialSkin.Controls;

namespace ElectronicJournal.Формы.Формы_для_редактирования
{
    public partial class ChangeTrainingEmployeeForm : MaterialForm
    {
        public employee_violation usersdb { get; set; }
        InstDBEntities1 db = new InstDBEntities1();

        public ChangeTrainingEmployeeForm(employee_violation empvio)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            usersdb = empvio;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedEmployee = comboBox1.SelectedItem.ToString();
            var selectedViolation = comboBox2.SelectedItem.ToString();
            var date = Convert.ToDateTime(textBox4.Text);

            var employeeId = Convert.ToInt32(selectedEmployee.Split(' ')[1]);
            var violationId = Convert.ToInt32(selectedViolation.Split(' ')[1]);
            var dateupdate = Convert.ToDateTime(date);

            usersdb.employee_id = employeeId;
            usersdb.violation_id = violationId;
            usersdb.violation_date = dateupdate;


            db.Entry(usersdb).State = EntityState.Modified;
            db.SaveChanges();

            MessageBox.Show("Изменения сохранены");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private async void ChangeTrainingEmployeeForm_Load(object sender, EventArgs e)
        {
            var employees = await Task.Run(() => db.employees.ToList());
            var violations = await Task.Run(() => db.violations.ToList());
            foreach (var employee in employees)
            {
                comboBox1.Items.Add($"Id {employee.id} " + $"Имя {employee.name}");
            }

            textBox4.Text = usersdb.violation_date.ToString();
            foreach (var violation in violations)
            {
                comboBox2.Items.Add($"Id {violation.id} " + $"Описание {violation.description}");
            }
        }

        private void ChangeTrainingEmployeeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //передать в главную форму
            var updatedvio = db.employee_violation.ToList();

            var mainForm = (MainForm)Application.OpenForms["MainForm"];
            mainForm.EmployeeViolations = updatedvio;
        }
    }
}