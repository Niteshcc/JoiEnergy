using JOIEnergy.Domain;
using JOIEnergy.Generator;
using System;
using System.Collections.Generic;

namespace JOIEnergy.Dataset
{
    public class DatasetService : IDatasetService
    {
        public List<SmartMeterAccount> SmartMeterAccounts { get; private set; }
        public Dictionary<string, List<ElectricityReading>> Readings { get; private set; }
        public List<PricePlan> PricePlans { get; private set; }

        public DatasetService()
        {
            SmartMeterAccounts = MockDataGenerator.GenerateSmartMeterAccounts();
            Readings = MockDataGenerator.GenerateMeterElectricityReadings(SmartMeterAccounts);
            PricePlans = MockDataGenerator.GeneratePricePlans();
        }
    }
}
