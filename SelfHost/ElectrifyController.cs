using System;
using System.Collections.Generic;
using System.Data;

namespace SelfHost
{
    //note controller must end with controller!
    public class ElectrifyController : System.Web.Http.ApiController
    {
        #region ##### CATEGORY QUERIES #####
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
        #endregion

        #region ##### ITEM QUERIES #####
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
                    lcItem.Id = Convert.ToInt32(dr["Id"]);
                    lcItem.Name = (string)dr["Name"];
                    //lcItem.Description = (string)dr["Description"];
                    lcItemList.Add(lcItem);
                }
                return lcItemList;
            }
            else
                return null;
        }

        public clsItem GetItem(string Id)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("Id", Id);
            DataTable lcResult =
                clsDbConnection.GetDataTable("SELECT * FROM item WHERE Id = @Id", par);
            if (lcResult.Rows.Count > 0)
                return new clsItem()
                {
                    Id = Convert.ToInt32(lcResult.Rows[0]["Id"]),
                    Name = (string)lcResult.Rows[0]["Name"],
                    Category = (string)lcResult.Rows[0]["CategoryName"],
                    Description = (string)lcResult.Rows[0]["Description"],
                    Price = Convert.ToSingle(lcResult.Rows[0]["Price"]),
                    ModifiedDate = (lcResult.Rows[0]["ModifiedDate"]).ToString(),
                    Quantity = Convert.ToInt32(lcResult.Rows[0]["Quantity"]),
                    Motor = (string)lcResult.Rows[0]["Motor"],
                    WarrantyPeriod = lcResult.Rows[0]["WarrantyPeriod"] is DBNull ? 0 : Convert.ToInt32(lcResult.Rows[0]["WarrantyPeriod"]),
                    Condition = lcResult.Rows[0]["Condition"] is DBNull ? "" : (string)lcResult.Rows[0]["Condition"],
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
                    "UPDATE item SET Name = @Name, CategoryName = @Category, Description = @Description, Price = @Price, ModifiedDate = @ModifiedDate, Quantity = @Quantity, Motor = @Motor, WarrantyPeriod = @WarrantyPeriod, Condition = @Condition, Type = @Type WHERE Id = @Id", PrepareItemParameters(prItem));
                if(lcRecCount == 1)
                    return "One Item Updated.";
                else
                    return "Unexpected Item Update Count.";
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
                    "INSERT INTO item(name, categoryName, description, price, modifiedDate, quantity, motor, warrantyPeriod, condition, type) VALUES (@Name, @Category, @Description, @Price, @ModifiedDate, @Quantity, @Motor, @WarrantyPeriod, @Condition, @Type)", PrepareItemParameters(prItem)
                );
                return "One Item Inserted.";
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }
        #endregion

        #region ##### ORDER QUERIES #####
        public List<clsOrder> GetOrders()
        {
            DataTable lcResult =
                clsDbConnection.GetDataTable("SELECT * FROM itemOrder", null);

            List<clsOrder> lcOrderList = new List<clsOrder>();

            if (lcResult.Rows.Count > 0)
            {
                foreach (DataRow dr in lcResult.Rows)
                {
                    clsOrder lcOrder = new clsOrder();
                    lcOrder.InvoiceNumber = Convert.ToInt32(dr["InvoiceNumber"]);
                    lcOrder.ItemName = (string)dr["ItemName"];
                    lcOrder.Quantity = Convert.ToInt32(dr["Quantity"]);
                    lcOrder.Price = Convert.ToSingle(dr["Price"]);
                    lcOrder.Name = (string)dr["Name"];
                    lcOrder.Email = (string)dr["Email"];
                    lcOrderList.Add(lcOrder);
                }
                return lcOrderList;
            }
            else
                return null;
        }
        #endregion

        #region ##### PREPARATION METHODS #####
        private Dictionary<string, object> PrepareItemParameters(clsItem prItem)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(11);
            par.Add("Id", prItem.Id);
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
        #endregion
    }
}
