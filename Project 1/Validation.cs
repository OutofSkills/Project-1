using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_1;
using DateTools;

namespace DataValidation
{
    class Validation : IValidation
    {
        public void validateNewRent()
        {
            CartRent rent = new CartRent();
            Tools tools = new Tools();
            rent.status = new ReservationsStatus();

            rent.car = new Car();
            rent.readCartRentData();

            if (tools.checkDateFormat(rent.startDate) && tools.checkDateFormat(rent.endDate))
            {
                if (tools.checkDates(tools.convertStringDate(rent.startDate), tools.convertStringDate(rent.endDate)))
                {
                    rent.car.getCarData();

                    if (rent.car.validateExistence())
                    {
                        if (rent.customer.validateExistence())
                        {
                            rent.status = new ReservationsStatus();
                            rent.status.CheckStatusInfo(rent);
                            Console.WriteLine(rent.status.name);

                            if (rent.checkCarAvailablility())
                            {
                                if (rent.car.checkForLocation(rent.location))
                                {
                                    rent.status.ID = 1;

                                    if (rent.registerNewCartRent())
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Your reservation ID is: " + rent.getReservationID());
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        rent.status.ID = 3;
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("This car is not available in your location!");
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("The car is not available!");
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("This Client ID doesn't exist!");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("This Car doesn't exist!");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Start date can't be smaller than the End date!");
                }
            }
        }

        public void validateRentUpdate()
        {
            CartRent rent = new CartRent();
            Tools tools = new Tools();
            rent.status = new ReservationsStatus();

            Console.Clear();

            int rentID;
            Console.WriteLine("Introduce your reservation ID:");

            string sRentID = Console.ReadLine();
            Int32.TryParse(sRentID, out rentID);
            if (!rent.validateExistence(rentID))
            {
                Console.Clear();
                Console.WriteLine("The following reservation ID doesn't exist!");
                Console.ReadKey();
                return;
            }
            rent.car = new Car();
            rent.readCartRentData();

            if (tools.checkDateFormat(rent.startDate) && tools.checkDateFormat(rent.endDate))
            {
                if (tools.checkDates(tools.convertStringDate(rent.startDate), tools.convertStringDate(rent.endDate)))
                {
                    rent.car.getCarData();

                    if (rent.car.validateExistence())
                    {
                        if (rent.customer.validateExistence())
                        {
                            rent.status = new ReservationsStatus();
                            rent.status.CheckStatusInfo(rent);
                            Console.WriteLine(rent.status.name);

                            if (rent.checkCarAvailablility())
                            {
                                if (rent.car.checkForLocation(rent.location))
                                {
                                    rent.status.ID = 1;

                                    if (rent.updateCartRent(rentID))
                                    {
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        rent.status.ID = 3;
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("This car is not available in your location!");
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("The car is not available!");
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("This Client ID doesn't exist!");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("This Car doesn't exist!");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Start date can't be smaller than the End date!");
                }
            }
        }

        public void validateNewCustomer()
        {
            Customer customer = new Customer();
            Tools tool = new Tools();

            customer.readCustomerData();
            if (!customer.validateExistence())
            {
                if (tool.checkDateFormat(customer.birthDate))
                {
                    if (customer.addNewCustomer())
                    {
                        Console.Clear();
                    }

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid date format!");
                    Console.WriteLine("Use dd/mm/yyy format!");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("This ID already exists!");
            }
        }
        public void validateCustomerUpdate()
        {
            Customer customer = new Customer();
            Tools tool = new Tools();

            customer.readCustomerData();
            if (customer.validateExistence())
            {
                if (tool.checkDateFormat(customer.birthDate))
                {
                    if (customer.updateCustomerData())
                    {
                        Console.Clear();
                    }

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid date format!");
                    Console.WriteLine("Use dd/mm/yyy format!");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("This ID doesn't exist!");
            }
        }
    }
}
