using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWPApp
{
    public sealed partial class pgItem : Page
    {
        #region ##### VARIABLES #####
        private clsItem _Item;
        public clsItem Item { get => _Item; set => _Item = value; }
        #endregion

        #region ##### CONSTRUCTOR #####
        public pgItem()
        {
            InitializeComponent();
        }
        #endregion

        #region ##### NAVIGATION #####
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                try
                {

                    Item = await ServiceClient.GetItemAsync(e.Parameter.ToString());
                    UpdateForm();

                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.GetBaseException().ToString();
                }
            }
            else
            {
                // TODO: Create a new item.
            }
            
        }
        #endregion

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
