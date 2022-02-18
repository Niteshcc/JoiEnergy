using FluentValidation;
using JOIEnergy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JOIEnergy.Validators
{
    public class MeterReadingsValidator: AbstractValidator<MeterReadings>
    {
        public MeterReadingsValidator()
        {
            RuleFor(meterReadings => meterReadings.SmartMeterId).NotEmpty();
            RuleFor(meterReadings => meterReadings.ElectricityReadings).NotEmpty();
        }
    }
}
