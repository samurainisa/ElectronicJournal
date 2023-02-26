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
    public partial class AddEmployeesForm : MaterialForm
    {
        public AddEmployeesForm()
        {
            InitializeComponent();
        }

        InstDBEntities1 db = new InstDBEntities1();

        private void AddEmployeesForm_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var name = textBox3.Text;
                var position = textBox4.Text;

                if (name == "" || position == "")
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }

                employees emp = new employees();
                emp.name = name;
                emp.position = position;

                db.employees.Add(emp);
                db.SaveChanges();
                MessageBox.Show("Сотрудник успешно добавлен");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void AddEmployeesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form1 = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is MainForm) as MainForm;
            if (form1 != null)
            {
                form1.employee.DataSource = db.employees.ToList();
            }
        }
    }
}