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
                frmCategory.Run(lstCategories.SelectedItem as string);
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
            // TODO: Open Current Orders Form.
            //frmOrderList.Instance.Show();
            MessageBox.Show("Not Implemented!");          
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
            lstCategories.DataSource = await ServiceClient.GetCategoryNamesAsync();
        }
        #endregion
    }
}
