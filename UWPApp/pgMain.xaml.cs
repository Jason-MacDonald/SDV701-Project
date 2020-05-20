using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace UWPApp
{
    public sealed partial class pgMain : Page
    {
        #region ##### CONSTRUCTOR #####
        public pgMain()
        {
            this.InitializeComponent();
        }
        #endregion

        #region ##### EVENTS #####
        private async void PgMain_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                lstCategories.ItemsSource = await ServiceClient.GetCategoryNamesAsync();
            }
            catch (Exception ex)
            {
                // TODO: Change to a client side error message.
                lblMessage.Text = ex.GetBaseException().ToString();
            }
        }
        #endregion

        #region ##### NAVIGATION #####
        public void OpenCategory()
        {
            if(lstCategories.SelectedItem != null)
            {
                Frame.Navigate(typeof(pgItems), lstCategories.SelectedItem);
            }
        }
        #endregion

        #region ##### CONTROL INTERACTION #####
        private void LstCategories_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            OpenCategory();
        }
        #endregion

        #region ##### BUTTONS #####
        private void BtnOpenSelectedCategory_Click(object sender, RoutedEventArgs e)
        {
            OpenCategory();
        }
        #endregion
    }
}
