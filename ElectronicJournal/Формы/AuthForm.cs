using ElectronicJournal.Model;
using MaterialSkin.Controls;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ElectronicJournal.Формы
{
    public partial class AuthForm : MaterialForm
    {
        private InstDBEntities1 db = new InstDBEntities1();

        public AuthForm()
        {
            InitializeComponent();
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                var login = textBox1.Text;
                var password = textBox2.Text;

                if (login == "" || password == "")
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }

                using (SHA256 hash = SHA256.Create())
                {
                    byte[] passwordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                    string hashedPassword = BitConverter.ToString(passwordHash).Replace("-", "");

                    var user = db.USERS.FirstOrDefault(u => u.username == login && u.password == hashedPassword);

                    if (user != null)
                    {
                        int accessLevel = user.access_level;

                        if (accessLevel == 1)
                        {
                            MessageBox.Show("Успешно, у вас 1 уровень доступа");
                            MainForm mainForm = new MainForm();
                            mainForm.Show();
                            Hide();
                        }
                        else if (accessLevel == 2)
                        {
                            MessageBox.Show("Успешно, у вас 2 уровень доступа");
                            MainForm mainForm = new MainForm();
                            mainForm.Show();
                            Hide();
                        }
                        else
                        {
                            MessageBox.Show("Неверен пароль или логин");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверен пароль или логин");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            RegistrationForm regform = new RegistrationForm();
            regform.Show();
            this.Hide();
        }
    }
}