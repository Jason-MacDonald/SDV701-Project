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
        // ##### Delegate #####
        public delegate void LoadUsedItemFormDelegate(clsUsedItem prUsedItem);
        public static LoadUsedItemFormDelegate LoadUsedItemForm;

        private string _Condition;

        public string Condition { get => _Condition; set => _Condition = value; }
        public override void EditDetails()
        {
            LoadUsedItemForm(this);
        }
    }  
}
