using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectronicJournal.Model;
using MaterialSkin.Controls;

namespace ElectronicJournal.Формы.Формы_для_редактирования
{
    public partial class ChangeUserInfo : MaterialForm
    {
        public USERS usersdb { get; set; }
        public InstDBEntities1 db = new InstDBEntities1();
        public ChangeUserInfo(USERS users)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.usersdb = users;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usersdb.username = textBox2.Text;
            usersdb.password = textBox3.Text;
            usersdb.access_level = Convert.ToInt32(textBox4.Text);

            db.Entry(usersdb).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            MessageBox.Show("Изменения сохранены");

            // Обновляем datagridview на главной форме
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ChangeUserInfo_Load(object sender, EventArgs e)
        {
            textBox2.Text = usersdb.username;
            textBox3.Text = usersdb.password;
            textBox4.Text = usersdb.access_level.ToString();
        }

        private void ChangeUserInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //передать в главную форму
            var updatedUsers = db.USERS.ToList();

            var mainForm = (MainForm)Application.OpenForms["MainForm"];
            if (mainForm != null) mainForm.Users = updatedUsers;
        }
    }
}