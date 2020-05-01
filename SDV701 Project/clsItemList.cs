using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDV701_Project
{
    [Serializable()]
    public class clsItemList : List<clsItem>
    {
        public void AddItem(string prChoice)
        {
            clsItem lcItem = clsItem.NewItem(prChoice);
            if(lcItem != null)
            {
                Add(lcItem);
            }
        }

        public void EditItem(int prIndex)
        {
            clsItem lcItem = this[prIndex];
            lcItem.EditDetails();
        }

        internal void DeleteWork(int prIndex)
        {
            RemoveAt(prIndex);
        }
    }
}
