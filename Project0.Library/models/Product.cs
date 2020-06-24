using System;
using System.Collections.Generic;

namespace Library
{
    public class Product : IEquatable<Product>
    {
        public string Name { get; set; }
        public int PID { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return "ID: " + PID + "  Product Name: " + Name + "  Price: $" + Price + "  Amount: " + Amount;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Product objAsProduct = obj as Product;
            if (objAsProduct == null) return false;
            else return Equals(objAsProduct);
        }
        public override int GetHashCode()
        {
            return PID;
        }
        public bool Equals(Product other)
        {
            if (other == null) return false;
            return (this.PID.Equals(other.PID));
        }

    }
}