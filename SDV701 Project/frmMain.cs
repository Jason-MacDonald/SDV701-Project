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
        clsCategoryList _CategoryList;

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                _CategoryList = clsCategoryList.Retrieve();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Retrieve Error");
            }
            updateDisplay();
        }



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

        private void updateDisplay()
        {
            string[] lcDisplayList = new string[_CategoryList.Count];

            lstCategories.DataSource = null;
            _CategoryList.Keys.CopyTo(lcDisplayList, 0);
            lstCategories.DataSource = lcDisplayList;
        }

        // ##### CONTROL INTERACTION #####
        private void LstCategories_DoubleClick(object sender, EventArgs e)
        {
           
        }

        // ##### BUTTONS #####
        private void BtnOpenCurrentOrdersForm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
            updateDisplay();
        }

        private void BtnOpenSelectedCategory_Click(object sender, EventArgs e)
        {
            frmItemList.Instance.Run();
            updateDisplay();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                _CategoryList.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Save Error");
            }
            Close();
        }
    }
}
