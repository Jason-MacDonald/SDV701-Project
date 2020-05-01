using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDV701_Project
{
    public partial class frmItem : Form
    {
        public frmItem()
        {
            InitializeComponent();
        }

        public void Run()
        {
            Show();
        }

        private void BtnUploadImage_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void BtnSaveAndClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void BtnCloseWithoutSaving_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
