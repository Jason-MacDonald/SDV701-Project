using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDV701_Project
{
    public class clsOrderList : List<clsOrder>
    {
        public void AddOrder(clsOrder prOrder)
        {
            Add(prOrder);
        }

        public void DeleteOrder()
        {
            System.Windows.Forms.MessageBox.Show("Not Implemented.");
        }
    }
}
