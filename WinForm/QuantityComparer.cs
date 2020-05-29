using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm
{
    class QuantityComparer : IComparer<clsItem>
    {
        public int Compare(clsItem prItemX, clsItem prItemY)
        {
            int lcQuantityX = prItemX.Quantity;
            int lcQuantityY = prItemY.Quantity;

            return lcQuantityX.CompareTo(lcQuantityY);
        }
    }
}
