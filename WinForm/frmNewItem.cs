using System;
using System.Linq;
using System.Windows.Forms;

namespace WinForm
{
    public sealed partial class frmNewItem : frmItem
    {
        #region ##### SINGLETON #####
        private frmNewItem()
        {
            InitializeComponent();
        }
        public static readonly frmNewItem Instance = new frmNewItem();
        #endregion

        #region ##### METHODS #####
        public static void Run(clsItem prNewItem)
        {
            Instance.SetDetails(prNewItem);
        }
        #endregion

        #region ##### UPDATES #####
        protected override bool PushData()
        {
            if (ValidWarrantyPeriod())
            {
                Item.WarrantyPeriod = Convert.ToInt32(txtWarrantyPeriod.Text);
                Item.Condition = null;
                if (base.PushData())
                {
                    return true;
                }
                else return false;
            }
            else
                return false;
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();
            txtWarrantyPeriod.Text = Item.WarrantyPeriod.ToString() ;
        }

        protected override bool HasChanged()
        {
            if (base.HasChanged() || txtWarrantyPeriod.Text != Item.WarrantyPeriod.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region ##### VALIDATION #####
        public bool ValidWarrantyPeriod()
        {
            if (string.IsNullOrWhiteSpace(txtWarrantyPeriod.Text))
            {
                MessageBox.Show("Please enter the warranty period.");
                return false;
            }
            if (!txtWarrantyPeriod.Text.All(warrantyPeriod => char.IsDigit(warrantyPeriod)))
            {
                MessageBox.Show("The warranty period has invalid characters.");
                return false;
            }
            return true;
        }
        #endregion
    }
}
