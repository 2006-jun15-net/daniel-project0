using DBAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project0.Library.models
{
    public static class OrderHistoryController
    {
        public static readonly String connectionString = System.IO.File.ReadAllText("C:/Users/james/Desktop/Revature/Project0Connect.txt");

        public static readonly DbContextOptions<Project01Context> Options = new DbContextOptionsBuilder<Project01Context>()
            .UseSqlServer(connectionString)
                .Options;

        public static int NewOrderID()
        {
            using var context = new Project01Context(Options);
            int orderHistory = context.OrderHistory.Count();

            return orderHistory;
        }

        public static void AddToOrderHistory(int customerID, int locationID)
        {


            using var context = new Project01Context(Options);

            string date = DateTime.UtcNow.ToString("MM-dd-yyyy");
            string time = DateTime.Now.ToString("HH:mm");


            var orderHistory = new OrderHistory { CustomerId = customerID, LocationId = locationID, Date = date, Time = time };
            context.OrderHistory.Add(orderHistory);
            context.SaveChanges();
        }

        public static void DeleteUnusedOrderHistory(int ID)
        {
            using var context = new Project01Context(Options);
            var orderHistory = context.OrderHistory
            .FirstOrDefault(e => e.OrderId == ID);


            context.OrderHistory.Remove(orderHistory);
            context.SaveChanges();

            //To prevent identity column errors this sql code must be run:
            //DBCC CHECKIDENT (OrderHistory, RESEED, 1);
            //DBCC CHECKIDENT (OrderHistory, RESEED);

        }


        public static void DisplayOrderHistoryLocation(int ID2)
        {
            using var context = new Project01Context(Options);
            var orderHistory = context.OrderHistory
                .Where(e => e.LocationId == ID2)
                .ToList();

            foreach(var order in orderHistory)
            {
                Console.WriteLine($"Order ID: [{order.OrderId}], Customer ID: [{order.CustomerId}], Date: {order.Date}, Time: {order.Time}");
            };
        }

        public static void DisplayOrderHistoryCustomer(int ID)
        {
            using var context = new Project01Context(Options);
            var orderHistory = context.OrderHistory
                .Where(e => e.CustomerId == ID)
                .ToList();

            foreach (var order in orderHistory)
            {
                Console.WriteLine($"Order ID: [{order.OrderId}], Location ID: [{order.LocationId}], Date: {order.Date}, Time: {order.Time}");
            };
        }







    }
}
