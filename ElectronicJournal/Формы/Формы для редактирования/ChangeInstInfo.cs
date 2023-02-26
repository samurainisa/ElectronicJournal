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
    public partial class ChangeInstInfo : MaterialForm
    {
        public trainings usersdb { get; set; }
        InstDBEntities1 db = new InstDBEntities1();

        public ChangeInstInfo(trainings usersdb)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.usersdb = usersdb;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usersdb.name = textBox4.Text;
            usersdb.description = richTextBox1.Text;

            db.Entry(usersdb).State = EntityState.Modified;
            db.SaveChanges();

            MessageBox.Show("Изменения сохранены");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ChangeInstInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //передать в главную форму
            var updatedinst = db.trainings.ToList();

            var mainForm = (MainForm)Application.OpenForms["MainForm"];
            mainForm.Trainings = updatedinst;
        }

        private void ChangeInstInfo_Load(object sender, EventArgs e)
        {
            textBox4.Text = usersdb.name;
            richTextBox1.Text = usersdb.description;
        }
    }
}