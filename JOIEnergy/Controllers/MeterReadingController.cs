using FluentValidation;
using JOIEnergy.Domain;
using JOIEnergy.Services;
using JOIEnergy.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections;
using JOIEnergy.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JOIEnergy.Controllers
{
    [Route("readings")]
    
    
    [DummyResourceFilter]
    [DummyAuthorizationFilter]
    public class MeterReadingController : Controller
    {
        private readonly IMeterReadingService _meterReadingService;

        public MeterReadingController(IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService;
        }

        // POST api/values
        [HttpPost ("store")]
        public ObjectResult Post([FromBody]MeterReadings meterReadings)
        {
            var validator = new MeterReadingsValidator();
            var result = validator.Validate(meterReadings);
            if (!result.IsValid) {
                var errorMessage = result.Errors.Select(x => x.ErrorMessage);
                return new BadRequestObjectResult(errorMessage);
            }

            _meterReadingService.StoreReadings(meterReadings.SmartMeterId,meterReadings.ElectricityReadings);
            return new OkObjectResult("{}");
        }

        [HttpGet("read/{smartMeterId}")]
        public ObjectResult GetReading(string smartMeterId) {
            return new OkObjectResult(_meterReadingService.GetReadings(smartMeterId));
        }
    }
}
