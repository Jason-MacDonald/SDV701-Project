﻿using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Media;

namespace UWPApp
{
    public sealed partial class pgItem : Page
    {
        #region ##### USER CONTROL SELECTION #####
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
                    clsItem tempItem = await ServiceClient.GetItemAsync(Item.Id.ToString());
                    // Checking to see if anyone else has changed the Items details since it was retrieved from the database.
                    if (tempItem.ModifiedDate != Item.ModifiedDate)
                    {
                        await new MessageDialog("The item has been modified. Please check the item details.").ShowAsync();
                        Item = tempItem;
                        UpdateForm();
                    }
                    else
                    {
                        await InsertNewOrderInDatabase();
                    }                 
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
            clsItem lcItem = new clsItem
            {
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

                if (lcResult == "success")
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
                    lblMessage.Text = "(004) " + ex.GetBaseException().Message;
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
            string lcInputUserName = txtUserName.Text;
            string lcInputEmail = txtUserEmail.Text;
            string lcInputQuantity = txtOrderQuantity.Text;

            if (ValidUserName(lcInputUserName))
            {
                if (ValidEmail(lcInputEmail))
                {
                    if (ValidQuantity(lcInputQuantity))
                    {
                        SubmitNewOrder();
                    }
                }
                else
                {
                    lblMessage.Text = "Please enter a valid email address.";
                }
            }
        }
        #endregion

        #region ##### UPDATES #####
        private void UpdateForm()
        {
            lblMessage.Text = "";
            lblItemName.Text = Item.Name;
            lblItemPrice.Text = "$" + Item.Price.ToString();
            lblItemMotor.Text = Item.Motor;
            lblItemDescription.Text = Item.Description;
            lblItemQty.Text = Item.Quantity.ToString();
            (ctcItemSpecs.Content as IItemControl).UpdateControl(Item);

            if(Item.Image != null)
            {
                picItem.Source = ConvertBytesToBitmapAsync(Item.Image).Result;
            }
            else
            {
                picItem.Source = null;
            }          
        }
        #endregion

        #region ##### VALIDATION #####

        #region ### USERNAME ###
        private bool ValidUserName(string prInputUserName)
        {
            if (string.IsNullOrWhiteSpace(prInputUserName))
            {
                lblMessage.Text = "Please enter your name.";
                return false;
            }

            if (!prInputUserName.All(name => char.IsLetter(name) || char.IsWhiteSpace(name)))
            {
                lblMessage.Text = "Your name contains invalid characters.";
                return false;
            }

            if (prInputUserName.Length >= 40)
            {
                lblMessage.Text = "Your name can not be longer than 40 characters.";
                return false;
            }

            return true;
        }
        #endregion

        #region ### EMAIL ###
        private bool ValidEmail(string prInputEmail) // https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        {
            if (string.IsNullOrWhiteSpace(prInputEmail))
            {
                lblMessage.Text = "Please enter your email address.";
                return false;
            }

            try
            {
                prInputEmail = Regex.Replace(prInputEmail, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    var domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException ex)
            {
                lblMessage.Text = "Error: 001 " + ex.GetBaseException().Message;
                return false;
            }
            catch (ArgumentException ex)
            {
                lblMessage.Text = "Error: 002 " + ex.GetBaseException().Message;
                return false;
            }

            try
            {
                return Regex.IsMatch(prInputEmail,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException ex)
            {
                lblMessage.Text = "Error: 003 " + ex.GetBaseException().Message;
                return false;
            }
        }
        #endregion

        #region ### QUANTITY ###
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

            if (Convert.ToInt16(prInputQuantity) <= 0)
            {
                lblMessage.Text = "Please enter a valid order quantity.";
                return false;
            }

            return true;
        }
        #endregion

        #endregion

        #region ##### IMAGE STREAM #####
        private async static Task<ImageSource> ConvertBytesToBitmapAsync(byte[] bytes)
        {
            BitmapImage image = new BitmapImage();
            
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(bytes.AsBuffer());
                stream.Seek(0);

                image.SetSourceAsync(stream);
            }
            return image;
        }
        #endregion
    }
}
