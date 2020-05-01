using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDV701_Project
{
    [Serializable()]
    public class clsUsedItem : clsItem
    {
        [NonSerialized()]
        private frmUsedItem _frmUsedItem;

        private string _Condition;

        public string Condition { get => _Condition; set => _Condition = value; }
    }
}
