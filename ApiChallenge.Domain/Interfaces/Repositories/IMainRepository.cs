using ApiChallenge.Domain.DTOs;
using ApiChallenge.Domain.Entities;

namespace ApiChallenge.Domain.Interfaces.Repositories {
    public interface IMainRepository {
        decimal FindTax(Transaction transaction);
        IEnumerable<MerchantDiscountRatesDTO> GetAllMerchantDiscountRates();
    }
}
