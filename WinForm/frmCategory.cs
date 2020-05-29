using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForm
{
    public sealed partial class frmCategory : Form
    {
        private static PriceComparer priceComparer = new PriceComparer();
        private static NameComparer nameComparer = new NameComparer();
        private static QuantityComparer quantityComparer = new QuantityComparer();

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
            Instance.cbChoice.SelectedItem = "New";
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
            string lcStringID = ItemList[lvItemList.FocusedItem.Index].Id.ToString();

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
            //UpdateDisplay();
        }

        private async void BtnEdit_Click(object sender, EventArgs e)
        {
            if (lvItemList.FocusedItem != null)
            {
                if (lvItemList.FocusedItem.Selected)
                {
                    try
                    {
                        clsItem lcItem = await ServiceClient.GetItemAsync(ItemList[lvItemList.FocusedItem.Index].Id.ToString());
                        OpenSelectedItemForm(lcItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.GetBaseException().Message);
                    }
                }
                else
                {
                    MessageBox.Show("No item has been selected.");
                }
            }              
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if(lvItemList.FocusedItem != null)
            {
                if (lvItemList.FocusedItem.Selected)
                {
                    if (MessageBox.Show("Are you sure?", "Deleting order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            MessageBox.Show(await ServiceClient.DeleteItemAsync(ItemList[lvItemList.FocusedItem.Index].Id.ToString()));
                            UpdateDisplay();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.GetBaseException().Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No item has been selected.");
                }
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

        #region ##### SORT BUTTONS #####
        private void BtnSortByName_Click(object sender, EventArgs e)
        {
            ItemList.Sort(nameComparer);
            UpdateForm();
        }

        private void BtnSortByPrice_Click(object sender, EventArgs e)
        {
            ItemList.Sort(priceComparer);
            UpdateForm();
        }      

        private void BtnSortByQuantity_Click(object sender, EventArgs e)
        {
            ItemList.Sort(quantityComparer);
            UpdateForm();
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
            try
            {
                if(ItemList == null)
                    ItemList = await ServiceClient.GetCategoryItemsAsync(Category.Name);

                if (ItemList != null)
                {
                    // ListView https://stackoverflow.com/questions/11482501/populating-a-listview-multi-column
                    lvItemList.View = View.Details;
                    lvItemList.Clear();
                    lvItemList.Columns.Add("Name");
                    lvItemList.Columns.Add("Unit Price");
                    lvItemList.Columns.Add("In Stock");

                    // Resize columns https://stackoverflow.com/questions/4802744/adjust-listview-columns-to-fit-with-winforms
                    int x = lvItemList.Width / 3;

                    foreach (ColumnHeader column in lvItemList.Columns)
                    {
                        column.Width = x;
                    }

                    foreach (clsItem item in ItemList)
                    {
                        lvItemList.Items.Add(new ListViewItem(new string[]
                        {
                            item.Name,
                            "$" + item.Price.ToString(),
                            item.Quantity.ToString(),
                        }));
                    }
                }
                else
                {
                    lvItemList.Clear();
                    MessageBox.Show("There are currently no items in this category.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }        
        }

        #endregion
    }
}
