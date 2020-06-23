using System;
using System.Collections.Generic;
using System.Data.Common;
using Library;
using Project0.Library.models;

namespace Projec0.app
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nEnter Customer Name: ");
            var name = Console.ReadLine();
            var date = DateTime.Now;
            Console.WriteLine($"\nHello, {name}, on {date:d} at {date:t}!");
            Console.Write("\nPress any key to exit...");
            Console.ReadKey(true);

           
            



            var inventory1 = new List<Product>
            {
                    new Product
                {
                    Name = "p1",
                    Amount = 10,
                    Price = 25,
                },
                    new Product
                {
                    Name = "p2",
                    Amount = 3,
                    Price = 8,
                },
                    new Product
                {
                    Name = "p3",
                    Amount = 17,
                    Price = 25,
                },
                    new Product
                {
                    Name = "p4",
                    Amount = 4,
                    Price = 9,
                }
            };

            new Location { Name = "North", Inventory = inventory1 };
        }
    }
}
 