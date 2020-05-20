using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPApp
{
    interface IItemControl
    {
        void PushData(clsItem prItem);
        void UpdateControl(clsItem prItem);
    }
}
