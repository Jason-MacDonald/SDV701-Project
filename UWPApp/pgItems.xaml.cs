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
            this.InitializeComponent();
        }
        #endregion

        #region ##### METHODS #####
        public void OpenSelectedItem()
        {
            int lcItemID = ItemList[lstItems.SelectedIndex].Id;

            if(lstItems.SelectedItem != null)
            {
                Frame.Navigate(typeof(pgItem), lcItemID);
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
                lblMessage.Text = ex.GetBaseException().ToString();
            }          
        }
        #endregion

        #region ##### CONTROL INTERACTIONS #####
        private void LstItems_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            OpenSelectedItem();
        }
        #endregion

        #region ##### BUTTONS #####
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Frame.Navigate(typeof(pgMain));
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.GetBaseException().ToString();
            }
        }

        private void BtnOpenSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            OpenSelectedItem();
        }
        #endregion

        #region ##### UPDATES #####
        public async void UpdateForm()
        {
            lblCategoryName.Text = Category.Name;
            lblDescription.Text = Category.Description;

            lstItems.ItemsSource = null;
            ItemList = await ServiceClient.GetCategoryItemsAsync(Category.Name);
            
            if(ItemList != null)
            {
                List<string> lcItemNames = new List<string>();
                foreach (clsItem item in ItemList)
                {
                    lcItemNames.Add(item.Id.ToString() + " " + item.Name);
                }
                lstItems.ItemsSource = lcItemNames;
            }           
        }
        #endregion
    }
}
