using JOIEnergy.Controllers;
using JOIEnergy.Domain;
using JOIEnergy.Enums;
using JOIEnergy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using Newtonsoft.Json.Linq;
using JOIEnergy.Dataset;

namespace JOIEnergy.Tests
{
    public class EnergyCostCalculationTest
    {
        private MeterReadingService meterReadingService;
        private EneryCostCalclationService service;
        private static String SMART_METER_ID = "smart-meter-id";
        private static Supplier SUPPLIER = Supplier.PowerForEveryone;
        private TestDatasetService dataSetService = new TestDatasetService();
        public EnergyCostCalculationTest()
        {
            dataSetService.SetSmartMeterAccounts(new List<SmartMeterAccount> {
                new SmartMeterAccount {
                    SmartMeterId = SMART_METER_ID,
                    Supplier = SUPPLIER
                }
            });
            dataSetService.SetReadings(new Dictionary<string, List<Domain.ElectricityReading>>());
            dataSetService.SetPricePlans(new List<PricePlan>() {
                new PricePlan() { EnergySupplier = Supplier.DrEvilsDarkEnergy, UnitRate = 10, PeakTimeMultiplier =new List<PeakTimeMultiplier>() },
                new PricePlan() { EnergySupplier = Supplier.TheGreenEco, UnitRate = 2, PeakTimeMultiplier = new List<PeakTimeMultiplier>() },
                new PricePlan() { EnergySupplier = Supplier.PowerForEveryone, UnitRate = 1, PeakTimeMultiplier = new List<PeakTimeMultiplier>() }
            });

            meterReadingService = new MeterReadingService(dataSetService);
            service = new EneryCostCalclationService(dataSetService, meterReadingService);
        }

        [Fact]
        public void GivenMatchingMeterIdShouldReturnLastWeekCost()
        {
            meterReadingService.StoreReadings(SMART_METER_ID, GenerateReadings(9));

            object result = service.GetConsumptionCostOfElectricityReadingsForDuration(SMART_METER_ID, 7);
            var cost = 5 * 168 * 1;
            Assert.Equal(cost, result);
        }

        public static List<ElectricityReading> GenerateReadings(int number)
        {
            var readings = new List<ElectricityReading>();
            var random = new Random();
            for (int i = 0; i < number; i++)
            {
                //var reading = (decimal)random.NextDouble();
                var electricityReading = new ElectricityReading
                {
                    Reading = i + 1,
                    Time = DateTime.Now.AddDays(-i)
                };
                readings.Add(electricityReading);
            }
            readings.Sort((reading1, reading2) => reading1.Time.CompareTo(reading2.Time));
            return readings;
        }
    }
}
