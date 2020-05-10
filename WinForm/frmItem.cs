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
        protected clsItem _Item;

        public frmItem()
        {
            InitializeComponent();
        }

        public void SetDetails(clsItem prItem)
        {
            _Item = prItem;

            if (_Item.Name != null)
                txtName.Enabled = false;
            else
                txtName.Enabled = true;

            UpdateForm();
            ShowDialog();
        }

        #region ##### BUTTONS #####
        private void BtnUploadImage_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private async void BtnSaveAndClose_Click(object sender, EventArgs e)
        {
            PushData();
            if (txtName.Enabled)
            {
                MessageBox.Show(await ServiceClient.InsertItemAsync(_Item));
                frmMain.Instance.UpdateDisplay();
                txtName.Enabled = false;
            }
            else
                MessageBox.Show(await ServiceClient.UpdateItemAsync(_Item));
            frmCategory.Instance.UpdateForm();
            Hide();
        }

        private void BtnCloseWithoutSaving_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close without saving? Any changes you have made will not be saved.", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Hide();
            }
            //DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region ##### UPDATES #####
        protected virtual void PushData()
        {
            _Item.Name = txtName.Text;
            _Item.Description = txtDescription.Text;
            _Item.ModifiedDate = DateTime.Today.ToString();
            _Item.Motor = txtMotor.Text;
            _Item.Battery = txtBattery.Text;
            _Item.Quantity = Convert.ToInt32(txtQuantity.Text);
            _Item.Price = float.Parse(txtPrice.Text);
        }

        protected virtual void UpdateForm()
        {
            txtName.Text = _Item.Name;
            txtDescription.Text = _Item.Description;
            txtMotor.Text = _Item.Motor;
            txtQuantity.Text = _Item.Quantity.ToString();
            txtPrice.Text = _Item.Price.ToString();
        }
        #endregion
    }
}
