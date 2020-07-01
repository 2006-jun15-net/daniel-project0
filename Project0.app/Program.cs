using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Threading;
using DBAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;
using Project0.Library.models;
namespace Projec0.app
{
    public class Program
    {
        
        // public static readonly ILoggerFactory MyLoggerFactory
        //    = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static readonly String connectionString = System.IO.File.ReadAllText("C:/Users/james/Desktop/Revature/Project0Connect.txt");

        public static readonly DbContextOptions<Project01Context> Options = new DbContextOptionsBuilder<Project01Context>()
                //.UseLoggerFactory(MyLoggerFactory)
                .UseSqlServer(connectionString)
                .Options;

        static void Main(string[] args)
        {
            var CCustomer = new Customer();
            
            for (int i = 0; i <= 100; i++)
            {
                Console.WriteLine("\nSelect From List");
                Console.WriteLine("\n Options: ('c') CustomerList, ('n') New Customer, ('r') Registered Customer, ('u') Update Customer Name ");
                Console.Write("Select Options: ");
                var option1 = Console.ReadLine();
                if (option1 == "r")
                {
                    Console.WriteLine("");
                    Console.Write("Enter your ID number: ");
                    
                    var idnumber = Console.ReadLine();
                    int ID;
                    //bool check = false;

                    while (!int.TryParse(idnumber, out ID))
                    {
                        Console.WriteLine("This is not a Valid ID number!");
                        idnumber = Console.ReadLine();
                        
                        //check = CustomerController.CustomerList(int.Parse(idnumber));
                    }
                    //requires a second input validation check to make certain that the ID given exists
                   
                    CCustomer.CustomerId = ID;
                    string name = CustomerController.FindCustomerName(ID);
                    var date = DateTime.Now;
                    Console.WriteLine($"\nHello, {name}, on {date:d} at {date:t}!");
                    Console.WriteLine("Enter any key to Continue: ");
                    Console.ReadKey(true);
                    Console.Clear();
                    Console.WriteLine("\nSelect a destination: ");
                    LocationController.DisplayLocations();
                    Console.Write("Select Destination by ID, ex: '1': ");
                    break;
                }
                else if (option1 == "n")
                {
                    CustomerController.AddCustomerToDB();
                    Console.Clear();
                    Console.WriteLine("Customer has been Registered");
                }
                else if (option1 == "c")
                {
                    Console.Clear();
                    CustomerController.DisplayCustomers();
                }
                else if (option1 == "u")
                {
                    Console.WriteLine("");
                    Console.Write("Enter your ID number: ");

                    var idnumber = Console.ReadLine();
                    int ID;
                    while (!int.TryParse(idnumber, out ID))
                    {
                        Console.WriteLine("This is not a ID number!");
                        idnumber = Console.ReadLine();
                    }
                    //requires a second input validation check to make certain that the ID given exists

                    CustomerController.ChangeCustomerName(ID);
                    Console.Clear();
                    Console.WriteLine("Customer Name has been Updated");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"\n ({i}) incorrect input, Please: 'Select a letter from Options'");

                }
                }

            var idnumber2 = Console.ReadLine();
            int ID2;
            while (!int.TryParse(idnumber2, out ID2))
            {
                Console.WriteLine("This is not a ID number!");
                idnumber2 = Console.ReadLine();
            }
            //requires a second input validation check to make certain that the ID given exists


            for (int i = 0; i <= 100; i++)
            {
                Console.WriteLine("");
                string name = CustomerController.FindCustomerName(CCustomer.CustomerId);
                Console.Write($"Welcome {name}, ");
                Console.WriteLine(LocationController.FindLocationName(ID2));
                InventoryController.DisplayInventory(ID2);
                Console.Write("options: 'p' Place Order, 'x' Exit, 'v' view location Order History, 'y' view your own Order History: ");
                string options2 = Console.ReadLine();
                List<Orders> orders = new List<Orders>();
                if (options2 == "p")
                {
                    OrderHistoryController.AddToOrderHistory(CCustomer.CustomerId, ID2);
                    int oID = OrderHistoryController.NewOrderID();
                    Console.Clear();

                    for (int z = 0; z <= 100; z++)
                    {
                        
                        InventoryController.DisplayInventory(ID2);
                        OrdersController.DisplayCurrentOrder(orders);
                       

                        Console.WriteLine("options: 'a' Add to order, 'c' complete order: ");
                        string option3 = Console.ReadLine();
                        if (option3 == "a")
                        {
                            Console.WriteLine("");
                            Console.Write("Select Product ID: ");

                            var idnumber3 = Console.ReadLine();
                            int PID;
                            while (!int.TryParse(idnumber3, out PID))
                            {
                                Console.WriteLine("This is not a ID number!");
                                idnumber3 = Console.ReadLine();
                            }
                            //requires a second input validation check to make certain that the ID given exists

                            Console.WriteLine("");
                            Console.Write("Select Amount: ");

                            var idnumber4 = Console.ReadLine();
                            int a;
                            while (!int.TryParse(idnumber4, out a))
                            {
                                Console.WriteLine("This is not a number!");
                                idnumber4 = Console.ReadLine();
                            }

                            var O = new Orders { ProductId = PID, OrderId = oID, Amount = a };
                            orders.Add(O);
                            Console.Clear();
                        }
                        else if (option3 == "c")
                        {
                            foreach (var order in orders)
                            {
                                int a = InventoryController.RemoveFromInventory(ID2, order.ProductId, order.Amount);
                                if (a == 0)
                                {

                                }
                                else
                                {
                                    OrdersController.AddToOrders(order.OrderId, order.ProductId, a);
                                }
                                
                                
                                
                            }
                            Console.WriteLine("your order has been completed, check your order history for more details");
                            z = 200;
                            Console.WriteLine("Enter any key to Continue: ");
                            Console.ReadKey(true);
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"\n ({z}) incorrect input, Please: 'Select a letter from Options'");
                        }
                    }
                    //break;
                }
                else if (options2 == "x")
                {
                    //DeleteUnusedOrderHistory(oID);
                    //This requires that a sql script is run on DataBase: 
                    //DBCC CHECKIDENT (OrderHistory, RESEED, 1);
                    //DBCC CHECKIDENT (OrderHistory, RESEED);

                    break;
                }
                else if (options2 == "v")
                {
                    Console.WriteLine("");
                    OrderHistoryController.DisplayOrderHistoryLocation(ID2);
                    Console.WriteLine("Enter any key to Continue: ");
                    Console.ReadKey(true);
                    Console.Clear();
                }
                else if (options2 == "y")
                {
                    Console.WriteLine("");
                    OrderHistoryController.DisplayOrderHistoryCustomer(CCustomer.CustomerId);
                    Console.WriteLine("Enter any key to Continue: ");
                    Console.ReadKey(true);
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"\n ({i}) incorrect input, Please: 'Select a letter from Options'");
                }
            //Console.ReadKey(true);
            }
        }







        /*
        ///<summary>This will load all saved data for Inventory1</summary>
        string filePathInventory1 = @"D:\Git Bash\Git Path\daniel-project0\Inventory1.txt";
        string loadInventory1 = System.IO.File.ReadAllText(filePathInventory1);
        List<Product> Inventory1 = JsonConvert.DeserializeObject<List<Product>>(loadInventory1);

        ///<summary>This will load all saved data for customers </summary>
        string filePathCustomers = @"D:\Git Bash\Git Path\daniel-project0\Customers.txt";
        string loadCustomers = System.IO.File.ReadAllText(filePathCustomers);
        List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(loadCustomers);

        ///<summary>save all data for Inventory1 to txt file Inventory1</summary>
        string saveInventory1 = JsonConvert.SerializeObject(Inventory1, Formatting.Indented);
        System.IO.File.WriteAllText(filePathInventory1, saveInventory1);

        ///<summary>save all data for Inventory1 to txt file customers</summary>
        string saveCustomers = JsonConvert.SerializeObject(customers, Formatting.Indented);
        System.IO.File.WriteAllText(filePathCustomers, saveCustomers);
        */

    }
}
 