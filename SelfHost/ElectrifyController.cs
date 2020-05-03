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

    }
}
