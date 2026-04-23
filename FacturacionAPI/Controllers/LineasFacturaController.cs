using FacturacionAPI.Application.LineasFactura;
using FacturacionAPI.DTOs.Facturas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LineasFacturaController : BaseController
    {
        private readonly ILineaFacturaService _lineaFacturaService;

        public LineasFacturaController(ILineaFacturaService lineaFacturaService)
        {
            _lineaFacturaService = lineaFacturaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int idFactura)
            => FromResult(await _lineaFacturaService.GetByFacturaAsync(idFactura));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LineaFactura nuevaLinea)
            => FromResult(await _lineaFacturaService.CreateAsync(nuevaLinea));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] LineaFactura updatedLinea)
            => FromResult(await _lineaFacturaService.UpdateAsync(updatedLinea));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => FromResult(await _lineaFacturaService.DeleteAsync(id));
    }
}