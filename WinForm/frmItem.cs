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
    public partial class frmItem : Form
    {
        #region ##### VARIABLES #####
        private clsItem item;

        protected clsItem Item { get => item; set => item = value; }
        #endregion

        #region ##### CONSTRUCTOR #####
        public frmItem()
        {
            InitializeComponent();
        }
        #endregion

        #region ##### METHODS ######
        public void SetDetails(clsItem prItem)
        {
            Item = prItem;

            if (Item.Name != null)
                txtName.Enabled = false;
            else
                txtName.Enabled = true;

            UpdateForm();
            ShowDialog();
        }

        private async Task UpdateItemInDatabase()
        {
            try
            {
                MessageBox.Show(await ServiceClient.UpdateItemAsync(Item));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }

        private async Task InsertIntoDatabase()
        {
            try
            {
                MessageBox.Show(await ServiceClient.InsertItemAsync(Item));
                txtName.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }
        #endregion

        #region ##### BUTTONS #####
        private void BtnUploadImage_Click(object sender, EventArgs e)
        {
            // TODO: Implement image functionality.
            MessageBox.Show("Not Implemented");
        }

        private async void BtnSaveAndClose_Click(object sender, EventArgs e)
        {
            // TODO: Should only save if changed as modified date is getting updated even when no changes are made.
            PushData();
            if (txtName.Enabled)
            {
                await InsertIntoDatabase();
            }
            else
            {
                await UpdateItemInDatabase();
            }
            frmCategory.Instance.UpdateForm();
            Hide();
        }

        private void BtnCloseWithoutSaving_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close without saving? Any changes you have made will not be saved.", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Hide();
            }
        }

        private void FrmItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
        #endregion

        #region ##### UPDATES #####
        protected virtual void PushData()
        {
            Item.Name = txtName.Text;
            Item.Description = txtDescription.Text;
            Item.ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Immediately formates date for database.
            Item.Motor = txtMotor.Text;
            Item.Battery = txtBattery.Text;
            Item.Quantity = Convert.ToInt32(txtQuantity.Text);
            Item.Price = float.Parse(txtPrice.Text);
        }

        protected virtual void UpdateForm()
        {
            txtName.Text = Item.Name;
            txtDescription.Text = Item.Description;
            txtMotor.Text = Item.Motor;
            txtQuantity.Text = Item.Quantity.ToString();
            txtPrice.Text = Item.Price.ToString();
            lblModifiedDate.Text = Item.ModifiedDate;
        }
        #endregion
    }
}
