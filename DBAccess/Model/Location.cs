using System;
using System.Collections.Generic;

namespace DBAccess.Model
{
    public partial class Location
    {
        public Location()
        {
            Inventory = new HashSet<Inventory>();
            OrderHistory = new HashSet<OrderHistory>();
        }

        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<OrderHistory> OrderHistory { get; set; }
    }
}
