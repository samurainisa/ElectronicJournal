using System;
using System.Windows.Forms;

namespace ElectronicJournal.Формы
{
    public partial class WordExportForm : Form
    {
        public string tableName { get; set; }

        public WordExportForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tableName = domainUpDown1.SelectedItem.ToString();
            Close();
        }

        private void WordExportForm_Load(object sender, EventArgs e)
        {
        }
    }
}