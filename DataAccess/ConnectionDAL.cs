using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ConnectionDAL
    {
        public string Connection { get; set; }

        public ConnectionDAL()
        {
            Connection = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        }
    }
}
