using MaterialSkin.Controls;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ElectronicJournal.Формы
{
    public partial class RegistrationForm : MaterialForm
    {
        public RegistrationForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        Database Database = new Database();

        private void button1_Click_1(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var password = textBox2.Text;
            var access_level = textBox3.Text;

            if (login == "" || password == "" || access_level == "")
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (access_level != "1" && access_level != "2")
            {
                MessageBox.Show("Уровень доступа может быть только 1 или 2");
                return;
            }

            using (SHA256 hash = SHA256.Create())
            {
                byte[] passwordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hashedPassword = BitConverter.ToString(passwordHash).Replace("-", "");

                SqlCommand command =
                    new SqlCommand(
                        "INSERT INTO USERS (username, password, access_level) VALUES (@username, @password, @access_level)",
                        Database.GetConnection());

                command.Parameters.AddWithValue("@username", login);
                command.Parameters.AddWithValue("@password", hashedPassword);
                command.Parameters.AddWithValue("@access_level", access_level);

                using (SqlConnection connection = Database.GetConnection())
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Вы успешно зарегистрировались");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка");
                    }
                }
            }
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            AuthForm authform = new AuthForm();
            authform.Show();
            this.Hide();
        }
    }
}