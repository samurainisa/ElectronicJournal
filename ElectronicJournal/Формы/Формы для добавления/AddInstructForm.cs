using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectronicJournal.Model;
using MaterialSkin.Controls;

namespace ElectronicJournal.Формы.Формы_для_добавления
{
    public partial class AddInstructForm : MaterialForm
    {
        public AddInstructForm()
        {
            InitializeComponent();
        }

        InstDBEntities1 db = new InstDBEntities1();


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var description = richTextBox1.Text;
                var name = textBox4.Text;

                if (description == "" || name == "")
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }

                trainings tr = new trainings();
                tr.description = description;
                tr.name = name;

                db.trainings.Add(tr);
                db.SaveChanges();
                MessageBox.Show("Инструкция успешно добавлена");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void AddInstructForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form1 = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is MainForm) as MainForm;
            if (form1 != null)
            {
                form1.trainings.DataSource = db.trainings.ToList();
            }
        }

    }
}