﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SDV701_Project
{
    public sealed partial class frmUsedItem : frmItem
    {
        // ##### Singleton #####
        private frmUsedItem()
        {
            InitializeComponent();
        }
        public static readonly frmUsedItem Instance = new frmUsedItem();

        public static void Run(clsUsedItem prUsedItem)
        {
            Instance.SetDetails(prUsedItem);
        }

        protected override void PushData()
        {
            base.PushData();
            clsUsedItem lcItem = (clsUsedItem)_Item; // Recast clsItem as clsNewItem
            lcItem.Condition = txtCondition.Text;
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();
            clsUsedItem lcItem = (clsUsedItem)_Item; // Recast clsItem as clsUsedItem
            txtCondition.Text = lcItem.Condition;
        }
    }
}
