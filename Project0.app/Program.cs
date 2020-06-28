using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Threading;
using DBAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public static void DisplayCurrentOrder(List<Orders> orders)
        {
            Console.WriteLine("Currently in shopping cart:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Item ID: [{order.ProductId}] and amount: {order.Amount}");
            }
        }
        public static int NewOrderID()
        {
            using var context = new Project01Context(Options);
            int orderHistory = context.OrderHistory.Count();

            return orderHistory;
        }

        private static void AddToOrderHistory(int customerID, int locationID)
        {
            using var context = new Project01Context(Options);

            string date = DateTime.UtcNow.ToString("MM-dd-yyyy");
            string time = DateTime.Now.ToString("HH:mm");
           
          
            var orderHistory = new OrderHistory { CustomerId = customerID, LocationId = locationID, Date = date, Time = time};
            context.OrderHistory.Add(orderHistory);
            context.SaveChanges();
        }

        public static void RemoveFromInventory(int ID2, int productid, int amount)
        {
            using var context = new Project01Context(Options);
            var inventory = context.Inventory
            .FirstOrDefault(e => e.LocationId == ID2 && e.ProductId == productid);
            
            inventory.Amount -= amount;

            context.Inventory.Update(inventory);
            if (inventory.Amount < 0)
            {
                //Error
            }
            else
            {
                context.SaveChanges();
            }
            
        }

        public static void AddToOrders(int orderid, int productid, int amount)
        {
            using var context = new Project01Context(Options);
            var Order = new Orders { Amount = amount, OrderId = orderid, ProductId = productid };
            context.Orders.Add(Order); 
            context.SaveChanges();
        }

        public static void AddCustomerToDB()
        {
            Console.WriteLine("Enter a new Customer firstname: ");
            var firstname = Console.ReadLine();
            Console.WriteLine("Enter a new Customer lastname: ");
            var lastname = Console.ReadLine();

            using var context = new Project01Context(Options);

            var customer = new Customer { FirstName = firstname, LastName = lastname };
            //context.Add(Customer);
            context.Customer.Add(customer);
            //context.Student.Update(student);

            context.SaveChanges();
        }

        public static void DisplayCustomers()
        {
            using var context = new Project01Context(Options);
            List<Customer> customers = context.Customer
                .ToList();

            foreach (var customer in customers)
            {
                Console.WriteLine($"[{customer.CustomerId}] {customer.FirstName} {customer.LastName}");
            }
        }

        public static void DisplayInventory(int ID2)
        {
            using var context = new Project01Context(Options);
            List<Inventory> inventories = context.Inventory
                .Include(s => s.Product)
                .Where(e => e.LocationId == ID2)
                .ToList();

            foreach (var inventory in inventories)
            {
                Console.WriteLine($"Product ID: [{inventory.ProductId}] Amount: {inventory.Amount} Cost per {inventory.Product.Name} = ${inventory.Product.Price} ");
            }
        }

        public static void DisplayLocations()
        {
            using var context = new Project01Context(Options);
            List<Location> locations = context.Location
                .ToList();

            foreach (var location in locations)
            {
                Console.WriteLine($"[{location.LocationId}] {location.Name}: Simply {location.Address} to reach your destination");
            }
        }

        public static string FindCustomerName(int ID)
        {
            using var context = new Project01Context(Options);
            var customer = context.Customer.Find(ID);
            
            
            return $"{customer.FirstName} {customer.LastName}";
        }
        public static string FindLocationName(int ID2)
        {
            using var context = new Project01Context(Options);
            var location = context.Location.Find(ID2);


            return $"You have Arrived at {location.Name}";
        }

        public static void ChangeCustomerName(int ID)
        {
            using var context = new Project01Context(Options);
            var customer = context.Customer.Find(ID);
            Console.WriteLine("Enter a new Customer firstname: ");
            customer.FirstName = Console.ReadLine();
            Console.WriteLine("Enter a new Customer lastname: ");
            customer.LastName = Console.ReadLine();

            context.SaveChanges();
        }

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
                    int ID = int.Parse(Console.ReadLine());
                    CCustomer.CustomerId = ID;
                    string name = FindCustomerName(ID);
                    var date = DateTime.Now;
                    Console.WriteLine($"\nHello, {name}, on {date:d} at {date:t}!");
                    Console.WriteLine("Enter any key to Continue: ");
                    Console.ReadKey(true);
                    Console.Clear();
                    Console.WriteLine("\nSelect a destination: ");
                    DisplayLocations();
                    Console.Write("Select Destination by ID, ex: '1': ");
                    break;
                }
                else if (option1 == "n")
                {
                    AddCustomerToDB();
                    Console.Clear();
                    Console.WriteLine("Customer has been Registered");
                }
                else if (option1 == "c")
                {
                    Console.Clear();
                    DisplayCustomers();
                }
                else if (option1 == "u")
                {
                    Console.WriteLine("");
                    Console.Write("Enter your ID number: ");
                    int ID = int.Parse(Console.ReadLine());
                    ChangeCustomerName(ID);
                    Console.Clear();
                    Console.WriteLine("Customer Name has been Updated");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"\n ({i}) incorrect input, Please: 'Select a letter from Options'");

                }
                }

            var ID2 = int.Parse(Console.ReadLine());
            
            for (int i = 0; i <= 100; i++)
            {
                string name = FindCustomerName(CCustomer.CustomerId);
                Console.Write($"Welcome {name}, ");
                Console.WriteLine(FindLocationName(ID2));
                DisplayInventory(ID2);
                Console.Write("options: 'p' Place Order, 'x' Exit, 'v' view location OrderHistory, 'y' view your own OrderHistory: ");
                string options2 = Console.ReadLine();
                List<Orders> orders = new List<Orders>();
                if (options2 == "p")
                {
                    for (int z = 0; z <= 100; z++)
                    {
                        Console.Clear();
                        DisplayInventory(ID2);
                        DisplayCurrentOrder(orders);
                        AddToOrderHistory(CCustomer.CustomerId, ID2);//needs fixing
                        int oID = NewOrderID();
                        Console.WriteLine($"Order ID: {oID}");

                        Console.WriteLine("options: 'a' Add to order, 'c' complete order: ");
                        string option3 = Console.ReadLine();
                        if (option3 == "a")
                        {
                            Console.WriteLine("");
                            Console.Write("Select Product ID: ");
                            int PID = int.Parse(Console.ReadLine());
                            Console.WriteLine("");
                            Console.Write("Select Amount: ");
                            int a = int.Parse(Console.ReadLine());
                            var O = new Orders { ProductId = PID, OrderId = oID, Amount = a };
                            orders.Add(O);
                        }
                        else if (option3 == "c")
                        {
                            
                            AddToOrderHistory(CCustomer.CustomerId, ID2);//needs fixing
                            
                            
                            foreach (var order in orders)
                            {
                                RemoveFromInventory(ID2, order.ProductId, order.Amount);
                                AddToOrders(order.OrderId, order.ProductId, order.Amount);
                            }
                            
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"\n ({z}) incorrect input, Please: 'Select a letter from Options'");
                        }
                    }
                    break;
                }
                else if (options2 == "x")
                {
                    break;
                }
                else if (options2 == "v")
                {
                    break;
                }
                else if (options2 == "y")
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"\n ({i}) incorrect input, Please: 'Select a letter from Options'");
                }
            Console.ReadKey(true);
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
 