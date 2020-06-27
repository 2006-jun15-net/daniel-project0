using System;
using System.Collections.Generic;
using System.Linq;
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
                Console.WriteLine($"Amount: {inventory.Amount} Cost per {inventory.Product.Name} = ${inventory.Product.Price} ");
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
                Console.WriteLine(FindLocationName(ID2));
                DisplayInventory(ID2);
                Console.ReadKey(true);
                break;
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

            ///<summary>The UI starts with the user name, this will result in the creation of 
            ///a new customer if the name is not already created</summary>

            
            for (int i = 0; i<= 100; i++)
            {
                Console.WriteLine("\nAre you a NEW or RETURNING Customer?");
                Console.WriteLine("\n Options: ('n') for new, ('r') for returning: ");
                var option1 = Console.ReadLine();
                if (option1 == "r")
                {
                    break;
                }
                else if (option1 == "n")
                {
                    Console.WriteLine("\nEnter First Name: ");
                    var name1 = Console.ReadLine();
                    Console.WriteLine("\nEnter Last Name: ");
                    var name2 = Console.ReadLine();
                    string fname = $"{name1} {name2}";
                    customers.Add(new Customer() { FirstName = name1, LastName = name2, FullName = fname });
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"\n ({i}) incorrect input, please type in r or n");
                   
                }
            }
            //need to place check against list for match
            Console.WriteLine("\nEnter Full Customer Name: ");

            var name = Console.ReadLine();
            var date = DateTime.Now;
            Console.WriteLine($"\nHello, {name}, on {date:d} at {date:t}!");
            Console.Write("\nPress any key to veiw inventory: ");
            Console.ReadKey(true);
            Console.WriteLine();

            foreach (Product aProduct in Inventory1)
            {
                Console.WriteLine(aProduct);
            }

            
            Console.Write("\nPress any key to exit...");
            Console.ReadKey(true);

            //new Location { Name = "North", Inventory = Inventory1 };

            ///<summary>save all data for Inventory1 to txt file Inventory1</summary>
            string saveInventory1 = JsonConvert.SerializeObject(Inventory1, Formatting.Indented);
            System.IO.File.WriteAllText(filePathInventory1, saveInventory1);

            ///<summary>save all data for Inventory1 to txt file customers</summary>
            string saveCustomers = JsonConvert.SerializeObject(customers, Formatting.Indented);
            System.IO.File.WriteAllText(filePathCustomers, saveCustomers);
            */
        
    }
}
 