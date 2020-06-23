using Library;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Project0.Library.models
{
    public class Location
    {

        //method to reduce inventory when orders are
        //placed

        //method to reject orders that canot be fulfilled
        //with remaining inventory

        
        public string Name { get; set; }

        public List<Product> Inventory { get; set; }


        //this will bring up order history for the location
        public void LocationLog() { }    



       
    }
}
