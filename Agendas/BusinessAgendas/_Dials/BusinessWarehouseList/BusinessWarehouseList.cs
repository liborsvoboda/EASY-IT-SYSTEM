﻿using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class BusinessWarehouseList {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = null;
        public string Description { get; set; }
        public bool AllowNegativeStatus { get; set; }
        public bool Default { get; set; }
        public bool LockedByStockTaking { get; set; }
        public DateTime LastStockTaking { get; set; }
        public bool Active { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}