using Windows.UI.Xaml.Controls;

namespace UWPApp
{
    public sealed partial class ucUsedItem : UserControl, IItemControl
    {
        #region ##### CONSTRUCTOR #####
        public ucUsedItem()
        {
            InitializeComponent();
        }
        #endregion

        #region ##### UPDATES #####
        public void UpdateControl(clsItem prItem)
        {
            lblItemCondition.Text = prItem.Condition.ToString();
        }
        #endregion
    }
}
