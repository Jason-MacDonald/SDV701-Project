using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDV701_Project
{
    [Serializable]
    public class clsOrder
    {
        private int _InvoiceNumber;
        private string _InvoiceDate;
        private int _Quantity;
        private float _Price;
        private string _Name;
        private string _Email;

        private clsItem _Item;

        public int InvoiceNumber { get => _InvoiceNumber; set => _InvoiceNumber = value; }
        public string InvoiceDate { get => _InvoiceDate; set => _InvoiceDate = value; }
        public int Quantity { get => _Quantity; set => _Quantity = value; }
        public float Price { get => _Price; set => _Price = value; }
        public string Name { get => _Name; set => _Name = value; }
        public string Email { get => _Email; set => _Email = value; }
        public clsItem Item { get => _Item; set => _Item = value; }
    }
}
