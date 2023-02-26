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
    public partial class ChangeAdressInfo : MaterialForm
    {
        public addresses usersdb { get; set; }
        InstDBEntities1 db = new InstDBEntities1();
        public ChangeAdressInfo(addresses adr)
        {
            this.usersdb = adr;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void ChangeAdressInfo_Load(object sender, EventArgs e)
        {
            var employees = db.employees.ToList();
            foreach (var employee in employees)
            {
                comboBox1.Items.Add(employee.name + " " + employee.name + " " + employee.position +
                                    $"(id = {employee.id})");
            }

            textBox2.Text = usersdb.address;
        }

        private void ChangeAdressInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //передать в главную форму
            var updatedadr = db.addresses.ToList();

            var mainForm = (MainForm)Application.OpenForms["MainForm"];
            mainForm.Addresses = updatedadr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usersdb.employee_id = Convert.ToInt32(comboBox1.Text.Split('=')[1].Trim(')'));
            usersdb.address = textBox2.Text;

            db.Entry(usersdb).State = EntityState.Modified;
            db.SaveChanges();

            MessageBox.Show("Изменения сохранены");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
