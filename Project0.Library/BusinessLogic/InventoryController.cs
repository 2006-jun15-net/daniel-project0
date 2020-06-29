using DBAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project0.Library.models
{
    public static class InventoryController
    {
        public static readonly String connectionString = System.IO.File.ReadAllText("C:/Users/james/Desktop/Revature/Project0Connect.txt");

        public static readonly DbContextOptions<Project01Context> Options = new DbContextOptionsBuilder<Project01Context>()
            .UseSqlServer(connectionString)
                .Options;

        public static int RemoveFromInventory(int ID2, int productid, int amount)
        {
            using var context = new Project01Context(Options);
            var inventory = context.Inventory
            .FirstOrDefault(e => e.LocationId == ID2 && e.ProductId == productid);

            int a = inventory.Amount;
            inventory.Amount -= amount;


            if (inventory.Amount <= 0)
            {
                Console.WriteLine("there is not enough stock in this store's inventory to fullfill that order");
                Console.WriteLine($"You have bought out the whole stock of ProductID: [{productid}]");
                Console.WriteLine("Enter any key to Continue: ");
                Console.ReadKey(true);
                Console.Clear();
                inventory.Amount = 0;
                context.Inventory.Update(inventory);
                context.SaveChanges();
                return a;
            }
            else
            {
                context.Inventory.Update(inventory);
                context.SaveChanges();
                return amount;
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
    }
}
