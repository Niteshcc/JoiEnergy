using System.Collections.Generic;

namespace JOIEnergy.Services
{
    public interface IEnergyCostCalculationService
    {
        Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId);

       decimal GetConsumptionCostOfElectricityReadingsForDuration(string smartMeterId, int dayCount);
    }
}