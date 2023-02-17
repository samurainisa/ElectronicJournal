using ElectronicJournal.Model;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace ElectronicJournal.Формы.Формы_для_добавления
{
    public partial class AddUserForm : MaterialForm 
    {
        public AddUserForm()
        {
            InitializeComponent();
        }
        InstDBEntities1 db = new InstDBEntities1();
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                var id = db.USERS.Max(x => x.id) + 1;
                var login = textBox2.Text;
                var password = textBox3.Text;
                var access_level = textBox4.Text;

                //add user to database using entity framework
                USERS user = new USERS();
                user.id = id;
                user.username = login;
                user.password = password;
                user.access_level = Convert.ToInt32(access_level);

                //password must be hashed by sha256
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    user.password = BitConverter.ToString(hashedBytes).Replace("-", "");
                }


                if (user.username == "" || user.password == "" || user.access_level == 0)
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }

                if (db.USERS.Any(x => x.username == user.username))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует");
                    return;
                }

                db.USERS.Add(user);
                db.SaveChanges();
                MessageBox.Show("Пользователь успешно добавлен");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }
    }
}
