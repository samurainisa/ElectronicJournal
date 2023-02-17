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
    public partial class AddTrainingEmployeeForm : MaterialForm
    {
        public AddTrainingEmployeeForm()
        {
            InitializeComponent();
        }

        InstDBEntities1 db = new InstDBEntities1();

        private async void AddTrainingEmployeeForm_Load(object sender, EventArgs e)
        {
            var employees = await Task.Run(() => db.employees.ToList());
            var violations = await Task.Run(() => db.violations.ToList());
            foreach (var employee in employees)
            {
                comboBox1.Items.Add($"Id {employee.id} " + $"Имя {employee.name}");
            }

            foreach (var violation in violations)
            {
                comboBox2.Items.Add($"Id {violation.id} " + $"Описание {violation.description}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var employee = comboBox1.SelectedItem.ToString();
                var violation = comboBox2.SelectedItem.ToString();
                var date = textBox4.Text;

                if (employee == "" || violation == "" || date == "")
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }

                string[] parts = employee.Split(' ');
                var employeeId = Convert.ToInt32(parts[1]);

                string[] parts2 = violation.Split(' ');
                var violationId = Convert.ToInt32(parts2[1]);

                var employee_violation = new employee_violation
                {
                    employee_id = employeeId,
                    violation_id = violationId,
                    violation_date = Convert.ToDateTime(date)
                };

                db.employee_violation.Add(employee_violation);
                db.SaveChanges();
                MessageBox.Show("Данные успешно добавлены");
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}