using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Project0.Library.models
{
    public class Customer : IEquatable<Customer>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        public override string ToString()
        {
            return "Name: " + FullName;
        }
        //this will bring up order history for the customer
        public void CustomerLog() { }

        public bool Equals(Customer other)
        {
            if (other == null) return false;
            return (this.FullName.Equals(other.FullName));
        }
    }
}
