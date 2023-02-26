using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectronicJournal.Model;
using MaterialSkin.Controls;

namespace ElectronicJournal.Формы.Формы_для_редактирования
{
    public partial class ChangeViolationResolutionInfo : MaterialForm
    {
        public violation_resolution usersdb { get; set; }
        InstDBEntities1 db = new InstDBEntities1();

        public ChangeViolationResolutionInfo(violation_resolution viores)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            usersdb = viores;
        }


        private async void ChangeViolationResolutionInfo_Load(object sender, EventArgs e)
        {
            var violations = await Task.Run(() => db.violations.ToList());
            foreach (var violation in violations)
            {
                comboBox1.Items.Add($"Id {violation.id} " + $"{violation.description}");
            }

            textBox3.Text = usersdb.employee_code.ToString();
            textBox4.Text = usersdb.resolution_date.ToString();
            materialSingleLineTextField1.Text = usersdb.resolution;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                usersdb.resolution = materialSingleLineTextField1.Text;
                usersdb.employee_code = Convert.ToInt32(textBox3.Text);
                usersdb.resolution_date = Convert.ToDateTime(textBox4.Text);
                usersdb.violation_id = Convert.ToInt32(comboBox1.SelectedItem.ToString().Split(' ')[1]);
                db.Entry(usersdb).State = EntityState.Modified;
                db.SaveChanges();

                MessageBox.Show("Изменения сохранены");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }


        private void ChangeViolationResolutionInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //передать в главную форму
            var updatedvio = db.violation_resolution.ToList();
            
            var mainForm = (MainForm)Application.OpenForms["MainForm"];
            mainForm.ViolationsResolution = updatedvio;
        }
    }
}