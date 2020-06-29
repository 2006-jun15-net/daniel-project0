using DBAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project0.Library.models
{


    public static class CustomerController
    {
        public static readonly String connectionString = System.IO.File.ReadAllText("C:/Users/james/Desktop/Revature/Project0Connect.txt");

        public static readonly DbContextOptions<Project01Context> Options = new DbContextOptionsBuilder<Project01Context>()
            .UseSqlServer(connectionString)
                .Options;

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


        public static void AddCustomerToDB()
        {
            Console.WriteLine("Enter a new Customer firstname: ");
            var firstname = Console.ReadLine();
            while (string.IsNullOrEmpty(firstname))
            {
                Console.WriteLine("First Name can't be empty! Input your first name once more");
                firstname = Console.ReadLine();
            }
            Console.WriteLine("Enter a new Customer lastname: ");
            var lastname = Console.ReadLine();
            while (string.IsNullOrEmpty(lastname))
            {
                Console.WriteLine("Last Name can't be empty! Input your last name once more");
                lastname = Console.ReadLine();
            }
            using var context = new Project01Context(Options);

            var customer = new Customer { FirstName = firstname, LastName = lastname };

            context.Customer.Add(customer);

            context.SaveChanges();
        }

        public static string FindCustomerName(int ID)
        {
            using var context = new Project01Context(Options);
            var customer = context.Customer.Find(ID);


            return $"{customer.FirstName} {customer.LastName}";
        }



        public static void ChangeCustomerName(int ID)
        {
            using var context = new Project01Context(Options);
            var customer = context.Customer.Find(ID);
            Console.WriteLine("Enter a new Customer firstname: ");
            customer.FirstName = Console.ReadLine();

            while (string.IsNullOrEmpty(customer.FirstName))
            {
                Console.WriteLine("First Name can't be empty! Input your first name once more");
                customer.FirstName = Console.ReadLine();
            }

            Console.WriteLine("Enter a new Customer lastname: ");
            customer.LastName = Console.ReadLine();
            while (string.IsNullOrEmpty(customer.LastName))
            {
                Console.WriteLine("Last Name can't be empty! Input your last name once more");
                customer.LastName = Console.ReadLine();
            }


            context.SaveChanges();
        }








    }
}