using DBAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project0.Library.models
{
    public static class LocationController
    {
        public static readonly String connectionString = System.IO.File.ReadAllText("C:/Users/james/Desktop/Revature/Project0Connect.txt");

        public static readonly DbContextOptions<Project01Context> Options = new DbContextOptionsBuilder<Project01Context>()
            .UseSqlServer(connectionString)
                .Options;

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

        public static string FindLocationName(int ID2)
        {
            using var context = new Project01Context(Options);
            var location = context.Location.Find(ID2);


            return $"You have Arrived at {location.Name}";
        }



    }
}
