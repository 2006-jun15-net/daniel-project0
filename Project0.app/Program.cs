using System;
using System.Collections.Generic;
using Library;
using Newtonsoft.Json;
using Project0.Library.models;

namespace Projec0.app
{
    class Program
    {
        static void Main(string[] args)
        {

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

        }
    }
}
 