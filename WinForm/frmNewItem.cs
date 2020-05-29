using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            // TODO: Implement additional warranty field.
            return base.PushData();
            //clsNewItem lcItem = (clsNewItem)_Item; // Recast clsItem as clsNewItem
            //lcItem.WarrantyPeriod = Convert.ToInt32(txtWarrantyPeriod.Text);
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();
            //clsNewItem lcItem = (clsNewItem)_Item; // Recast clsItem as clsNewItem
            txtWarrantyPeriod.Text = Item.WarrantyPeriod.ToString() ;
        }
        #endregion
    }
}
