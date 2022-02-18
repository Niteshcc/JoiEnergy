using System;
using System.Linq;
using System.Collections.Generic;
using JOIEnergy.Dataset;
using JOIEnergy.Domain;
using JOIEnergy.Exceptions;

namespace JOIEnergy.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly IDatasetService _dataSet;
        public MeterReadingService(IDatasetService dataSet)
        {
            _dataSet = dataSet;
        }

        public List<ElectricityReading> GetReadings(string smartMeterId) {
            if (_dataSet.Readings.ContainsKey(smartMeterId)) {
                return _dataSet.Readings[smartMeterId];
            }
            return new List<ElectricityReading>();
        }

        public void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings) {
            if (!_dataSet.SmartMeterAccounts.Any((x) =>x.SmartMeterId == smartMeterId)) {
                throw new BadRequestException(String.Format("Meter with id {0} not found", smartMeterId));
            }

            if (!_dataSet.Readings.ContainsKey(smartMeterId))
            {
                _dataSet.Readings[smartMeterId] = new List<ElectricityReading>();
            }

            electricityReadings.ForEach(electricityReading => _dataSet.Readings[smartMeterId].Add(electricityReading));
        }
    }
}
