using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinForm
{
    public sealed partial class frmUsedItem : frmItem
    { 
        #region ##### SINGLETON #####
        private frmUsedItem()
        {
            InitializeComponent();
        }
        public static readonly frmUsedItem Instance = new frmUsedItem();
        #endregion

        #region ##### METHODS #####
        public static void Run(clsItem prItem)
        {
            Instance.SetDetails(prItem);
        }
        #endregion

        #region ##### UPDATES #####
        protected override void PushData()
        {
            // TODO: Implement additional NewItem var condition. 
            base.PushData();
            //clsUsedItem lcItem = (clsUsedItem)_Item; // Recast clsItem as clsNewItem
            //lcItem.Condition = txtCondition.Text;
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();
            //clsUsedItem lcItem = (clsUsedItem)_Item; // Recast clsItem as clsUsedItem
            txtCondition.Text = Item.Condition;
        }
        #endregion
    }
}
