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

        //copied from trainer_code
        private string _name;

        //copied from trainer_code
        public string Name
        {
            // expression-body syntax for accessing the backing field.
            // equivalent to "get { return _name; }"
            get => _name;
            set
            {
                // "value" is the value passed to the setter.
                if (value.Length == 0)
                {
                    // good practice to provide useful messages when throwing exceptions,
                    // as well as the name of the relevant parameter if applicable.
                    throw new ArgumentException("Name must not be empty.", nameof(value));
                }
                _name = value;
            }
        }

        public List<Product> Inventory { get; set; }
        
        
            



       
    }
}
