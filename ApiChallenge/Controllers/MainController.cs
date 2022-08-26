using ApiChallenge.Domain.Entities;
using ApiChallenge.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace ApiChallenge.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase {

        private readonly IMainService _mainService;
        public MainController(IMainService mainService) {

            _mainService = mainService;
        }
        // GET: api/mdr>
        [HttpGet("/mdr")]
        [SwaggerOperation("GetMdr")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]

        public IActionResult GetMdr() {

            var search = _mainService.GetAllMerchantDiscountRates();

            if (search == null) 
                throw new Exception("Um problema ocorreu durante a busca");
            if (search.Count() == 0) 
                return NoContent();
            
            return Ok(search);
        }

        // POST api/transaction>
        [HttpPost("/transaction")]
        [SwaggerOperation("PostTransaction")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public IActionResult PostTransaction([FromBody] Transaction transaction) {

            if (!ModelState.IsValid)
                return BadRequest();    

            return Ok(_mainService.CalculateTax(transaction));
        }

    }
}
