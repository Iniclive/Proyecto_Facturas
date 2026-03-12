using inercya.EntityLite;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly FacturacionDataService _dataService;

        public MaterialController(FacturacionDataService dataService)
        {
            _dataService = dataService;
        }
        // GET: api/<MaterialController>
        [HttpGet]
        public ActionResult<IEnumerable<Insurance>> Get()
        {
            var insurances = _dataService.MaterialRepository.Query(MaterialProjections.BaseTable).ToList();
            return Ok(insurances);
        }

        // GET api/<MaterialController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MaterialController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MaterialController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MaterialController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
