using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FacturacionAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FacturaSimpleController : ControllerBase
    {

        private readonly FacturacionDataService _dataService;

        public FacturaSimpleController(FacturacionDataService dataService)
        {
            _dataService = dataService;
        }


        // GET: api/<FacturaSimpleController>
        [HttpGet]
        public ActionResult<IEnumerable<FacturaSimple>> Get()
        {
            var facturas = _dataService.FacturaSimpleRepository.Query(FacturaSimpleProjections.BaseTable).ToList();
            return Ok(facturas);
        }

        // GET api/<FacturaSimpleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FacturaSimpleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FacturaSimpleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FacturaSimpleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
