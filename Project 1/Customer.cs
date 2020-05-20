using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DataValidation;
using DataConnection;
using DateTools;

namespace Project_1
{
    class Customer : IExistence
    {
        public int customerID { set; get; }
        public string name { set; get; }
        public string birthDate { set; get; }
        public string location { set; get; }

        public void readCustomerData()
        {
            Console.Clear();

            Console.WriteLine("Client ID:");
            this.customerID = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Client Name:");
            this.name = Console.ReadLine();

            Console.WriteLine("Birth Date:");
            this.birthDate = Console.ReadLine();

            Console.WriteLine("Location:");
            this.location = Console.ReadLine();
        }
        public bool addNewCustomer()
        {
            DataManager data = new DataManager();
            Tools tools = new Tools();
            string query = "INSERT INTO Customers(Name, BirthDate, Location) " +
                           "VALUES(@Name, @BirthDate, @Location)";

            using (data.databaseConnection = new SqlConnection())
            {
                using (SqlCommand insertNewCustomerData = new SqlCommand())
                {
                    data.setDatabaseConection();

                    insertNewCustomerData.Connection = data.databaseConnection;
                    insertNewCustomerData.CommandText = query;

                    insertNewCustomerData.Parameters.AddWithValue("@Name", this.name);
                    insertNewCustomerData.Parameters.AddWithValue("@BirthDate", tools.convertStringDate(this.birthDate));
                    insertNewCustomerData.Parameters.AddWithValue("@Location", this.location);
                    try
                    {
                        data.databaseConnection.Open();
                        insertNewCustomerData.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e);
                        return false;
                    }
                }
            }
            return true;
        }
        public void getCustomerData()
        {

            DataManager data = new DataManager();
            string query = "Select Name, BirthDate, Location From Customers Where CustomerID = '" + this.customerID + "'";

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
                        this.name = reader[0].ToString();
                        this.birthDate = reader[1].ToString();
                        this.location = reader[2].ToString();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public bool updateCustomerData()
        {
            DataManager data = new DataManager();
            Tools tools = new Tools();
            string query = "UPDATE Customers SET Name = @Name, BirthDate = @BirthDate, Location = @Location " +
                           "WHERE CustomerID = '" + this.customerID + "'";

            using (data.databaseConnection = new SqlConnection())
            {
                using (SqlCommand updateCustomerData = new SqlCommand())
                {
                    data.setDatabaseConection();

                    updateCustomerData.Connection = data.databaseConnection;
                    updateCustomerData.CommandText = query;

                    updateCustomerData.Parameters.AddWithValue("@Name", this.name);
                    updateCustomerData.Parameters.AddWithValue("@BirthDate", tools.convertStringDate(this.birthDate));
                    updateCustomerData.Parameters.AddWithValue("@Location", this.location);
                    try
                    {
                        data.databaseConnection.Open();
                        updateCustomerData.ExecuteNonQuery();
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
        public void displayCustomersList()
        {
            this.customerID = 1;

            Console.WriteLine(string.Format("{0}\t {1, -20}\t {2, 10}\t {3, 18}", "Client ID", "Client Name", "BirthDate", "Location"));

            while (validateExistence())
           {
                Tools tools = new Tools();

                getCustomerData();

                Console.WriteLine(string.Format("{0}\t |{1,-25}\t  |{2,-20}\t  |{3,-20}",
                       this.customerID, this.name, tools.convertStringDate(this.birthDate).ToString("dd/MM/yyyy"), this.location));

                this.customerID++;
            } 
        }

        public bool validateExistence()
        {
            bool validation = true;
            DataManager data = new DataManager(); 
            string query = "SELECT COUNT(*) FROM [Customers] WHERE ([CustomerID] = @CustomerID)";

            using (data.databaseConnection = new SqlConnection())
            {
                using (SqlCommand checkCustomerID = new SqlCommand())
                {
                    data.setDatabaseConection();

                    checkCustomerID.Connection = data.databaseConnection;
                    checkCustomerID.CommandText = query;
                    checkCustomerID.Parameters.AddWithValue("@CustomerID", this.customerID);

                    try
                    {
                        data.databaseConnection.Open();

                        int idExists = (int)checkCustomerID.ExecuteScalar();
                        checkCustomerID.ExecuteNonQuery();

                        if (idExists == 0)
                        {
                            return !validation;
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e);
                    }
                }

                return validation;
            }
        }
    }
}
