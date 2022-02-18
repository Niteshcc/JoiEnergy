using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JOIEnergy.Controllers
{
    [Route("price-plans")]
    public class PricePlanComparatorController : Controller
    {
        private readonly IEnergyCostCalculationService _pricePlanService;

        public PricePlanComparatorController(IEnergyCostCalculationService pricePlanService)
        {
            this._pricePlanService = pricePlanService;
        }

        [HttpGet("compare-all/{smartMeterId}")]
        public ObjectResult CalculatedCostForEachPricePlan(string smartMeterId)
        {
            var costPerPricePlan = _pricePlanService.GetConsumptionCostOfElectricityReadingsForEachPricePlan(smartMeterId);
            return costPerPricePlan.Any() ? new ObjectResult(JObject.FromObject(costPerPricePlan)) :
                new NotFoundObjectResult(string.Format("Smart Meter ID ({0}) not found", smartMeterId));
        }

        [HttpGet("recommend/{smartMeterId}")]
        public ObjectResult RecommendCheapestPricePlans(string smartMeterId, int? limit = null) {
            var consumptionForPricePlans = _pricePlanService.GetConsumptionCostOfElectricityReadingsForEachPricePlan(smartMeterId);

            if (!consumptionForPricePlans.Any()) {
                return new NotFoundObjectResult(string.Format("Smart Meter ID ({0}) not found", smartMeterId));
            }

            var recommendations = consumptionForPricePlans.OrderBy(pricePlanComparison => pricePlanComparison.Value);

            if (limit.HasValue && limit.Value < recommendations.Count())
            {
                return new ObjectResult(recommendations.Take(limit.Value));
            }

            return new ObjectResult(recommendations);
        }
    }
}
