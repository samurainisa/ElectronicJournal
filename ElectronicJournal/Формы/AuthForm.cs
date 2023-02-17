using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace ElectronicJournal.Формы
{
    public partial class AuthForm : MaterialForm
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        Database Database = new Database();

        private void button1_Click_1(object sender, EventArgs e)
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

                    SqlCommand command =
                        new SqlCommand(
                            "SELECT access_level FROM USERS WHERE username = @username AND password = @password",
                            Database.GetConnection());

                    command.Parameters.AddWithValue("@username", login);
                    command.Parameters.AddWithValue("@password", hashedPassword);

                    using (SqlConnection connection = Database.GetConnection())
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            int accessLevel = reader.GetInt32(0);

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
                                connection.Close();
                                Dispose();
                                return;
                            }
                        }
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