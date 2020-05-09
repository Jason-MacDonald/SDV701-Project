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
    }    
}
