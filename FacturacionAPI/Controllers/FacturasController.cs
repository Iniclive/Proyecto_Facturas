using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly FacturacionDataService _dataService;

        // El sistema inyecta automáticamente el servicio aquí gracias al registro en Program.cs
        public FacturasController(FacturacionDataService dataService)
        {
            _dataService = dataService;
        }


        // GET: api/<FacturasController>
        [HttpGet]
        public ActionResult<IEnumerable<Factura>> Get()
        {
            var facturas = _dataService.FacturaRepository.Query(FacturaProjections.BaseTable).ToList();
            return Ok(facturas);
        }

        // GET api/<FacturasController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var facturas = await _dataService.FacturaRepository.GetAsync(FacturaProjections.Basic, id);
            return Ok(facturas);
        }

        // POST api/<FacturasController>
        [HttpPost]
        public ActionResult<Factura> Post([FromBody] Factura nuevaFactura)
        {
            try
            {
                if (nuevaFactura == null)
                {
                    return BadRequest("Los datos de la factura son nulos.");
                }
                int usuarioFake = 1;

                nuevaFactura.Creado = DateTime.UtcNow;
                nuevaFactura.Modificado = DateTime.UtcNow;

                nuevaFactura.CreadoPor = usuarioFake;
                nuevaFactura.ModificadoPor = usuarioFake;

                _dataService.FacturaRepository.Save(nuevaFactura);

                return Ok(nuevaFactura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar la factura: {ex.Message}");
            }
        }

        // PUT api/<FacturasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FacturasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
