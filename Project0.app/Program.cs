using System;
using System.Collections.Generic;
using System.Linq;
using DBAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Projec0.app
{
    public class Program
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });
       
        public static readonly String connectionString = System.IO.File.ReadAllText("C:/Users/james/Desktop/Revature/Project0Connect.txt");
        
        public static readonly DbContextOptions<Project01Context> Options = new DbContextOptionsBuilder<Project01Context>()
                .UseLoggerFactory(MyLoggerFactory)
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


        static void Main(string[] args)
        {
            
            
            for (int i = 0; i <= 100; i++)
            {
                Console.WriteLine("\nAre you a NEW or RETURNING Customer?");
                Console.WriteLine("\n Options: CustomerList('c'), ('n') for new, ('r') for returning: ");
                var option1 = Console.ReadLine();
                if (option1 == "r")
                {
                    break;
                }
                else if (option1 == "n")
                {
                    AddCustomerToDB();
                    break;
                }
                else if (option1 == "c")
                {
                    DisplayCustomers();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"\n ({i}) incorrect input, please type in r or n");

                }
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
 