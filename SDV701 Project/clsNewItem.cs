using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDV701_Project
{
    [Serializable()]
    public class clsNewItem : clsItem
    {
        // ##### Delegate #####
        public delegate void LoadNewItemFormDelegate(clsNewItem prNewItem);
        public static LoadNewItemFormDelegate LoadNewItemForm;

        private int _WarrantyPeriod;

        public int WarrantyPeriod { get => _WarrantyPeriod; set => _WarrantyPeriod = value; }

        public override void EditDetails()
        {
            LoadNewItemForm(this);
        }
    }
}
