using System;
using System.Windows.Forms;

namespace SDV701_Project
{
    public sealed partial class frmMain : Form
    {
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
            UpdateDisplay();
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

        private void OpenSelectedItemForm()
        {
            string lcKey = Convert.ToString(lstCategories.SelectedItem);
            if (lcKey != null)
            {
                EditCategory(lcKey);
                UpdateDisplay();
            }
        }

        private void EditCategory(string prKey)
        {
            clsCategory lcCategory; // TODO: Need to get rid of this.
            lcCategory = _CategoryList[prKey];

            if (lcCategory != null)
            {
                _CategoryList.EditCategory(lcCategory);
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        // ##### CONTROL INTERACTION #####
        private void LstCategories_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectedItemForm();
        }

        // ##### BUTTONS #####
        private void BtnOpenCurrentOrdersForm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
            UpdateDisplay();
        }

        private void BtnOpenSelectedCategory_Click(object sender, EventArgs e)
        {
            OpenSelectedItemForm();
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

        // ##### UPDATES #####
        private void UpdateDisplay()
        {
            string[] lcDisplayList = new string[_CategoryList.Count];

            lstCategories.DataSource = null;
            _CategoryList.Keys.CopyTo(lcDisplayList, 0);
            lstCategories.DataSource = lcDisplayList;
        }
    }
}
