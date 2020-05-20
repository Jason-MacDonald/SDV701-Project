using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pgItems : Page
    {
        private clsCategory _Category;
        private List<clsItem> _ItemList = new List<clsItem>();

        public clsCategory Category { get => _Category; set => _Category = value; }
        public List<clsItem> ItemList { get => _ItemList; set => _ItemList = value; }

        public pgItems()
        {
            this.InitializeComponent();
        }  
        
        public void OpenSelectedItem()
        {
            int lcItemID = ItemList[lstItems.SelectedIndex].Id;

            if(lstItems.SelectedItem != null)
            {
                Frame.Navigate(typeof(pgItem), lcItemID);
            }
        }

        // ##### NAVIGATION #####
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
                txtMessage.Text = ex.GetBaseException().ToString();
            }          
        }

        // ##### BUTTONS #####
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Frame.Navigate(typeof(pgMain));
            }
            catch (Exception ex)
            {
                txtMessage.Text = ex.GetBaseException().ToString();
            }

        }

        private void BtnOpenSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            OpenSelectedItem();
        }

        private void LstItems_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            OpenSelectedItem();
        }

        // ##### UPDATES #####

        public async void UpdateForm()
        {
            txtName.Text = Category.Name;
            txtDescription.Text = Category.Description;

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
    }
}
