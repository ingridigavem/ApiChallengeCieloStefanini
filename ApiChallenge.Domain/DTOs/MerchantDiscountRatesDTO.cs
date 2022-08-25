using ApiChallenge.Domain.Entities;

namespace ApiChallenge.Domain.DTOs {
    public class MerchantDiscountRatesDTO {
        public string Adquirente { get; set; }
        public List<Tax> Taxas { get; set; }
    }
}
