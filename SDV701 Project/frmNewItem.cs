using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SDV701_Project
{
    public partial class frmNewItem : frmItem
    {
        private int _WarrantyPeriod;

        public frmNewItem()
        {
            InitializeComponent();
        }

        public int WarrantyPeriod { get => _WarrantyPeriod; set => _WarrantyPeriod = value; }

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
