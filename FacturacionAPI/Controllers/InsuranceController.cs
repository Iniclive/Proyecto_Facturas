using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private readonly FacturacionDataService _dataService;

        public InsuranceController(FacturacionDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: api/<InsuranceController>
        [HttpGet]
        public ActionResult<IEnumerable<Insurance>> Get()
        {
            var insurances = _dataService.InsuranceRepository.Query(InsuranceProjections.BaseTable).ToList();
            return Ok(insurances);
        }

        // GET api/<InsuranceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InsuranceController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<InsuranceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InsuranceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
