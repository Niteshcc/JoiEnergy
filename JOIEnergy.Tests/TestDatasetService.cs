using JOIEnergy.Dataset;
using JOIEnergy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace JOIEnergy.Tests
{
    class TestDatasetService : IDatasetService
    {
        public List<SmartMeterAccount> SmartMeterAccounts { get; private set; }

        public Dictionary<string, List<ElectricityReading>> Readings { get; private set; }

        public List<PricePlan> PricePlans { get; private set; }

        public TestDatasetService()
        {
            SmartMeterAccounts = new List<SmartMeterAccount>();
            Readings = new Dictionary<string, List<ElectricityReading>>();
            PricePlans = new List<PricePlan>();

        }

        public void SetSmartMeterAccounts(List<SmartMeterAccount> accounts)
        {
            this.SmartMeterAccounts = accounts;
        }
        public void SetReadings(Dictionary<string, List<ElectricityReading>> readings)
        {
            this.Readings = readings;
        }

        public void SetPricePlans(List<PricePlan> pricePlans)
        {
            this.PricePlans = pricePlans;
        }

    }
}
