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

    }
}
