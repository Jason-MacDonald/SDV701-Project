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
                Instance.RefreshFormFromDB(prCategoryName);
            }
            Instance.Show();
        }

        private async void RefreshFormFromDB(string prCategoryName)
        {
            SetDetails(await ServiceClient.GetCategoryAsync(prCategoryName));
        }

        public async void SetDetails(clsCategory prCategory)
        {
            Category = prCategory;
            ItemList = await ServiceClient.GetCategoryItemsAsync(Category.Name);
            UpdateForm();
            Show();
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
            clsItem lcItem = await ServiceClient.GetItemAsync(lcStringID);
            OpenSelectedItemForm(lcItem);
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
            clsItem lcItem = await ServiceClient.GetItemAsync(ItemList[lstItems.SelectedIndex].Id.ToString());
            OpenSelectedItemForm(lcItem);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //TODO: Delete Item;
            UpdateDisplay();
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
            ItemList = await ServiceClient.GetCategoryItemsAsync(Category.Name);
            List<string> lcItemNames = new List<string>(); 
            foreach (clsItem item in ItemList)
            {
                lcItemNames.Add(item.Id.ToString() + " " + item.Name);
            }
            lstItems.DataSource = lcItemNames;
        }
        #endregion
    }
}
