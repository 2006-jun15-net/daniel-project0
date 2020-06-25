using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text.Json.Serialization;
using Library;
using Newtonsoft.Json;
using Project0.Library.models;

namespace Projec0.app
{
    class Program
    {
        static void Main(string[] args)
        {

            ///<summary>This will load all saved data from Json</summary>
            string Json = System.IO.File.ReadAllText(@"D:\Git Bash\Git Path\daniel-project0\Inventory1.txt");
            List<Product> Inventory1 = JsonConvert.DeserializeObject<List<Product>>(Json);

            ///<summary>The UI starts with the user name, this will result in the creation of 
            ///a new customer if the name is not already created</summary>

            List<Customer> customers = new List<Customer>
            {
                new Customer() { FirstName = "John", LastName = "Smith", FullName = "John Smith"},
                new Customer() { FirstName = "May", LastName = "Flower", FullName = "May Flower"},
                new Customer() { FirstName = "Daniel", LastName = "Aasa", FullName = "Daniel Aasa"}
            };

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

           /* List<Product> Inventory1 = new List<Product>
            {
                new Product() { PID = 1001, Amount = 54, Name = "1p1", Price = 7 },
                new Product() { PID = 1002, Amount = 23, Name = "1p2", Price = 9 },
                new Product() { PID = 1003, Amount = 5, Name = "1p3", Price = 15 }
            };
           */
            foreach (Product aProduct in Inventory1)
            {
                Console.WriteLine(aProduct);
            }

            
            Console.Write("\nPress any key to exit...");
            Console.ReadKey(true);

            new Location { Name = "North", Inventory = Inventory1 };

            ///<summary>save all data to txt files</summary>
            string json = JsonConvert.SerializeObject(Inventory1, Formatting.Indented);
            System.IO.File.WriteAllText(@"D:\Git Bash\Git Path\daniel-project0\Inventory1.txt", json);

        }
    }
}
 