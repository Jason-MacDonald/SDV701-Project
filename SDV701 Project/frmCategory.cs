using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDV701_Project
{
    public sealed partial class frmCategory : Form
    {
        // ##### TEMPORARY CODE #####
        private clsItemList _ItemList;
        private clsCategory _Category;

        // ### SINGLETON #####
        private frmCategory()
        {
            InitializeComponent();
        }
        public static readonly frmCategory Instance = new frmCategory();

        public void Run()
        {
            Show();
        }
        public void SetDetails(clsCategory prCategory)
        {
            _Category = prCategory;
            UpdateForm();
            UpdateDisplay();
            ShowDialog();
        }


        // ##### CONTROLLER INTERACTION #####
        private void LstCategories_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        // ##### BUTTONS #####
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            _ItemList.AddItem(cbChoice.Text);
            UpdateDisplay();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        // ##### UPDATES #####
        private void UpdateForm()
        {
            Text = _Category.Name;
            txtDescription.Text = _Category.Description;
            _ItemList = _Category.ItemList;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            
        }
    }
}
