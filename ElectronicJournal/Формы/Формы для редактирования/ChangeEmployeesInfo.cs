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
    public partial class ChangeEmployeesInfo : MaterialForm
    {
        public employees usersdb { get; set; }
        InstDBEntities1 db = new InstDBEntities1();

        public ChangeEmployeesInfo(employees emp)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.usersdb = emp;
        }

        private void ChangeEmployeesInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //передать в главную форму
            var updatedemp = db.employees.ToList();

            var mainForm = (MainForm)Application.OpenForms["MainForm"];
            mainForm.Employees = updatedemp;
        }

        private void ChangeEmployeesInfo_Load(object sender, EventArgs e)
        {
            textBox3.Text = usersdb.name;
            textBox4.Text = usersdb.position;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usersdb.name = textBox3.Text;
            usersdb.position = textBox4.Text;

            db.Entry(usersdb).State = EntityState.Modified;
            db.SaveChanges();

            MessageBox.Show("Изменения сохранены");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}