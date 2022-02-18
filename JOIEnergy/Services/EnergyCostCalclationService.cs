using System;
using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Dataset;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public class EneryCostCalclationService : IEnergyCostCalculationService
    {
        private readonly IDatasetService _dataSet;
        private IMeterReadingService _meterReadingService;

        public EneryCostCalclationService(IDatasetService dataSet, IMeterReadingService meterReadingService)
        {
            _dataSet = dataSet;
            _meterReadingService = meterReadingService;
        }

        private decimal calculateAverageReading(List<ElectricityReading> electricityReadings)
        {
            var newSummedReadings = electricityReadings.Select(readings => readings.Reading).Aggregate((reading, accumulator) => reading + accumulator);

            return newSummedReadings / electricityReadings.Count();
        }

        private decimal calculateTimeElapsed(List<ElectricityReading> electricityReadings)
        {
            var first = electricityReadings.Min(reading => reading.Time);
            var last = electricityReadings.Max(reading => reading.Time);

            return (decimal)(last - first).TotalHours;
        }
        private decimal calculateCost(List<ElectricityReading> electricityReadings, PricePlan pricePlan)
        {
            var average = calculateAverageReading(electricityReadings);
            var timeElapsed = calculateTimeElapsed(electricityReadings);
            var averagedCost = average / timeElapsed;
            return averagedCost * pricePlan.UnitRate;
        }

        public Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(String smartMeterId)
        {
            List<ElectricityReading> electricityReadings = _meterReadingService.GetReadings(smartMeterId);

            if (!electricityReadings.Any())
            {
                return new Dictionary<string, decimal>();
            }
            return this._dataSet.PricePlans.ToDictionary(plan => plan.EnergySupplier.ToString(), plan => calculateCost(electricityReadings, plan));
        }

        public decimal GetConsumptionCostOfElectricityReadingsForDuration(string smartMeterId, int dayCount)
        {
            return 100m;
        }
    }
}
