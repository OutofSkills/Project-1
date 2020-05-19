using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Project_1;
using DataValidation;

namespace Menu
{
    public class Menu : IMenu
    {
        public void DisplayMenu()
        {
            int menuChoice = 0;

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
            IValidation validation;
            ReservationsStatus status = new ReservationsStatus();
            status.updateReservationsStatuses();

            switch (choice)
            {
                case 1:
                    validation = new Validation();
                    validation.validateNewRent();

                    Console.Clear();
                    DisplayMenu();
                    break;
                case 2:
                    validation = new Validation();
                    validation.validateRentUpdate();

                    Console.Clear();
                    DisplayMenu();
                    break;
                case 3:
                    Console.Clear();
                    CartRent rent = new CartRent();
                    rent.displayRentsList();

                    Console.ReadKey();
                    Console.Clear();
                    DisplayMenu();
                    break;
                case 4:
                    break;
                case 5:
                    validation = new Validation();
                    validation.validateNewCustomer();

                    Console.Clear();
                    DisplayMenu();
                    break;
                case 6:
                    Console.Clear();

                    validation = new Validation();
                    validation.validateCustomerUpdate();

                    Console.Clear();
                    DisplayMenu();
                    break;
                case 7:
                    Console.Clear();

                    Customer customerData = new Customer();
                    customerData.displayCustomersList();

                    Console.ReadKey();
                    Console.Clear();
                    DisplayMenu();
                    break;

                case 8:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
