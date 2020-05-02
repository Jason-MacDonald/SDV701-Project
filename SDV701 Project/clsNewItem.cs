﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDV701_Project
{
    [Serializable()]
    public class clsNewItem : clsItem
    {
        private int _WarrantyPeriod;

        [NonSerialized()]
        private static frmNewItem _NewItemDialog; // TODO: Need to get rid of this.

        public int WarrantyPeriod { get => _WarrantyPeriod; set => _WarrantyPeriod = value; }

        public override void EditDetails()
        {
            if(_NewItemDialog == null)
            {
                _NewItemDialog = frmNewItem.Instance;
            }
            _NewItemDialog.SetDetails(this);
        }
    }
}
