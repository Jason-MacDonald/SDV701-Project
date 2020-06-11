using Windows.UI.Xaml.Controls;

namespace UWPApp
{
    public sealed partial class ucNewItem : UserControl, IItemControl
    {
        #region ##### CONSTRUCTOR #####
        public ucNewItem()
        {
            InitializeComponent();
        }
        #endregion

        #region ##### UPDATES #####
        public void UpdateControl(clsItem prItem)
        {
            lblItemWarranty.Text = $"{prItem.WarrantyPeriod.ToString()} months";
        }
        #endregion
    }
}
