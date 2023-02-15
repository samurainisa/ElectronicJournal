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
    public partial class WordExportForm : Form
    {
        public string tableName { get; set; }

        public WordExportForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tableName = domainUpDown1.SelectedItem.ToString();
            Close();
        }

    }
}
