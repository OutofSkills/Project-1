using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    class ReservationsStatus
    {
        public int ID { get; set; }
        public string name { get; private set; }
        public string description { private set; get; }

        public void setReservationStatus()
        {
            DataManager data = new DataManager();
            string query = "Select Name, Description From ReservationStatuses Where ReservStatsID = '" + this.ID + "'";

            try
            {
                using (data.databaseConnection = new SqlConnection())
                {
                    data.setDatabaseConection();
                    data.databaseConnection.Open();

                    SqlCommand getStatusInfo = new SqlCommand(query, data.databaseConnection);
                    SqlDataReader reader = getStatusInfo.ExecuteReader();

                    while (reader.Read())
                    {
                        this.name = reader[0].ToString();
                        this.description = reader[1].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            Console.ReadLine();
        }
        public void CheckStatusInfo(CartRent rent)
        {
            DataManager data = new DataManager();
            Tools tools = new Tools();

            string query1 = "(SELECT ReservStatsID FROM Reservations WHERE NOT((StartDate > convert(date,'" + rent.endDate + "',104)) or" +
                " (EndDate < convert(date, '" +rent.startDate + "', 104))))";
            string query = "SELECT DISTINCT ReservStatsID FROM Reservations WHERE CarID = '" + rent.car.carID + "' and ReservStatsID IN ";
            try
            {
                using (data.databaseConnection = new SqlConnection())
                {
                    data.setDatabaseConection();
                    data.databaseConnection.Open();

                    SqlCommand getStatusInfo = new SqlCommand(query + query1, data.databaseConnection);
                    SqlDataReader reader = getStatusInfo.ExecuteReader();

                    while (reader.Read())
                    {
                        string sID = reader[0].ToString();
                        this.ID = Int32.Parse(sID);
                    }
                }
            }catch(SqlException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public void updateReservationsStatuses()
        {
            DataManager data = new DataManager();
            Tools tools = new Tools();
            string query = "UPDATE Reservations SET ReservStatsID = '2'" +
                           "WHERE (ReservationID > '0') and (convert(date,'" + tools.getCurrentDate() + "', 104) >= EndDate)";

            using (data.databaseConnection = new SqlConnection())
            {
                using (SqlCommand updateStatus = new SqlCommand())
                {
                    data.setDatabaseConection();
                    updateStatus.Connection = data.databaseConnection;
                    updateStatus.CommandText = query;

                    try
                    {
                        data.databaseConnection.Open();
                        updateStatus.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
    }
}
