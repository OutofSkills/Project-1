using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataConnection;
using DataValidation;
using DateTools;

namespace Project_1
{
    class Car : IExistence
    {
        public int carID { get; set; }
        public string plate { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string pricePerDay { get; set; }
        public string location { get; set; }

        public void readCarData(CartRent rent)
        {
            Console.Clear();

            Console.WriteLine("Cart Plate:");
            this.plate = Console.ReadLine();

            Console.WriteLine("Car Model:");
            this.model = Console.ReadLine();

            Console.WriteLine("Start Date:");
            rent.startDate = Console.ReadLine();

            Console.WriteLine("End Date:");
            rent.endDate = Console.ReadLine();

            Console.WriteLine("City:");
            this.location = Console.ReadLine();
        }

        public void getCarData()
        {
            int carID = 0;
            string sCarID = "";
            string query = "Select CarID, Manufacturer, Model, PricePerDay, Location From Cars Where Plate = '" + this.plate + "'";
            DataManager data = new DataManager();

            try
            {
                using (data.databaseConnection = new SqlConnection())
                {
                    data.setDatabaseConection();
                    data.databaseConnection.Open();

                    SqlCommand getCarID = new SqlCommand(query, data.databaseConnection);
                    SqlDataReader reader = getCarID.ExecuteReader();

                    while (reader.Read())
                    {
                         sCarID = reader[0].ToString();
                        this.manufacturer = reader[1].ToString();
                        this.model = reader[2].ToString(); 
                        this.pricePerDay = reader[3].ToString();
                        this.location = reader[4].ToString();
                    }

                    Int32.TryParse(sCarID, out carID);
                    this.carID = carID;

                }
            }catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

               
        }

        public void availableCarList(CartRent rent) ////Need Modifications
        {
            DataManager data = new DataManager();
            Tools tools = new Tools();

            string query1 = "SELECT * FROM Cars WHERE Location = '" + location + "' and Plate = '" + plate +"' and Model = '"+ model +"' and carID NOT IN";
            string query2 = "(SELECT carID FROM Reservations WHERE NOT((StartDate > '"+ tools.convertStringDate(rent.endDate).ToString("yyyy-MM-dd") + 
                "') OR (EndDate < '"+ tools.convertStringDate(rent.startDate).ToString("yyyy-MM-dd") + "')))";

            using (data.databaseConnection = new SqlConnection())
            {
                using (SqlCommand getAvailableCars = new SqlCommand())
                {
                    data.setDatabaseConection();

                    getAvailableCars.Connection = data.databaseConnection;
                    getAvailableCars.CommandText = query1+query2;

                    Console.WriteLine(string.Format("{0}\t {1}\t {2}\t {3}\t {4}\t {5}", "Car ID", "Cart Plate", "Manufacturer", "Model", "PricePerDay", "City"));
                    try
                    {
                        data.databaseConnection.Open();
                        SqlDataReader dataReader = getAvailableCars.ExecuteReader();

                        while (dataReader.Read())
                        {
                            Console.WriteLine(string.Format("{0}\t |{1}\t\t |{2}\t |{3}\t |{4}\t |{5}",
                       dataReader[0], dataReader[1], dataReader[2], dataReader[3], dataReader[4], dataReader[5]));
                        }

                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e);
                    }
                }

            }
        }

        public bool validateExistence()
        {
            DataManager data = new DataManager();
            bool validation = true;
            string query = "SELECT COUNT(*) FROM [Cars] WHERE ([CarID] = @CarID)";

            using (data.databaseConnection = new SqlConnection())
            {
                using (SqlCommand checkCarModel = new SqlCommand())
                {
                    data.setDatabaseConection();

                    checkCarModel.Connection = data.databaseConnection;
                    checkCarModel.CommandText = query;
                    checkCarModel.Parameters.AddWithValue("@CarID", this.carID);

                    try 
                    {
                        data.databaseConnection.Open();
                        int carExists = (int)checkCarModel.ExecuteScalar();

                        checkCarModel.ExecuteNonQuery();

                        if (carExists > 0)
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
        public bool checkForLocation(string location)
        {
            bool validation = true;

            if(this.location.ToLower() == location.ToLower())
            {
                return validation;
            }
            return !validation;
        }

    }
}

