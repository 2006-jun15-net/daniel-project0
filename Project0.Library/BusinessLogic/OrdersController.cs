using DBAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.models
{
    public static class OrdersController
    {
        public static readonly String connectionString = System.IO.File.ReadAllText("C:/Users/james/Desktop/Revature/Project0Connect.txt");

        public static readonly DbContextOptions<Project01Context> Options = new DbContextOptionsBuilder<Project01Context>()
            .UseSqlServer(connectionString)
                .Options;

        public static void DisplayCurrentOrder(List<Orders> orders)
        {
            Console.WriteLine("");
            Console.WriteLine("Currently in shopping cart:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Item ID: [{order.ProductId}] and amount: {order.Amount}");
            }
        }

        public static void AddToOrders(int orderid, int productid, int a)
        {
            using var context = new Project01Context(Options);
            var Order = new Orders { Amount = a, OrderId = orderid, ProductId = productid };
            context.Orders.Add(Order);
            context.SaveChanges();
        }

    }
}
