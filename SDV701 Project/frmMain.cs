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
    public sealed partial class frmMain : Form
    {
        // ##### TEMPORARY CODE FOR TESTING #####

        // ##### SINGLETON ##### 
        private frmMain()
        {
            InitializeComponent();
        }
        private static readonly frmMain _Instance = new frmMain();

        public static frmMain Instance
        {
            get { return _Instance;  } // Part of the additional singleton setup for the applications first form (see program.cs line 19).           
        }

        // ##### CONTROL INTERACTION #####
        private void LstCategories_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        // ##### BUTTONS #####
        private void BtnOpenCurrentOrdersForm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void BtnOpenSelectedCategory_Click(object sender, EventArgs e)
        {
            frmItemList.Instance.Run();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
