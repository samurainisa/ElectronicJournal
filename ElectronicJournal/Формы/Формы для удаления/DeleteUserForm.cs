using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectronicJournal.Model;
using MaterialSkin.Controls;

namespace ElectronicJournal.Формы.Формы_для_удаления
{
    public partial class DeleteUserForm : MaterialForm
    {
        InstDBEntities1 db = new InstDBEntities1();

        public DeleteUserForm()
        {
            InitializeComponent();
        }

        private void DeleteUserForm_Load(object sender, EventArgs e)
        {

        }
    }
}
