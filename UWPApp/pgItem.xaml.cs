using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using System.Collections.Generic;

namespace UWPApp
{
    public sealed partial class pgItem : Page
    {
        #region USER CONTROL SELECTION #####
        private delegate void LoadItemControlDelegate(clsItem prItem);
        private Dictionary<string, Delegate> _ItemContent;
        private void DispatchItemContent(clsItem prItem)
        {
            _ItemContent[prItem.Type].DynamicInvoke(prItem);
            UpdateForm();
        }
        #endregion

        #region ##### VARIABLES #####
        private clsItem _Item;
        public clsItem Item { get => _Item; set => _Item = value; }
        #endregion

        #region ##### CONSTRUCTOR #####
        public pgItem()
        {
            InitializeComponent();

            _ItemContent = new Dictionary<string, Delegate>
            {
                {"new", new LoadItemControlDelegate(RunNewItem)},
                {"used", new LoadItemControlDelegate(RunUsedItem)}
            };
        }
        #endregion

        #region ##### METHODS #####
        private async void SubmitNewOrder()
        {
            MessageDialog lcMessageBox = new MessageDialog("Please confirm that you would like to place an order.");
            lcMessageBox.Commands.Add(new UICommand("Yes", async x => 
            {
                try
                {
                    await InsertNewOrderInDatabase();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.GetBaseException().Message;
                }
            }));
            lcMessageBox.Commands.Add(new UICommand("Cancel"));

            await lcMessageBox.ShowAsync();                    
        }

        private async void GetItemFromDB()
        {
            try
            {
                Item = await ServiceClient.GetItemAsync(Item.Id.ToString());
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.GetBaseException().Message;
            }
        }

        private async Task InsertNewOrderInDatabase()
        {
            // TODO: Concurrency - Consider stored procedure.
            
            clsItem lcItem = Item;
            Item.Quantity -= Convert.ToInt16(txtOrderQuantity.Text);

            try
            {
                await ServiceClient.UpdateItemAsync(lcItem);
                await ServiceClient.InsertOrderAsync(new clsOrder
                {
                    ItemName = Item.Name,
                    Quantity = Convert.ToInt16(txtOrderQuantity.Text),
                    Price = Item.Price,
                    Name = txtUserName.Text,
                    Email = txtUserEmail.Text
                });
                GetItemFromDB();
                OrderPlaced();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.GetBaseException().Message;
            }                      
        }

        private void OrderPlaced()
        {          
            UpdateForm();
            lblMessage.Text = "Your order has been placed successfully.";
            txtUserName.Text = "";
            txtUserEmail.Text = "";
            txtOrderQuantity.Text = "";           
        }
        #endregion

        #region ##### USER CONTROL #####
        private void RunNewItem(clsItem prItem)
        {
            ctcItemSpecs.Content = new ucNewItem();
        }

        private void RunUsedItem(clsItem prItem)
        {
            ctcItemSpecs.Content = new ucUsedItem();
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
                    DispatchItemContent(Item as clsItem);
                    //UpdateForm();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "(001) " + ex.GetBaseException().Message;
                }
            }
            else
            {
                // TODO: ?
                lblMessage.Text = "(002) ";
            }
            
        }
        #endregion

        #region ##### BUTTONS #####
        private void BtnPlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Proper validation.
            if(txtOrderQuantity.Text != "")
            {
                if (Item.Quantity - Convert.ToInt16(txtOrderQuantity.Text) >= 0)
                {
                    SubmitNewOrder();
                }
                else
                {
                    lblMessage.Text = "Unfortunately their is not currently enough stock to fill this order. Please reduce the quantity or contact our sales team.";
                }
            }
            lblMessage.Text = "Please enter the order quantity.";
        }
        #endregion

        #region ##### UPDATES #####
        public void UpdateForm()
        {
            lblMessage.Text = "";
            lblItemName.Text = Item.Name;
            lblItemPrice.Text = "$" + Item.Price.ToString();
            lblItemMotor.Text = Item.Motor;
            lblItemDescription.Text = Item.Description;
            lblItemQty.Text = Item.Quantity.ToString();
            (ctcItemSpecs.Content as IItemControl).UpdateControl(Item);
        }

        #endregion
    }
}
