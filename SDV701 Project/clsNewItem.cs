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
        private int _WarrantyPeriod;

        public int WarrantyPeriod { get => _WarrantyPeriod; set => _WarrantyPeriod = value; }
    }
}
