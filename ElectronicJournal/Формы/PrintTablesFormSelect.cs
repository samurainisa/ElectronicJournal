using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace ElectronicJournal.Формы
{
    public partial class PrintTablesFormSelect : MaterialForm
    {
        public string tableName { get; set; }

        public PrintTablesFormSelect()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (domainUpDown1.SelectedItem == null)
            {
                MessageBox.Show("Выберите таблицу");
                return;
            }
            tableName = domainUpDown1.SelectedItem.ToString();
            Close();
        }
    }
}
