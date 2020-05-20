using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataConnection;
using DateTools;


namespace Project_1
{
    class CartRent
    {
        public Car car { get; set; }
        public Customer customer { get; set; }
        public ReservationsStatus status { get; set; }
        public string startDate {get; set;}
        public string endDate { get; set; }
        public string location { get; set;}
        public void readCartRentData()
        {
            this.car = new Car();
            this.customer = new Customer();

            Console.Clear();

            Console.WriteLine("Cart Plate:");
            this.car.plate = Console.ReadLine();

            Console.WriteLine("Client ID:");
            this.customer.customerID = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Start Date:");
            this.startDate = Console.ReadLine();    

            Console.WriteLine("End Date:");
            this.endDate = Console.ReadLine();

            Console.WriteLine("City:");
            this.location = Console.ReadLine();
        }

        public bool registerNewCartRent()
        {
            DataManager data = new DataManager();
            Tools tools = new Tools();
            string query = "INSERT INTO Reservations(CarID, CartPlate, CustomerID, ReservStatsID, StartDate, EndDate, Location) " +
                       "VALUES(@CarID, @CartPlate, @CustomerID, @ReservStatsID, @StartDate, @EndDate, @Location)";

            using (data.databaseConnection = new SqlConnection())
            {
                using (SqlCommand insertNewCartData = new SqlCommand())
                {
                    data.setDatabaseConection();

                    insertNewCartData.Connection = data.databaseConnection;
                    insertNewCartData.CommandText = query;
                   
                    insertNewCartData.Parameters.AddWithValue("@CarID", this.car.carID);
                    insertNewCartData.Parameters.AddWithValue("@CartPlate", this.car.plate);
                    insertNewCartData.Parameters.AddWithValue("@CustomerID", this.customer.customerID);
                    insertNewCartData.Parameters.AddWithValue("@ReservStatsID", this.status.ID);
                    insertNewCartData.Parameters.AddWithValue("@StartDate", tools.convertStringDate(this.startDate));
                    insertNewCartData.Parameters.AddWithValue("@EndDate", tools.convertStringDate(this.endDate));
                    insertNewCartData.Parameters.AddWithValue("@Location", this.location);

                    try
                    {
                        data.databaseConnection.Open();
                        insertNewCartData.ExecuteNonQuery();
                    }
                    catch(SqlException e)
                    {
                        Console.WriteLine(e);
                        return false;
                    }
                }

            }
            return true;
        }
        public int getReservationID()
        {
            int ID = 0;
            string sID = "";
            string query = "Select MAX(ReservationID) From Reservations";
            DataManager data = new DataManager();

            try
            {
                using (data.databaseConnection = new SqlConnection())
                {
                    data.setDatabaseConection();
                    data.databaseConnection.Open();

                    SqlCommand getID = new SqlCommand(query, data.databaseConnection);
                    SqlDataReader reader = getID.ExecuteReader();

                    while (reader.Read())
                    {
                        sID = reader[0].ToString();
                    } 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            Int32.TryParse(sID, out ID);
            return ID;
        }

        public bool validateExistence(int rentID)
        {
            DataManager data = new DataManager();
            bool validation = true;
            string query = "SELECT COUNT(*) FROM [Reservations] WHERE ([ReservationID] = @ReservationID)";

            using (data.databaseConnection = new SqlConnection())
            {
                using (SqlCommand checkRentID = new SqlCommand())
                {
                    data.setDatabaseConection();

                    checkRentID.Connection = data.databaseConnection;
                    checkRentID.CommandText = query;
                    checkRentID.Parameters.AddWithValue("@ReservationID", rentID);

                    try
                    {
                        data.databaseConnection.Open();
                        int idExists = (int)checkRentID.ExecuteScalar();

                        checkRentID.ExecuteNonQuery();

                        if (idExists > 0)
                        {
                            return validation;
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e);
                    }
                }
                return !validation;
            }
        }

        public bool updateCartRent(int rentID)
        {
            DataManager data = new DataManager();
            Tools tools = new Tools();
            string query = "UPDATE Reservations SET CarID = @CarID, CartPlate = @CartPlate, CustomerID = @CustomerID," +
                " ReservStatsID = @ReservStatsID, StartDate = @StartDate, EndDate = @EndDate, Location = @Location " +
                       "WHERE (ReservationID ='" + rentID + "')";

            using (data.databaseConnection = new SqlConnection())
            {
                using (SqlCommand updateCartRentData = new SqlCommand())
                {
                    data.setDatabaseConection();

                    updateCartRentData.Connection = data.databaseConnection;
                    updateCartRentData.CommandText = query;

                    updateCartRentData.Parameters.AddWithValue("@CarID", this.car.carID);
                    updateCartRentData.Parameters.AddWithValue("@CartPlate", this.car.plate);
                    updateCartRentData.Parameters.AddWithValue("@CustomerID", this.customer.customerID);
                    updateCartRentData.Parameters.AddWithValue("@ReservStatsID", this.status.ID);
                    updateCartRentData.Parameters.AddWithValue("@StartDate", tools.convertStringDate(this.startDate));
                    updateCartRentData.Parameters.AddWithValue("@EndDate", tools.convertStringDate(this.endDate));
                    updateCartRentData.Parameters.AddWithValue("@Location", this.location);
                    try
                    {
                        data.databaseConnection.Open();
                        updateCartRentData.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e);
                        Console.ReadKey();
                        return false;
                    }
                }

            }
            return true;
        }
        public void displayRentsList()
        {
            DataManager data = new DataManager();
            Tools tools = new Tools();
            string query = "Select CartPlate, CustomerID, StartDate, EndDate, Location FROM Reservations " +
                           "WHERE(ReservationID > '0')";

            using (data.databaseConnection = new SqlConnection())
            {
                using (SqlCommand displayRents = new SqlCommand())
                {
                    data.setDatabaseConection();

                    displayRents.Connection = data.databaseConnection;
                    displayRents.CommandText = query;
                    Console.WriteLine(string.Format("{0}\t {1}\t {2}\t {3}\t {4}","Cart Plate", "CustomerID", "StartDate", "EndDate", "Location"));
                    try
                    {
                        data.databaseConnection.Open();
                        SqlDataReader dataReader = displayRents.ExecuteReader();

                        while (dataReader.Read())
                        {
                            Console.WriteLine(string.Format("{0}\t |{1}\t\t |{2}\t |{3}\t |{4}",
                       dataReader[0], dataReader[1], tools.convertStringDate(dataReader[2].ToString()).ToString("dd/MM/yyyy"),
                       tools.convertStringDate(dataReader[3].ToString()).ToString("dd/MM/yyyy"), dataReader[4]));
                        }

                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e);
                      
                    }
                }

            }
        }
        public bool checkCarAvailablility()
        {
            bool validation = true;

            if (this.status.name == "CLOSED" || this.status.name == null)
                return validation;
            return !validation;
        }


    }
}
