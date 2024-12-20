﻿using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class BusinessPaymentStatusList {
        public int Id { get; set; }
        public string Name { get; set; } = null;
        public bool Default { get; set; } = false;
        public string Description { get; set; } = null;
        public int UserId { get; set; }
        public bool Active { get; set; }
        public bool Receipt { get; set; }
        public bool CreditNote { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}