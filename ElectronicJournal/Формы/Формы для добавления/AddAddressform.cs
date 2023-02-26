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
    public partial class AddAddressform : MaterialForm
    {
        public AddAddressform()
        {
            InitializeComponent();
        }

        InstDBEntities1 db = new InstDBEntities1();

        private void AddAddressform_Load(object sender, EventArgs e)
        {
            var employees = db.employees.ToList();
            foreach (var employee in employees)
            {
                comboBox1.Items.Add(employee.name + " " + employee.name + " " + employee.position +
                                    $"(id = {employee.id})");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var address = textBox2.Text;
                var employee = comboBox1.SelectedItem.ToString();

                if (address == "" || employee == "")
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }

                var employeeId = Convert.ToInt32(employee.Substring(employee.IndexOf("id = ") + 5).TrimEnd(')'));
                var employeeObj = db.employees.FirstOrDefault(x => x.id == employeeId);
                if (employeeObj == null)
                {
                    MessageBox.Show("Сотрудник не найден");
                    return;
                }

                addresses addr = new addresses();
                addr.address = address;
                addr.employee_id = employeeId;

                db.addresses.Add(addr);
                db.SaveChanges();
                MessageBox.Show("Адрес успешно добавлен");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void AddAddressform_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form1 = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is MainForm) as MainForm;
            if (form1 != null)
            {
                form1.addresses.DataSource = db.addresses.ToList();
            }
        }
    }
}