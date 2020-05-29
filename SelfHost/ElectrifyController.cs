using System;
using System.Collections.Generic;
using System.Data;

namespace SelfHost
{
    public class ElectrifyController : System.Web.Http.ApiController
    {
        #region ##### CATEGORY QUERIES #####
        public List<string> GetCategoryNames()
        {
            DataTable lcResult = clsDbConnection.GetDataTable(
                "SELECT name " +
                "FROM category",
                null);
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
                clsDbConnection.GetDataTable(
                    "SELECT * " +
                    "FROM category " +
                    "WHERE Name = @Name", 
                    par);
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

        #region ### GET ###
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
                    lcItemList.Add(new clsItem
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = (string)dr["Name"],
                        Category = (string)dr["CategoryName"],
                        Description = (string)dr["Description"],
                        Price = Convert.ToSingle(dr["Price"]),
                        ModifiedDate = (dr["ModifiedDate"]).ToString(),
                        Quantity = Convert.ToInt32(dr["Quantity"]),
                        Motor = (string)dr["Motor"],
                        Battery = (string)dr["Battery"],
                        WarrantyPeriod = dr["WarrantyPeriod"] is DBNull ? 0 : Convert.ToInt32(dr["WarrantyPeriod"]),
                        Condition = dr["Condition"] is DBNull ? "" : (string)dr["Condition"],
                        Type = (string)dr["Type"]
                    });
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
                    Battery = (string)lcResult.Rows[0]["Battery"],
                    WarrantyPeriod = lcResult.Rows[0]["WarrantyPeriod"] is DBNull ? 0 : Convert.ToInt32(lcResult.Rows[0]["WarrantyPeriod"]),
                    Condition = lcResult.Rows[0]["Condition"] is DBNull ? "" : (string)lcResult.Rows[0]["Condition"],
                    Type = (string)lcResult.Rows[0]["Type"]
                };
            else
                return null;
        }
        #endregion

        #region ### PUT ###
        public string PutItem(clsItem prItem)
        {
            try
            {
                int lcRecCount = clsDbConnection.Execute(
                    "UPDATE item " +
                    "SET Name = @Name, Description = @Description, Price = @Price, ModifiedDate = @ModifiedDate, Quantity = @Quantity, Motor = @Motor, Battery = @Battery, WarrantyPeriod = @WarrantyPeriod, Condition = @Condition " +
                    "WHERE Id = @Id",
                    PrepareItemParameters(prItem));
                if (lcRecCount == 1)
                    return "Item Updated.";
                else
                    return "Error 001: Unexpected Item Update Count.";
            }
            catch (Exception ex)
            {
                return "Error 002: " + ex.GetBaseException().Message;
            }
        }

        public string PutItemQuantity(clsItem prItem)
        {
            try
            {
                int lcRecCount = clsDbConnection.Execute(
                    "UPDATE item " +
                    "SET Quantity = Quantity - @Quantity " +
                    "WHERE Id = @Id", 
                    PrepareItemParameters(prItem));
                if(lcRecCount == 1)
                    return "success";
                else
                    return "Error 001: Unexpected Item Update Count.";
            }
            catch (Exception ex)
            {
                return "Error 002: " + ex.GetBaseException().Message;
            }
        }
        #endregion

        #region ### POST ###
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

        #region ### DELETE ###
        public string DeleteItem(string Id)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("Id", Id);
            try
            {
                int lcRecCount = clsDbConnection.Execute(
                    "DELETE FROM item WHERE Id = @Id", par);
                if (lcRecCount == 1)
                    return "One Item Deleted.";
                else
                    return "Unexpected Item Delete Count.";
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }
        #endregion

        #endregion

        #region ##### ORDER QUERIES #####

        #region ### POST ###
        public string PostOrder(clsOrder prOrder)
        {
            try
            {
                int lcRecCount = clsDbConnection.Execute(
                    "INSERT INTO itemOrder(itemName, quantity, price, name, email) " +
                    "VALUES (@ItemName, @Quantity, @Price, @Name, @Email)", 
                    PrepareOrderParameters(prOrder)
                );
                return "One Order Inserted.";
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }
        #endregion

        #region ### GET ###
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

        #region ### DELETE ###
        public string DeleteOrder(string Id)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("Id", Id);
            try
            {
                int lcRecCount = clsDbConnection.Execute(
                    "DELETE FROM itemOrder WHERE InvoiceNumber = @Id", par);
                if (lcRecCount == 1)
                    return "One Item Deleted.";
                else
                    return "Unexpected Item Delete Count.";
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }
        #endregion

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
            par.Add("Battery", prItem.Battery);
            par.Add("WarrantyPeriod", prItem.WarrantyPeriod);
            par.Add("Condition", prItem.Condition);
            par.Add("Type", prItem.Type);
            return par;
        }

        private Dictionary<string, object> PrepareOrderParameters(clsOrder prOrder)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(5);
            par.Add("ItemName", prOrder.ItemName);
            par.Add("Quantity", prOrder.Quantity);
            par.Add("Price", prOrder.Price);
            par.Add("Name", prOrder.Name);
            par.Add("Email", prOrder.Email);
            return par;
        }
        #endregion
    }
}
