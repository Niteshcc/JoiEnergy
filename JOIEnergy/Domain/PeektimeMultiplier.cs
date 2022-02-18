using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JOIEnergy.Domain
{
    public class PeakTimeMultiplier
    {
        public DayOfWeek DayOfWeek { get; set; }
        public decimal Multiplier { get; set; }
    }
}
