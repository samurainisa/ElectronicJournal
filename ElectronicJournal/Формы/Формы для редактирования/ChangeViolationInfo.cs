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
    public partial class ChangeViolationInfo : MaterialForm
    {
        public violations usersdb { get; set; }
        InstDBEntities1 db = new InstDBEntities1();

        public ChangeViolationInfo(violations vio)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.usersdb = vio;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usersdb.description = richTextBox1.Text;
            usersdb.control_level = Convert.ToInt32(textBox3.Text);
            usersdb.due_date = Convert.ToDateTime(textBox4.Text);

            db.Entry(usersdb).State = EntityState.Modified;
            db.SaveChanges();

            MessageBox.Show("Изменения сохранены");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ChangeViolationInfo_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = usersdb.description;
            textBox3.Text = usersdb.control_level.ToString();
            textBox4.Text = usersdb.due_date.ToString();
        }

        private async void ChangeViolationInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //передать в главную форму
            var updatedvio = db.violations.ToList();

            var mainForm = (MainForm)Application.OpenForms["MainForm"];
            mainForm.Violations = updatedvio;
        }
    }
}