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
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        Database Database = new Database();
        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var password = textBox2.Text;
            var access_level = textBox3.Text;

            using (SHA256 hash = SHA256.Create())
            {
                byte[] passwordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hashedPassword = BitConverter.ToString(passwordHash).Replace("-", "");

                SqlCommand command = new SqlCommand("INSERT INTO USERS (username, password, access_level) VALUES (@username, @password, @access_level)", Database.GetConnection());

                command.Parameters.AddWithValue("@username", login);
                command.Parameters.AddWithValue("@password", hashedPassword);
                command.Parameters.AddWithValue("@access_level", access_level);

                using (SqlConnection connection = Database.GetConnection())
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // If the query was successful, rowsAffected will be greater than 0
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("You have successfully registered");
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
            }

        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {


        }

        private void label4_Click(object sender, EventArgs e)
        {
            AuthForm authform = new AuthForm();
            authform.Show();
            this.Hide();
        }
    }
}
