﻿using System;
using System.Collections.Generic;

namespace DBAccess.Model
{
    public partial class Inventory
    {
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
    }
}
