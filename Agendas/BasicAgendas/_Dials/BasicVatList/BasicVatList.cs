﻿using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class BasicVatList {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = null;
        public decimal Value { get; set; }
        public bool Default { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}