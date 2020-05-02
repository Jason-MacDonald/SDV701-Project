using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SDV701_Project
{
    public sealed partial class frmNewItem : frmItem
    {
        // ##### SINGLETON #####
        private frmNewItem()
        {
            InitializeComponent();
        }
        public static readonly frmNewItem Instance = new frmNewItem();

        public static void Run(clsNewItem prNewItem)
        {
            Instance.SetDetails(prNewItem);
        }

        protected override void PushData()
        {
            base.PushData();
            clsNewItem lcItem = (clsNewItem)_Item; // Recast clsItem as clsNewItem
            lcItem.WarrantyPeriod = Convert.ToInt32(txtWarrantyPeriod.Text);
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();
            clsNewItem lcItem = (clsNewItem)_Item; // Recast clsItem as clsNewItem
            txtWarrantyPeriod.Text = lcItem.WarrantyPeriod.ToString() ;
        }
    }
}
