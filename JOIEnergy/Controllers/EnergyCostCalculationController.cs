using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JOIEnergy.Controllers
{
    [Route("energy-cost")]
    public class EnergyCostCalculationController : Controller
    {
        private readonly IEnergyCostCalculationService _pricePlanService;

        public EnergyCostCalculationController(IEnergyCostCalculationService pricePlanService)
        {
            this._pricePlanService = pricePlanService;
        }

        [HttpGet("{smartMeterId}")]
        public ObjectResult CalculatedCostFoLastWeek(string smartMeterId)
        {
            var costPerPricePlan = _pricePlanService.GetConsumptionCostOfElectricityReadingsForDuration(smartMeterId, 7);
            return Ok(costPerPricePlan);
        }
    }
}
