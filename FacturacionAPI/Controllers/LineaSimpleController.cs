using inercya.EntityLite;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineaSimpleController : ControllerBase
    {

        private readonly FacturacionDataService _dataService;

        public LineaSimpleController(FacturacionDataService dataService)
        {
            _dataService = dataService;
        }
        // GET: api/<LineaSimple>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int idFactura)
        {
            var lineas = await _dataService.LineaSimpleRepository
                .Query(LineaSimpleProjections.BaseTable)
                .Where(LineaSimpleFields.IdFactura, idFactura)
                .ToListAsync();
            return Ok(lineas);
        }

        // GET api/<LineaSimple>/5
        [HttpGet("{id}")]
        public string Get()
        {
            return "value";
        }

        // POST api/<LineaSimple>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LineaSimple>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LineaSimple>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
