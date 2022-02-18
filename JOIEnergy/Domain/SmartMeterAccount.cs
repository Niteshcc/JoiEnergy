using JOIEnergy.Enums;
using System;

namespace JOIEnergy.Domain
{
    public class SmartMeterAccount
    {
        public string SmartMeterId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
