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
        private string _Condition;

        [NonSerialized()]
        private frmUsedItem _UsedItemDialog;

       
        public string Condition { get => _Condition; set => _Condition = value; }
        public override void EditDetails()
        {
            if (_UsedItemDialog == null)
            {
                _UsedItemDialog = new frmUsedItem();
            }
            _UsedItemDialog.SetDetails(this);
        }
    }  
}
