using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm
{
    class NameComparer : IComparer<clsItem>
    {
        public int Compare(clsItem prItemX, clsItem prItemY)
        {
            string lcNameX = prItemX.Name;
            string lcNameY = prItemY.Name;

            return lcNameX.CompareTo(lcNameY);
        }
    }
}
