using System;
using System.Collections.Generic;

namespace DBAccess.Model
{
    public partial class OrderHistory
    {
        public OrderHistory()
        {
            Orders = new HashSet<Orders>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
