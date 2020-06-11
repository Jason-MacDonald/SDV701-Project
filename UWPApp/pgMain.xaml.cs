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
            InitializeComponent();
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
                lblMessage.Text = "Error 001: " + ex.GetBaseException().Message;
            }
        }
        #endregion

        #region ##### NAVIGATION #####
        public void OpenCategory()
        {      
            try
            {
                Frame.Navigate(typeof(pgItems), lstCategories.SelectedItem);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error 002: " + ex.GetBaseException().Message;
            }                        
        }
        #endregion

        #region ##### CONTROL INTERACTION #####
        private void LstCategories_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (lstCategories.SelectedItem != null)
            {
                OpenCategory();
            }               
        }
        #endregion

        #region ##### BUTTONS #####
        private void BtnOpenSelectedCategory_Click(object sender, RoutedEventArgs e)
        {
            if (lstCategories.SelectedItem != null)
            {
                OpenCategory();
            }
            else
            {
                if(lstCategories.ItemsSource != null)
                {
                    lblMessage.Text = "Please select a category from the list.";
                }
                else
                {
                    lblMessage.Text = "Error 003: A connection with the server could not be established.";
                }
            }
        }
        #endregion
    }
}
