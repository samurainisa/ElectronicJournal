using ElectronicJournal.Model;
using System;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;

namespace ElectronicJournal.Формы
{
    public partial class ExcelTableConfirmForm : Form
    {
        private Label label1;
        private Button button1;
        InstDBEntities1 db = new InstDBEntities1();
        public string tableName { get; set; }

        public ExcelTableConfirmForm()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            tableName = domainUpDown1.SelectedItem.ToString();
            Close();
        }

        private void ExcelTableConfirmForm_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;

        }
    }
}