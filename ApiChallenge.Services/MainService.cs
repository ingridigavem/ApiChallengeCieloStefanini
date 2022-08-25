using ApiChallenge.Domain.DTOs;
using ApiChallenge.Domain.Entities;
using ApiChallenge.Domain.Interfaces.Repositories;
using ApiChallenge.Domain.Interfaces.Services;

namespace ApiChallenge.Services {
    public class MainService : IMainService {
        private readonly IMainRepository _repository;
        public MainService(IMainRepository repository) {
            _repository = repository;
        }

        public PostResult CalculateTax(Transaction transaction) {
            try {
                decimal taxa = _repository.FindTax(transaction);
                if (taxa < 0) throw new Exception("Valor negativo para taxa.");
                
                decimal valorLiquido = (transaction.Valor - taxa);
                return new PostResult(valorLiquido);

            } catch (Exception) {
                throw;
            }
        }

        public IEnumerable<MerchantDiscountRatesDTO> GetAllMerchantDiscountRates() {
            try {
                return _repository.GetAllMerchantDiscountRates();
            } catch (Exception) {

                throw;
            }
        }
    }
}