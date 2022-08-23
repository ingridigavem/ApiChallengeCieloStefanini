using ApiChallenge.Repositories.Models;

namespace Repositories {
    public class MerchantDiscountRatesDTO {
        private IEnumerable<IGrouping<object, MerchantDiscountRates>> testeAgrupado;

        public string Adquirente { get; set; }
        public List<Tax> Taxas { get; set; }
    }
}
