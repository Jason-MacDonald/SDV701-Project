using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForm
{
    public sealed partial class frmCategory : Form
    {
        #region ##### VARIABLES #####
        private clsCategory _Category;
        private List<clsItem> _ItemList;

        public clsCategory Category { get => _Category; set => _Category = value; }
        public List<clsItem> ItemList { get => _ItemList; set => _ItemList = value; }
        #endregion

        #region ##### SINGLETON #####
        private frmCategory()
        {
            InitializeComponent();
        }
        public static readonly frmCategory Instance = new frmCategory();
        #endregion

        #region ##### METHODS #####
        public static void Run(string prCategoryName)
        {
            if (string.IsNullOrEmpty(prCategoryName))
            {
                Instance.SetDetails(new clsCategory());
            }
            else
            {
                Instance.GetCategoryFromDB(prCategoryName);
            }
            Instance.Show();
        }

        private async void GetCategoryFromDB(string prCategoryName)
        {
            try
            {
                SetDetails(await ServiceClient.GetCategoryAsync(prCategoryName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }          
        }

        public async void SetDetails(clsCategory prCategory)
        {
            Category = prCategory;

            try
            {
                ItemList = await ServiceClient.GetCategoryItemsAsync(Category.Name);
                UpdateForm();
                Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }          
        }

        private void DeleteItem(int prIndex)
        {
            //if (prIndex >= 0 && prIndex < ItemList.Count)
            //{
            //    if (MessageBox.Show("Are you sure?", "Deleting work", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        ItemList.DeleteWork(prIndex);
            //    }
            //}
        }
        private void OpenSelectedItemForm(clsItem prItem)
        {
            if (prItem != null)
            {
                switch(prItem.Type)
                {
                    case "new":
                        frmNewItem.Run(prItem);
                        break;
                    case "used":
                        frmUsedItem.Run(prItem);
                        break;
                }             
            }
        }
        #endregion

        #region ##### CONTROLLER INTERACTION #####
        private async void LstCategories_DoubleClick(object sender, EventArgs e)
        {
            string lcStringID = ItemList[lstItems.SelectedIndex].Id.ToString();

            try
            {
                clsItem lcItem = await ServiceClient.GetItemAsync(lcStringID);
                OpenSelectedItemForm(lcItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }           
        }
        #endregion

        #region ##### BUTTONS #####
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            clsItem lcItem = new clsItem();
            switch (cbChoice.Text)
            {
                case "New":
                    lcItem.Type = "new";
                    lcItem.Category = Category.Name;
                    frmNewItem.Run(lcItem);
                    break;
                case "Used":
                    lcItem.Type = "used";
                    lcItem.Category = Category.Name;
                    frmUsedItem.Run(lcItem);
                    break;
                default:
                    MessageBox.Show("No item type selected!");
                    break;
            }
            UpdateDisplay();
        }

        private async void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                clsItem lcItem = await ServiceClient.GetItemAsync(ItemList[lstItems.SelectedIndex].Id.ToString());
                OpenSelectedItemForm(lcItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }           
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(await ServiceClient.DeleteItemAsync(ItemList[lstItems.SelectedIndex].Id.ToString()));
                UpdateDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }          
        }

        private void FrmCategory_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
        #endregion

        #region ##### UPDATES #####
        public void UpdateForm()
        {
            Text = Category.Name;
            txtDescription.Text = Category.Description;
            UpdateDisplay();
        }

        private async void UpdateDisplay()
        {
            lstItems.DataSource = null;

            try
            {
                ItemList = await ServiceClient.GetCategoryItemsAsync(Category.Name);

                if(ItemList != null)
                {
                    lstItems.DataSource = FormatItemListRecord();
                }
                else
                {
                    MessageBox.Show("There are currently no items in this category.");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }           
        }

        private List<string> FormatItemListRecord()
        {
            List<string> lcItemNames = new List<string>();

            foreach (clsItem item in ItemList)
            {
                lcItemNames.Add(item.Id.ToString() + " " + item.Name);
            }

            return lcItemNames;
        }
        #endregion


    }
}
