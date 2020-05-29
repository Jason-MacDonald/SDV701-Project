using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinForm
{
    public sealed partial class frmNewItem : frmItem
    {
        #region ##### SINGLETON #####
        private frmNewItem()
        {
            InitializeComponent();
        }
        public static readonly frmNewItem Instance = new frmNewItem();
        #endregion

        #region ##### METHODS #####
        public static void Run(clsItem prNewItem)
        {
            Instance.SetDetails(prNewItem);
        }
        #endregion

        #region ##### UPDATES #####
        protected override bool PushData()
        {
            if (ValidWarrantyPeriod())
            {
                Item.WarrantyPeriod = Convert.ToInt32(txtWarrantyPeriod);
            }
            return base.PushData();
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();
            txtWarrantyPeriod.Text = Item.WarrantyPeriod.ToString() ;
        }
        #endregion

        #region ##### VALIDATION #####
        public bool ValidWarrantyPeriod()
        {
            if (string.IsNullOrWhiteSpace(txtWarrantyPeriod.Text))
            {
                MessageBox.Show("Please enter the warranty period.");
                return false;
            }
            if (!txtWarrantyPeriod.Text.All(warrantyPeriod => char.IsDigit(warrantyPeriod)))
            {
                MessageBox.Show("The warranty period has invalid characters.");
                return false;
            }
            return true;
        }
        #endregion
    }
}
