using System;
using System.Collections.Generic;
using JOIEnergy.Domain;
using JOIEnergy.Enums;
using JOIEnergy.Services;
using Xunit;

namespace JOIEnergy.Tests
{
    public class AccountServiceTest
    {
        private const Supplier PRICE_PLAN_ID = Supplier.PowerForEveryone;
        private const string SMART_METER_ID = "smart-meter-id";

        private AccountService accountService;
        private TestDatasetService dataSetService = new TestDatasetService();

        public AccountServiceTest()
        {
            dataSetService.SetSmartMeterAccounts(new List<SmartMeterAccount> {
                new SmartMeterAccount {
            SmartMeterId = SMART_METER_ID,
            Supplier = PRICE_PLAN_ID
            } });
            accountService = new AccountService(dataSetService);
        }

        [Fact]
        public void GivenTheSmartMeterIdReturnsThePricePlanId()
        {
            var result = accountService.GetSupplierForSmartMeter("smart-meter-id");
            Assert.Equal(Supplier.PowerForEveryone, result);
        }

        [Fact]
        public void GivenAnUnknownSmartMeterIdReturnsANullSupplier()
        {
            var result = accountService.GetSupplierForSmartMeter("bob-carolgees");
            Assert.Equal(Supplier.NullSupplier, result);
        }
    }
}
