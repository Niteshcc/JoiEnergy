using JOIEnergy.Domain;
using JOIEnergy.Enums;
using JOIEnergy.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JOIEnergy.Generator
{
    public static class MockDataGenerator
    {
        public static List<SmartMeterAccount> GenerateSmartMeterAccounts()
        {
            List<SmartMeterAccount> smartMeterAccounts = new List<SmartMeterAccount>();
            smartMeterAccounts.Add(new SmartMeterAccount
            { SmartMeterId = "smart-meter-0", Supplier = Supplier.DrEvilsDarkEnergy });
            smartMeterAccounts.Add(new SmartMeterAccount
            { SmartMeterId = "smart-meter-1", Supplier = Supplier.TheGreenEco });
            smartMeterAccounts.Add(new SmartMeterAccount
            { SmartMeterId = "smart-meter-2", Supplier = Supplier.DrEvilsDarkEnergy });
            smartMeterAccounts.Add(new SmartMeterAccount
            { SmartMeterId = "smart-meter-3", Supplier = Supplier.PowerForEveryone });
            smartMeterAccounts.Add(new SmartMeterAccount
            { SmartMeterId = "smart-meter-4", Supplier = Supplier.TheGreenEco });

            return smartMeterAccounts;
        }

        public static Dictionary<String, List<ElectricityReading>> GenerateMeterElectricityReadings(List<SmartMeterAccount> smartMeterAccounts)
        {
            Dictionary<String, List<ElectricityReading>> result = new Dictionary<string, List<ElectricityReading>>();
            var smartMeterIds = smartMeterAccounts.Select(mtpp => mtpp.SmartMeterId);

            foreach (var smartMeterId in smartMeterIds)
            {
                result[smartMeterId] = ElectricityReadingGenerator.Generate(20);
            }

            return result;
        }

        public static List<PricePlan> GeneratePricePlans()
        {
            var result = new List<PricePlan>();
            result.Add(new PricePlan
            {
                EnergySupplier = Enums.Supplier.DrEvilsDarkEnergy,
                UnitRate = 10m,
                PeakTimeMultiplier = new List<PeakTimeMultiplier>{
                    new PeakTimeMultiplier { DayOfWeek = DayOfWeek.Tuesday, Multiplier = 0.5m } }
            });

            result.Add(new PricePlan
            {
                EnergySupplier = Enums.Supplier.TheGreenEco,
                UnitRate = 2m,
                PeakTimeMultiplier = new List<PeakTimeMultiplier>{
                    new PeakTimeMultiplier { DayOfWeek = DayOfWeek.Tuesday, Multiplier = 2.8m } }
            });

            result.Add(new PricePlan
            {
                EnergySupplier = Enums.Supplier.PowerForEveryone,
                UnitRate = 1m,
                PeakTimeMultiplier = new List<PeakTimeMultiplier>{
                    new PeakTimeMultiplier { DayOfWeek = DayOfWeek.Tuesday, Multiplier = 2.5m } }
            });

            return result;
        }
    }
}
