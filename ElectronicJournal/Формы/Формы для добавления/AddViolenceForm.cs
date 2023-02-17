using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectronicJournal.Model;
using MaterialSkin.Controls;

namespace ElectronicJournal.Формы.Формы_для_добавления
{
    public partial class AddViolenceForm : MaterialForm
    {
        public AddViolenceForm()
        {
            InitializeComponent();
        }

        InstDBEntities1 db = new InstDBEntities1();

        private void AddViolenceForm_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var description = richTextBox1.Text;
                var level = textBox3.Text;
                var date = textBox4.Text;



                if (description == "" || level == "" || date == "")
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }

                violations vio = new violations();
                vio.description = description;
                vio.control_level = Convert.ToInt32(level);
                vio.due_date = Convert.ToDateTime(date);

                db.violations.Add(vio);
                db.SaveChanges();

                MessageBox.Show("Нарушение успешно добавлено");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}