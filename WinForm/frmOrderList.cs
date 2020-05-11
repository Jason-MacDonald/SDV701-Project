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
        private void FrmOrderList_Load(object sender, EventArgs e)
        {
            UpdateForm();
        }
        #endregion

        #region ##### BUTTONS #####
        private void BtnDeleteOrder_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented.");
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
        #endregion

        #region ##### UPDATES #####
        public async void UpdateForm()
        {
            lstOrderList.DataSource = null;
            lstOrderList.DataSource = await ServiceClient.GetOrdersAsync();

            OrderList = await ServiceClient.GetOrdersAsync();
            List<string> lcOrderString = new List<string>();
            foreach (clsOrder order in OrderList)
            {
                lcOrderString.Add(order.InvoiceNumber.ToString() + " " + order.ItemName + " $" + order.Price + " " + order.Quantity.ToString() + "units");
            }
            lstOrderList.DataSource = lcOrderString;
        }
        #endregion
    }
}
