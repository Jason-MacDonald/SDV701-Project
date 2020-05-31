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
        protected override bool PushData()
        {
            if (ValidCondition())
            {
                Item.Condition = txtCondition.Text;
                Item.WarrantyPeriod = 0;
            }
            return base.PushData();
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();
            txtCondition.Text = Item.Condition;
        }

        protected override bool HasChanged()
        {
            if (base.HasChanged() || txtCondition.Text != Item.Condition)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region ##### VALIDATION #####
        public bool ValidCondition()
        {
            if (string.IsNullOrWhiteSpace(txtCondition.Text))
            {
                MessageBox.Show("Please enter the condition.");
                return false;
            }
            if(!txtCondition.Text.All(condition => char.IsLetter(condition)))
            {
                MessageBox.Show("The condition has invalid characters.");
                return false;
            }
            return true;
        }
        #endregion
    }
}
