using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDV701_Project
{
    [Serializable()]
    public class clsCategory
    {
        // ##### Delegate #####
        public delegate void LoadCategoryFormDelegate(clsCategory prCategory);
        public static LoadCategoryFormDelegate LoadCategoryForm;

        private string _Name;
        private string _Description;

        private clsItemList _ItemList;

        //private readonly static frmCategory _CategoryDialog = frmCategory.Instance; // TODO: Need to get rid of this.

        public string Name { get => _Name; set => _Name = value; }
        public string Description { get => _Description; set => _Description = value; }
        public clsItemList ItemList { get => _ItemList; set => _ItemList = value; }

        public clsCategory(string prName, string prDescription) // TODO: Could preserve whole object.
        {
            Name = prName;
            Description = prDescription;

            ItemList = new clsItemList();
        }
    }
}
