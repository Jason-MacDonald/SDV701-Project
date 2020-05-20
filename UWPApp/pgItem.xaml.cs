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
    public sealed partial class pgItem : Page
    {
        private clsItem _Item;
        public clsItem Item { get => _Item; set => _Item = value; }

        public pgItem()
        {
            this.InitializeComponent();
        }

        // ##### NAVIGATION #####
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            try
            {
                if (e.Parameter != null)
                {
                    Item = await ServiceClient.GetItemAsync(e.Parameter.ToString());
                    UpdateForm();
                }
            }
            catch (Exception ex)
            {
                txtMessage.Text = ex.GetBaseException().ToString();
            }
            
        }


        #region ##### UPDATES #####
        public void UpdateForm()
        {
            lblItemName.Text = Item.Name;
            lblItemPrice.Text = Item.Description;
            lblItemMotor.Text = Item.Motor;
            lblItemDescription.Text = Item.Description;
            lblItemQty.Text = Item.Quantity.ToString();
        }

        #endregion
    }
}
