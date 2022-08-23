using ApiChallenge.Repositories.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;


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

    }
}
