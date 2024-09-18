using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class BusinessExchangeRateList {
        public int Id { get; set; } = 0;
        public int CurrencyId { get; set; }
        public decimal Value { get; set; }
        public DateTime? ValidFrom { get; set; } = null;
        public DateTime? ValidTo { get; set; } = null;
        public string Description { get; set; } = null;
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public partial class BusinessExtendedExchangeRateList : BusinessExchangeRateList {
        public string Currency { get; set; }
    }
}