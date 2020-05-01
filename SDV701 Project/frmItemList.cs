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
    public sealed partial class frmItemList : Form
    {
        // ##### TEMPORARY CODE #####
        public frmItem pbItemForm = new frmItem();

        // ### SINGLETON #####
        private frmItemList()
        {
            InitializeComponent();
        }
        public static readonly frmItemList Instance = new frmItemList();

        public void Run()
        {
            Show();
        }

        // ##### CONTROLLER INTERACTION #####
        private void LstCategories_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        // ##### BUTTONS #####
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            pbItemForm.Run();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            pbItemForm.Run();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Instance.Hide();
        }
    }
}
