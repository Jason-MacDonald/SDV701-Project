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
        //private static Dictionary<clsCategory, frmCategory> _CategoryFormList = new Dictionary<clsCategory, frmCategory>();
        private static frmCategory _CategoryForm = new frmCategory();
        private clsCategory _Category;
        private clsItemList _ItemList;
        
        // ### SINGLETON #####
        private frmCategory()
        {
            InitializeComponent();
        }
        public static readonly frmCategory Instance = new frmCategory();

        public static frmCategory CategoryForm { get => _CategoryForm; set => _CategoryForm = value; }
        public clsCategory Category { get => _Category; set => _Category = value; }
        public clsItemList ItemList { get => _ItemList; set => _ItemList = value; }

        public static void Run(clsCategory prCategory)
        {
            CategoryForm.SetDetails(prCategory);
            CategoryForm.Show();
        }

        public void SetDetails(clsCategory prCategory)
        {
            Category = prCategory;
            UpdateForm();
            UpdateDisplay();
            Show();
        }

        private void EditItem(int prIndex)
        {
            if(prIndex>= 0 && prIndex < ItemList.Count)
            {
                ItemList.EditItem(prIndex);
            }
            else
            {
                MessageBox.Show("No Item Selected.");
            }
        }

        private void DeleteItem(int prIndex)
        {
            if (prIndex >= 0 && prIndex < ItemList.Count)
            {
                if (MessageBox.Show("Are you sure?", "Deleting work", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ItemList.DeleteWork(prIndex);
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
            ItemList.AddItem(cbChoice.Text);
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
            Hide();
        }

        // ##### UPDATES #####
        private void UpdateForm()
        {
            Text = Category.Name;
            txtDescription.Text = Category.Description;
            ItemList = Category.ItemList;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            lstItems.DataSource = null;
            lstItems.DataSource = ItemList;
        }
    }
}
