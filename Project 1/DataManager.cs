using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DataConnection
{
    class DataManager
    {
        public SqlConnection databaseConnection { get; set; }
        public void setDatabaseConection()
        {
            databaseConnection.ConnectionString = "Data Source=DESKTOP-3B6JT5J\\SQLEXPRESS;Initial Catalog=academy_net;" +
                        "Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        }

    }
}
