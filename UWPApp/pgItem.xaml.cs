using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using System.Collections.Generic;
using System.Linq;

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
                UpdateForm();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.GetBaseException().Message;
            }
        }

        private async Task InsertNewOrderInDatabase()
        {
            // TODO: Concurrency - Consider stored procedure.

            clsItem lcItem = new clsItem{
                Id = Item.Id,
                Name = Item.Name,
                Category = Item.Category,
                Description = Item.Description,
                Price = Item.Price,
                ModifiedDate = Item.ModifiedDate,
                Quantity = Convert.ToInt32(txtOrderQuantity.Text),
                Motor = Item.Name,
                Battery = Item.Name,
                WarrantyPeriod = Item.WarrantyPeriod,
                Condition = Item.Condition,
                Type = Item.Type
            };

            try
            {
                string lcResult = await ServiceClient.UpdateItemQuantityAsync(lcItem);
                lcResult = lcResult.Trim('"');

                if(lcResult == "success")
                {
                    await ServiceClient.InsertOrderAsync(new clsOrder
                    {
                        ItemName = Item.Name,
                        Quantity = Convert.ToInt16(txtOrderQuantity.Text),
                        Price = Item.Price,
                        Name = txtUserName.Text,
                        Email = txtUserEmail.Text
                    });

                    OrderPlaced();
                    GetItemFromDB();

                    await new MessageDialog("Your order has been placed.").ShowAsync();
                }
                else
                {
                    await new MessageDialog("Unable to complete order. Please check we have the requested stock.").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.GetBaseException().Message;
            }          
        }

        private void OrderPlaced()
        {                    
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
            string lcInputQuantity = txtOrderQuantity.Text;

            if (ValidQuantity(lcInputQuantity))
            {
                SubmitNewOrder();
            }                 
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

        #region ##### VALIDATION #####
        private bool ValidQuantity(string prInputQuantity)
        {
            if (string.IsNullOrWhiteSpace(prInputQuantity))
            {
                lblMessage.Text = "Please enter the order quantity.";
                return false;
            }

            if (!prInputQuantity.All(char.IsDigit))
            {
                lblMessage.Text = "The order quantity contains invalid characters.";
                return false;
            }

            if(Convert.ToInt16(prInputQuantity) <= 0)
            {
                lblMessage.Text = "Please enter a valid order quantity.";
                return false;
            }

            return true;
        }
        #endregion
    }
}
