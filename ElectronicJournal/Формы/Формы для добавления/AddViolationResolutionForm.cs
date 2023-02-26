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
    public partial class AddViolationResolutionForm : MaterialForm
    {
        public AddViolationResolutionForm()
        {
            InitializeComponent();
        }

        InstDBEntities1 db = new InstDBEntities1();

        private async void AddViolationResolutionForm_Load(object sender, EventArgs e)
        {
            var violations = await Task.Run(() => db.violations.ToList());
            foreach (var violation in violations)
            {
                comboBox1.Items.Add($"Id {violation.id} " + $"{violation.description}");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var resolution = materialSingleLineTextField1.Text;
                var violation = comboBox1.SelectedItem.ToString();

                string[] parts = violation.Split(' ');
                var violationId = Convert.ToInt32(parts[1]);

                var empcode = textBox3.Text;
                var resolution_date = textBox4.Text;

                if (resolution == "" || violation == "" || empcode == "" || resolution_date == "")
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }

                violation_resolution violationResolution = new violation_resolution();
                violationResolution.resolution = resolution;
                violationResolution.resolution_date = Convert.ToDateTime(resolution_date);
                violationResolution.violation_id = violationId;

                violationResolution.employee_code = Convert.ToInt32(empcode);

                db.violation_resolution.Add(violationResolution);
                db.SaveChanges();
                MessageBox.Show("Решение успешно добавлено");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}