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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pgMain : Page
    {
        public pgMain()
        {
            this.InitializeComponent();
        }

        private async void PgMain_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                lstCategories.ItemsSource = await ServiceClient.GetCategoryNamesAsync();
            }
            catch (Exception ex)
            {
                // TODO: Change to a client side error message.
                txtMessage.Text = ex.GetBaseException().ToString();
            }
        }

        // ##### NAVIGATION #####
        public void OpenCategory()
        {
            if(lstCategories.SelectedItem != null)
            {
                Frame.Navigate(typeof(pgItems), lstCategories.SelectedItem);
            }
        }


        // ##### CONTROL INTERACTION #####
        private void LstCategories_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            OpenCategory();
        }

        // ##### BUTTONS #####
        private void BtnOpenSelectedCategory_Click(object sender, RoutedEventArgs e)
        {
            OpenCategory();
        }
    }
}
