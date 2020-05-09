using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost
{
    //note controller must end with controller!
    public class ElectrifyController : System.Web.Http.ApiController
    {
        // ##### CATEGORY #####
        public List<string> GetCategoryNames()
        {
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT name FROM category", null);
            List<string> lcNames = new List<string>();
            foreach (DataRow dr in lcResult.Rows)
                lcNames.Add((string)dr[0]);
            return lcNames;
        }
        public clsCategory GetCategory(string Name)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("Name", Name);
            DataTable lcResult =
                clsDbConnection.GetDataTable("SELECT * FROM category WHERE Name = @Name", par);
            if (lcResult.Rows.Count > 0)
                return new clsCategory()
                {
                    Name = (string)lcResult.Rows[0]["Name"],
                    Description = (string)lcResult.Rows[0]["Description"]
                };
            else
                return null;
        }

        // ##### ITEM #####
        // GET
        public List<string> GetCategoryItemNames(string Category)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("Category", Category);
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT name FROM item WHERE CategoryName = @Category", par);
            List<string> lcNames = new List<string>();
            foreach (DataRow dr in lcResult.Rows)
                lcNames.Add((string)dr[0]);
            return lcNames;
        }

        public List<clsItem> GetCategoryItems(string Category)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("Category", Category);
            DataTable lcResult =
                clsDbConnection.GetDataTable("SELECT * FROM item WHERE CategoryName = @Category", par);

            List<clsItem> lcItemList = new List<clsItem>();

            if (lcResult.Rows.Count > 0)
            {
                foreach (DataRow dr in lcResult.Rows)
                {
                    clsItem lcItem = new clsItem();
                    lcItem.Name = (string)dr["Name"];
                    lcItem.Description = (string)dr["Description"];
                    lcItemList.Add(lcItem);
                }
                return lcItemList;
            }
            else
                return null;
        }

        public clsItem GetItem(string Name)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("Name", Name);
            DataTable lcResult =
                clsDbConnection.GetDataTable("SELECT * FROM item WHERE Name = @Name", par);
            if (lcResult.Rows.Count > 0)
                return new clsItem()
                {
                    Name = (string)lcResult.Rows[0]["Name"],
                    Category = (string)lcResult.Rows[0]["CategoryName"],
                    Description = (string)lcResult.Rows[0]["Description"],
                    Price = Convert.ToSingle(lcResult.Rows[0]["Price"]),
                    ModifiedDate = (lcResult.Rows[0]["ModifiedDate"]).ToString(),
                    Quantity = Convert.ToInt32(lcResult.Rows[0]["Quantity"]),
                    Motor = (string)lcResult.Rows[0]["Motor"],
                    //WarrantyPeriod = Convert.ToInt32(lcResult.Rows[0]["WarrantyPeriod"]),
                    //Condition = (string)lcResult.Rows[0]["Condition"],
                    Type = (string)lcResult.Rows[0]["Type"]
                };
            else
                return null;
        }

        // PUT
        public string PutItem(clsItem prItem)
        {
            try
            {
                int lcRecCount = clsDbConnection.Execute(
                    "UPDATE item SET Name = @Name, Category = @Category, Description = @Description, Price = @Price, ModifiedDate = @ModifiedDate, Quantity = @Quantity, Motor = @Motor, WarrantyPeriod = @WarrantyPeriod, Condition = @Condition, Type = @Type", PrepareItemParameters(prItem));
                if(lcRecCount == 1)
                {
                    return "One Item Updated.";
                }
                else
                {
                    return "Unexpected Item Update Count.";
                }
            }
            catch (Exception ex)
            {

                return ex.GetBaseException().Message;
            }
        }

        //POST
        public string PostItem(clsItem prItem)
        {
            try
            {
                int lcRecCount = clsDbConnection.Execute(
                    "INSERT INTO item VALUES Name = @Name, Category = @Category, Description = @Description, Price = @Price, ModifiedDate = @ModifiedDate, Quantity = @Quantity, Motor = @Motor, WarrantyPeriod = @WarrantyPeriod, Condition = @Condition, Type = @Type", PrepareItemParameters(prItem));
                //if (lcRecCount == 1)
                //{
                //    return "One Item Inserted.";
                //}
                //else
                //{
                //    return "Unexpected Item Update Count.";
                //}
                return "One Item Inserted.";
            }
            catch (Exception ex)
            {

                return ex.GetBaseException().Message;
            }
        }

        // ##### PREPARATION METHODS #####
        private Dictionary<string, object> PrepareItemParameters(clsItem prItem)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(10);
            par.Add("Name", prItem.Name);
            par.Add("Category", prItem.Category);
            par.Add("Description", prItem.Description);
            par.Add("Price", prItem.Price);
            par.Add("ModifiedDate", prItem.ModifiedDate);
            par.Add("Quantity", prItem.Quantity);
            par.Add("Motor", prItem.Motor);
            par.Add("WarrantyPeriod", prItem.WarrantyPeriod);
            par.Add("Condition", prItem.Condition);
            par.Add("Type", prItem.Type);
            return par;
        }
    }    
}
