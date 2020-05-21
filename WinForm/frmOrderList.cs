using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public sealed partial class frmOrderList : Form
    {
        #region ##### VARIABLES #####
        private List<clsOrder> _OrderList = new List<clsOrder>();

        public List<clsOrder> OrderList { get => _OrderList; set => _OrderList = value; }
        #endregion

        #region ##### SINGLETON #####
        private frmOrderList()
        {
            InitializeComponent();
        }
        public static readonly frmOrderList Instance = new frmOrderList();
        #endregion

        #region ##### METHODS #####
        private void FrmOrderList_Shown(object sender, EventArgs e)
        {
            UpdateForm();
        }
        #endregion

        #region ##### BUTTONS #####
        private async void BtnDeleteOrder_Click(object sender, EventArgs e)
        {
            if(lvOrderList.FocusedItem != null)
            {
                //string lcSelectedID = OrderList[lstOrderList.SelectedIndex].InvoiceNumber.ToString();
                string lcSelectedID = OrderList[lvOrderList.FocusedItem.Index].InvoiceNumber.ToString();

                if (MessageBox.Show("Are you sure?", "Deleting order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        MessageBox.Show(await ServiceClient.DeleteOrderAsync(lcSelectedID));
                        UpdateForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.GetBaseException().Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("No order has been selected.");
            }          
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
        #endregion

        #region ##### UPDATES #####
        public async void UpdateForm()
        {
            try
            {
                OrderList = await ServiceClient.GetOrdersAsync();

                if (OrderList != null)
                {
                    // ListView https://stackoverflow.com/questions/11482501/populating-a-listview-multi-column
                    lvOrderList.View = View.Details;
                    lvOrderList.Clear();
                    lvOrderList.Columns.Add("Invoice Number");
                    lvOrderList.Columns.Add("Item");
                    lvOrderList.Columns.Add("Unit Price");
                    lvOrderList.Columns.Add("Quantity");
                    lvOrderList.Columns.Add("Total Price");

                    // Resize columns https://stackoverflow.com/questions/4802744/adjust-listview-columns-to-fit-with-winforms
                    int x = lvOrderList.Width / 5;

                    foreach (ColumnHeader column in lvOrderList.Columns)
                    {
                        column.Width = x;
                    }

                    foreach (clsOrder order in OrderList)
                    {
                        lvOrderList.Items.Add(new ListViewItem(new string[]
                        {
                            order.InvoiceNumber.ToString(),
                            order.ItemName,
                            order.Price.ToString(),
                            order.Quantity.ToString(),
                            (order.Quantity * order.Price).ToString()
                        }));
                    }

                    float lcTotal = 0;

                    foreach (clsOrder order in OrderList)
                    {
                        lcTotal += order.Price * order.Quantity;
                    }
                    lblTotal.Text = "$" + lcTotal.ToString();
                }
                else
                {
                    lvOrderList.Clear();
                    MessageBox.Show("There are currently no active orders.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }          
        }
        #endregion

        private void FrmOrderList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
