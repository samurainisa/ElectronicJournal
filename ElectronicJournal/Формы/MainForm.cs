using ElectronicJournal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronicJournal.Формы
{
    public partial class MainForm : Form
    {

        InstDBEntities1 db = new InstDBEntities1();
        public MainForm()
        {
            InitializeComponent();
            dataGridView1.DataSource= db.USERS.ToList();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}
