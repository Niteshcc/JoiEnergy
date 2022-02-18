using System;
using System.Collections.Generic;
using JOIEnergy.Dataset;
using JOIEnergy.Enums;
using System.Linq;

namespace JOIEnergy.Services
{
    public class AccountService : IAccountService
    { 
        private readonly IDatasetService _dataSet;

        public AccountService(IDatasetService dataSet) {
            _dataSet = dataSet;
        }

        public Supplier GetSupplierForSmartMeter(string smartMeterId) {
            var account = _dataSet.SmartMeterAccounts.FirstOrDefault((x) => x.SmartMeterId == smartMeterId);
            return account != null ? account.Supplier : Supplier.NullSupplier;
        }
    }
}
