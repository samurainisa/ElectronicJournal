using System;
using System.Windows.Forms;

namespace ElectronicJournal.Формы
{
    public partial class PrintTablesFormSelect : Form
    {
        public string tableName { get; set; }

        public PrintTablesFormSelect()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tableName = domainUpDown1.SelectedItem.ToString();
            Close();
        }

        private void PrintTablesFormSelect_Load(object sender, EventArgs e)
        {

        }
    }
}
