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
    public partial class ChangeViolationInfo : MaterialForm
    {
        public violations usersdb { get; set; }
        InstDBEntities1 db = new InstDBEntities1();

        public ChangeViolationInfo(violations vio)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.usersdb = vio;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usersdb.description = richTextBox1.Text;
            usersdb.control_level = Convert.ToInt32(textBox3.Text);
            usersdb.due_date = Convert.ToDateTime(textBox4.Text);

            //update data in database using entity framework
            UpdateRecord(usersdb);

            MessageBox.Show("Изменения сохранены");
            Close();
        }

        private void ChangeViolationInfo_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = usersdb.description;
            textBox3.Text = usersdb.control_level.ToString();
            textBox4.Text = usersdb.due_date.ToString();
        }

        private void UpdateRecord<T>(T updatedEntity) where T : class
        {
            try
            {
                var entity = db.Entry(updatedEntity);
                if (entity.State == EntityState.Detached)
                {
                    var set = db.Set<T>();
                    T attachedEntity = set.Local.FirstOrDefault(e => db.Entry(e).Entity == updatedEntity);
                    if (attachedEntity != null)
                    {
                        var attachedEntry = db.Entry(attachedEntity);
                        attachedEntry.CurrentValues.SetValues(updatedEntity);
                    }
                    else
                    {
                        entity.State = EntityState.Modified;
                    }
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при обновлении записи: {ex.Message}");
            }
        }

        private async void ChangeViolationInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //при закрытии формы обновить форму violations в главном меню
            await Task.Run(() =>
            {
                var violations = db.violations.ToList();
                var violationsForm = Application.OpenForms.OfType<DataGridView>().FirstOrDefault();
                if (violationsForm != null) violationsForm.DataSource = violations;
                db.Dispose();
            });
        }
    }
}