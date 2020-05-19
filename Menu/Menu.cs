using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Project_1;

namespace BaseFeatures
{
    public class Menu : IMenu
    {
        public void DisplayMenu()
        {
            int menuChoice;

            DisplayMenuText();

            string sMenuChoice = Console.ReadLine();
            Int32.TryParse(sMenuChoice, out menuChoice);

            SelectMenuOption(menuChoice);

        }

        public void DisplayMenuText()
        {
            Console.WriteLine("1 Register new Cart Rent");
            Console.WriteLine("2 Update Car Rent");
            Console.WriteLine("3 List Rents");
            Console.WriteLine("4 List Available Cars");
            Console.WriteLine("5 Register new Customer");
            Console.WriteLine("6 Update Customer");
            Console.WriteLine("7 List Customers");
            Console.WriteLine("8 Quit\n");
        }

        public void SelectMenuOption(int choice)
        {
            Validation validation;

            switch (choice)
            {
                case 1:
                    validation = new Validation();
                    validation.validateNewRent();            
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    validation = new Validation();
                    validation.validateNewCustomer();
                    break;
                case 6:
                    break;
                case 7:
                    Console.Clear();

                    Customer customerData = new Customer();
                    customerData.displayCustomersList();
                    break;

                case 8:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
