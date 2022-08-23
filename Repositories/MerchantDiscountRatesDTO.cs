using ApiChallenge.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories {
    public class MerchantDiscountRatesDTO {
        private IEnumerable<IGrouping<object, MerchantDiscountRates>> testeAgrupado;

        public string Adquirente { get; set; }
        public List<Tax> Taxas { get; set; }
        //public string Tipo { get; set; }
        //public string Bandeira { get; set; }
        //public decimal Taxa { get; set; }
        
    }
}
