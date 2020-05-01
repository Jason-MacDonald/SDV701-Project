using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDV701_Project
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
            UpdateForm();
            ShowDialog();
        }

        // ##### BUTTONS #####

        private void BtnUploadImage_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void BtnSaveAndClose_Click(object sender, EventArgs e)
        {
            PushData();
            Close();
        }

        private void BtnCloseWithoutSaving_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // ##### UPDATES #####

        protected virtual void PushData()
        {
            _Item.Name = txtName.Text;
            _Item.Description = txtDescription.Text;
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
            txtBattery.Text = _Item.Battery;
            txtQuantity.Text = _Item.Quantity.ToString();
            txtPrice.Text = _Item.Price.ToString();
        }
    }
}
