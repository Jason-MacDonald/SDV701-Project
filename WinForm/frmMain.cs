using System;
using System.Windows.Forms;

namespace WinForm
{
    public sealed partial class frmMain : Form
    {
        #region ##### SINGLETON ##### 
        private frmMain()
        {
            InitializeComponent();
        }
        private static readonly frmMain _Instance = new frmMain();

        public static frmMain Instance
        {
            get { return _Instance;  } // Part of the additional singleton setup for the applications first form (see program.cs line 19).           
        }
        #endregion

        #region ##### METHODS #####
        private void FrmMain_Load(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void OpenSelectedItemForm()
        {
            if (lstCategories.SelectedItem != null)
            {
                frmItems.Run(lstCategories.SelectedItem as string);
            }
            else
            {
                MessageBox.Show("No item has been selected.");
            }
        }
        #endregion

        #region ##### CONTROL INTERACTION #####
        private void LstCategories_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectedItemForm();
        }
        #endregion

        #region ##### BUTTONS #####
        private void BtnOpenCurrentOrdersForm_Click(object sender, EventArgs e)
        {
            frmOrderList.Instance.Show();        
            UpdateDisplay();
        }

        private void BtnOpenSelectedCategory_Click(object sender, EventArgs e)
        {
            OpenSelectedItemForm();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region ##### UPDATES #####
        public async void UpdateDisplay()
        {
            lstCategories.DataSource = null;

            try
            {
                lstCategories.DataSource = await ServiceClient.GetCategoryNamesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }          
        }
        #endregion
    }
}
