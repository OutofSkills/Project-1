using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BaseFeatures
{
    class Customer
    {
        public void ReadData()
        {

            using (SqlConnection conn = new SqlConnection())
            {
                try
                {
                    conn.ConnectionString = "Data Source=DESKTOP-3B6JT5J\\SQLEXPRESS;Initial Catalog=academy_net;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    conn.Open();

                    var query = "SELECT* FROM Customers";
                    SqlCommand command = new SqlCommand(query, conn);

                    SqlDataReader read = command.ExecuteReader();
                    while (read.Read() == true)
                    {
                        Console.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3}",
                       // call the objects from their index
                       read[0], read[1], read[2], read[3]));
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                }


            }
        }
    }
}
