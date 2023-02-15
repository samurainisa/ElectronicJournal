using ElectronicJournal.Model;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using Excel = Microsoft.Office.Interop.Excel;

namespace ElectronicJournal.Формы
{
    public partial class ExcelTableConfirmForm : Form
    {
        private DomainUpDown domainUpDown1;
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

        }
    }
}