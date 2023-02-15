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

namespace ElectronicJournal.Формы
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }
        Database Database = new Database();

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var password = textBox2.Text;

            using (SHA256 hash = SHA256.Create())
            {
                byte[] passwordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hashedPassword = BitConverter.ToString(passwordHash).Replace("-", "");

                SqlCommand command = new SqlCommand("SELECT access_level FROM USERS WHERE username = @username AND password = @password", Database.GetConnection());

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
                            MessageBox.Show("You have successfully logged in as a user with access level 1");
                            MainForm mainForm = new MainForm();
                            mainForm.Show();
                            Hide();

                        }
                        else if (accessLevel == 2)
                        {
                            MessageBox.Show("You have successfully logged in as a user with access level 2");
                            MainForm mainForm = new MainForm();
                            mainForm.Show();
                            Hide();
                        }
                        else
                        {
                            MessageBox.Show("You have successfully logged in, but your access level is not recognized");
                            MainForm mainForm = new MainForm();
                            mainForm.Show();
                            Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Login failed");
                    }
                }
            }

        }
    }
}
