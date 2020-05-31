using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace UWPApp
{
    public sealed partial class pgItems : Page
    {
        #region ##### VARIABLES #####
        private clsCategory _Category;
        private List<clsItem> _ItemList = new List<clsItem>();

        public clsCategory Category { get => _Category; set => _Category = value; }
        public List<clsItem> ItemList { get => _ItemList; set => _ItemList = value; }
        #endregion

        #region ##### CONSTRUCTOR #####
        public pgItems()
        {
            InitializeComponent();
        }
        #endregion

        #region ##### METHODS #####
        public void OpenSelectedItem()
        {
            try
            {
                int lcItemID = ItemList[lstItems.SelectedIndex].Id;
                Frame.Navigate(typeof(pgItem), lcItemID);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "(003) " + ex.GetBaseException().Message;
            }           
        }

        private async void SetItemListSource()
        {
            lstItems.ItemsSource = null;

            try
            {
                ItemList = await ServiceClient.GetItemsAsync(Category.Name);

                if (ItemList != null)
                {
                    List<string> lcItemNames = new List<string>();
                    foreach (clsItem item in ItemList)
                    {
                        lcItemNames.Add(item.Id.ToString() + " " + item.Name);
                    }
                    lstItems.ItemsSource = lcItemNames;
                }
                else
                {
                    lblMessage.Text = "This category does not currently contain any items.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.GetBaseException().Message;
            }          
        }
        #endregion

        #region ##### NAVIGATION #####
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            try
            {
                if (e.Parameter != null)
                {
                    string lcCategoryName = e.Parameter.ToString();
                    Category = await ServiceClient.GetCategoryAsync(lcCategoryName);
                    UpdateForm();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.GetBaseException().Message;
            }          
        }
        #endregion

        #region ##### CONTROL INTERACTIONS #####
        private void LstItems_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (lstItems.SelectedItem != null)
            {
                OpenSelectedItem();
            }
        }
        #endregion

        #region ##### BUTTONS #####
        private void BtnOpenSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            if (lstItems.SelectedItem != null)
            {
                OpenSelectedItem();
            }
            else
            {
                if (lstItems.ItemsSource != null)
                {
                    lblMessage.Text = "Please select a category from the list.";
                }
                else
                {
                    lblMessage.Text = "This category does not currently contain any items.";
                }
            }
        }
        #endregion

        #region ##### UPDATES #####
        public void UpdateForm()
        {
            lblCategoryName.Text = Category.Name;
            lblDescription.Text = Category.Description;
            lblMessage.Text = "";

            SetItemListSource();
        }
        #endregion
    }
}
