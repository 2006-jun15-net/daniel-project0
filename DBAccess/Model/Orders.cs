using System;
using System.Collections.Generic;

namespace DBAccess.Model
{
    public partial class Orders
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public virtual OrderHistory Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
