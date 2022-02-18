using JOIEnergy.Enums;

namespace JOIEnergy.Services
{
    public interface IAccountService
    {
        Supplier GetSupplierForSmartMeter(string smartMeterId);
    }
}