using JOIEnergy.Domain;
using System;
using System.Collections.Generic;

namespace JOIEnergy.Dataset
{
    public interface IDatasetService
    {
        List<SmartMeterAccount> SmartMeterAccounts { get; }
        Dictionary<string, List<ElectricityReading>> Readings { get; }
        List<PricePlan> PricePlans { get; }
    }
}
