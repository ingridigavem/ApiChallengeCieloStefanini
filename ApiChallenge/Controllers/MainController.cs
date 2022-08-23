using ApiChallenge.Repositories.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiChallenge.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase {

        private readonly IConfiguration _configuration;
        private readonly IMainRepository _repository;
        public MainController(IConfiguration configuration, IMainRepository repository) {
            _configuration = configuration;
            _repository = repository;
        }
        // GET: api/mdr>
        [HttpGet("/mdr")]
        public IEnumerable<object> Get() {
            return _repository.GetAllMerchantDiscountRates(_configuration.GetConnectionString("Default"));
        }


        // POST api/transaction>
        [HttpPost("/transaction")]
        public IActionResult Post([FromBody] Transaction transaction) {

            string sql = @$"SELECT taxa FROM table_taxes
                            WHERE bandeira = '{transaction.Bandeira}'
                                AND adquirente = '{transaction.Adquirente}'
                                AND tipo = '{transaction.Tipo}' ";

            decimal taxa;
            decimal valorLiquido;

            try {
                taxa = _repository.FindTax(sql, _configuration.GetConnectionString("Default"));
                valorLiquido = (transaction.Valor - taxa);
            } catch (Exception ex) {
                return BadRequest(ex);
                throw;
            }

            return Ok(new PostResult() { ValorLiquido = valorLiquido});
        }

        //private void FindMerchantDiscountRatesMocks(Transaction transaction) {
        //    //var mdr = GetMerchantDiscountRatesMocks();

        //    //string adquirente;
        //    //List<MerchantDiscountRates> adquirentes;
        //    //foreach (var item in mdr) {
        //    //    adquirente = item.Adquirente.Contains("A") ? "A" : (item.Adquirente.Contains("B") ? "B" : "C");

        //    //}
             
        //}

        //private List<MerchantDiscountRates> GetMerchantDiscountRatesMocks() {
        //    var adA =  new MerchantDiscountRates() {
        //        Adquirente = "Adquirente A",
        //        Taxas = new List<Tax>() {
        //            new Tax() {
        //                Bandeira = "Visa",
        //                Credito = 2.25M,
        //                Debito = 2.00M
        //            },
        //             new Tax() {
        //                Bandeira = "Master",
        //                Credito = 2.35M,
        //                Debito = 1.98M
        //            },

        //        }

        //    };

        //    var adB = new MerchantDiscountRates() {
        //        Adquirente = "Adquirente B",
        //        Taxas = new List<Tax>() {
        //            new Tax() {
        //                Bandeira = "Visa",
        //                Credito= 2.50M,
        //                Debito = 2.0M
        //            },
        //            new Tax() {
        //                Bandeira = "Master",
        //                Credito = 2.65M,
        //                Debito = 1.75M
        //            },
        //        }

        //    };
        //    var adC = new MerchantDiscountRates() {
        //        Adquirente = "Adquirente C",
        //        Taxas = new List<Tax>() {
        //            new Tax() {
        //                Bandeira = "Visa",
        //                Credito= 2.75M,
        //                Debito = 2.16M
        //            },
        //            new Tax() {
        //                Bandeira = "Master",
        //                Credito = 3.10M,
        //                Debito = 1.58M
        //            },
        //        }

        //    };
        //    return new List<MerchantDiscountRates>() { adA, adB, adC };
        //}
    }
}
