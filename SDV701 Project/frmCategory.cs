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
        private clsCategory _Category;
        private clsItemList _ItemList;
        
        // ### SINGLETON #####
        private frmCategory()
        {
            InitializeComponent();
        }
        public static readonly frmCategory Instance = new frmCategory();

        public void SetDetails(clsCategory prCategory)
        {
            _Category = prCategory;
            UpdateForm();
            UpdateDisplay();
            ShowDialog();
        }

        private void EditItem(int prIndex)
        {
            if(prIndex>= 0 && prIndex < _ItemList.Count)
            {
                _ItemList.EditItem(prIndex);
            }
            else
            {
                MessageBox.Show("No Item Selected.");
            }
        }

        private void DeleteItem(int prIndex)
        {
            if (prIndex >= 0 && prIndex < _ItemList.Count)
            {
                if (MessageBox.Show("Are you sure?", "Deleting work", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _ItemList.DeleteWork(prIndex);
                }
            }
        }

        // ##### CONTROLLER INTERACTION #####
        private void LstCategories_DoubleClick(object sender, EventArgs e)
        {
            int lcIndex = lstItems.SelectedIndex;
            if (lcIndex >= 0)
            {
                EditItem(lcIndex);
                UpdateDisplay();
            }
        }

        // ##### BUTTONS #####
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            _ItemList.AddItem(cbChoice.Text);
            UpdateDisplay();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            int lcIndex = lstItems.SelectedIndex;
            if(lcIndex >= 0)
            {
                EditItem(lcIndex);
                UpdateDisplay();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteItem(lstItems.SelectedIndex);
            UpdateDisplay();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
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
            lstItems.DataSource = null;
            lstItems.DataSource = _ItemList;
        }
    }
}
