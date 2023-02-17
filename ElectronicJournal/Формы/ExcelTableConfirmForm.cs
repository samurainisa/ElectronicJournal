using System;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace ElectronicJournal.Формы
{
    public partial class ExcelTableConfirmForm : MaterialForm
    {
        public string tableName { get; set; }
        public ExcelTableConfirmForm()
        {
            InitializeComponent();
        }
        private void ExcelTableConfirmForm_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
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