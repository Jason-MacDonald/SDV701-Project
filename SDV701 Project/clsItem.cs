using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDV701_Project
{
    [Serializable()]
    public abstract class clsItem
    {
        private string _Name;
        private string _Description;
        private float _Price;
        private string _ModifiedDate;
        private int _Quantity;
        private string _Motor;

        public string Name { get => _Name; set => _Name = value; }
        public string Description { get => _Description; set => _Description = value; }
        public float Price { get => _Price; set => _Price = value; }
        public string ModifiedDate { get => _ModifiedDate; set => _ModifiedDate = value; }
        public int Quantity { get => _Quantity; set => _Quantity = value; }
        public string Motor { get => _Motor; set => _Motor = value; }

        public static clsItem NewItem(string prType)
        {
            switch (prType)
            {
                case "New": return new clsNewItem();
                case "Used": return new clsUsedItem();
                default: return null;
            }
        }
    }
}
