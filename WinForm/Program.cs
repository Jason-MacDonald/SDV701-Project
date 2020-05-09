using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //clsNewItem.LoadNewItemForm = new clsNewItem.LoadNewItemFormDelegate(frmNewItem.Run);
            //clsUsedItem.LoadUsedItemForm = new clsUsedItem.LoadUsedItemFormDelegate(frmUsedItem.Run);

            Application.Run(frmMain.Instance);
        }
    }
}
