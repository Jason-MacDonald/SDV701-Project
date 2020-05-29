using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm
{
    class PriceComparer : IComparer<clsItem>
    {
        public int Compare(clsItem prItemX, clsItem prItemY)
        {
            float lcPriceX = prItemX.Price;
            float lcPriceY = prItemY.Price;

            return lcPriceX.CompareTo(lcPriceY);
        }
    }
}
