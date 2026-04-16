using System.Security.Claims;
using FacturacionAPI.DTOs.Facturas;
using FacturacionAPI.DTOs.Lineas;
using FacturacionAPI.Extensions;
using inercya.EntityLite;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineasFacturaController : ControllerBase
    {
        private readonly FacturacionDataService _dataService;

        // El sistema inyecta automáticamente el servicio aquí gracias al registro en Program.cs
        public LineasFacturaController(FacturacionDataService dataService)
        {
            _dataService = dataService;
        }
        // GET: api/<LineasFacturaController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int idFactura)
        {
            var lineas = await _dataService.LineaFacturaRepository
                .Query(LineaFacturaProjections.Basic)
                .Where(LineaFacturaFields.IdFactura, idFactura)
                .ToListAsync();
            return Ok(lineas);
        }

        // POST api/<LineasFacturaController>
        [HttpPost]
        public async Task<ActionResult<LineaFacturaResponseDto>> GuardarNuevaLinea([FromBody] LineaFactura nuevaLinea)
        {
            _dataService.BeginTransaction();
            try
            {
                if (nuevaLinea == null)
                {
                    return BadRequest("Los datos de la linea son nulos.");
                }
                int userId = User.GetUserId();

                if (!validateInvoiceUser(nuevaLinea.IdFactura, userId))
                {
                    return StatusCode(403, $"El usuario no tiene acceso a esa factura");
                }

                var fechaActual = DateTime.UtcNow;
                nuevaLinea.Creado = fechaActual;
                nuevaLinea.Modificado = fechaActual;
                nuevaLinea.CreadoPor = userId;
                nuevaLinea.ModificadoPor = userId;

                await _dataService.LineaFacturaRepository.SaveAsync(nuevaLinea);
                var facturaResumen = await ActualizarFacturaAsync(nuevaLinea.IdFactura, userId, fechaActual);
                var lineaCompleta = await _dataService.LineaFacturaRepository
                .GetAsync(LineaFacturaProjections.BaseTable, nuevaLinea.IdLineaFactura);

                if (_dataService.IsActiveTransaction)
                    _dataService.Commit();

                return Ok(new LineaFacturaResponseDto
                {
                    Linea = lineaCompleta,
                    Factura = facturaResumen,
                });
            }
            catch (InvalidOperationException ex)
            {
                if (this._dataService.IsActiveTransaction) this._dataService.Rollback();
                return StatusCode(415, $"La factura se ha modificado en otra sesion, es necesario recargar: {ex.Message}");
            }
            catch (Exception ex)
            {
                if (this._dataService.IsActiveTransaction) this._dataService.Rollback();
                return StatusCode(500, $"Error al guardar la factura: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<LineaFacturaResponseDto>> ActualizarLinea([FromBody] LineaFactura nuevaLinea)
        {
            _dataService.BeginTransaction();
            try
            {
                if (nuevaLinea == null)
                {
                    return BadRequest("Los datos de la linea son nulos.");
                }
                int userId = User.GetUserId();

                if (!validateInvoiceUser(nuevaLinea.IdFactura, userId))
                {
                    return StatusCode(403, $"El usuario no tiene acceso a esa factura");
                }

                var lineaOriginal = await _dataService.LineaFacturaRepository
                .GetAsync(LineaFacturaProjections.BaseTable, nuevaLinea.IdLineaFactura);

                var fechaActual = DateTime.UtcNow;
                lineaOriginal.Modificado = fechaActual;
                lineaOriginal.ModificadoPor = userId;
                lineaOriginal.Cantidad = nuevaLinea.Cantidad;
                lineaOriginal.Importe = nuevaLinea.Importe;
                lineaOriginal.ProductId = nuevaLinea.ProductId;

                _dataService.LineaFacturaRepository.Save(lineaOriginal);
                var facturaResumen = await ActualizarFacturaAsync(lineaOriginal.IdFactura, userId, fechaActual);

                if (_dataService.IsActiveTransaction)
                    _dataService.Commit();

                return Ok(new LineaFacturaResponseDto
                {
                    Linea = lineaOriginal,
                    Factura = facturaResumen,
                });
            }
            catch (InvalidOperationException ex)
            {
                if (this._dataService.IsActiveTransaction) this._dataService.Rollback();
                return StatusCode(415, $"La factura se ha modificado en otra sesion, es necesario recargar: {ex.Message}");
            }
            catch (Exception ex)
            {
                if (this._dataService.IsActiveTransaction) this._dataService.Rollback();
                return StatusCode(500, $"Error al guardar la factura: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<FacturaResumenDto>> DeleteLinea(int id)
        {
            _dataService.BeginTransaction();
            try
            {
                int userId = User.GetUserId();
                var fechaActual = DateTime.UtcNow;
                var lineaActual = await _dataService.LineaFacturaRepository.GetAsync(LineaFacturaProjections.BaseTable, id);
                var idFactura = lineaActual.IdFactura;

                if (!validateInvoiceUser(idFactura, userId))
                {
                    return StatusCode(403, $"El usuario no tiene acceso a esa factura");
                }

                await _dataService.LineaFacturaRepository.DeleteAsync(id);
                var facturaResumen = await ActualizarFacturaAsync(idFactura, userId, fechaActual);
                if (_dataService.IsActiveTransaction)
                    _dataService.Commit();
                return Ok(facturaResumen);
            }
            catch (InvalidOperationException ex)
            {
                if (this._dataService.IsActiveTransaction) this._dataService.Rollback();
                return StatusCode(415, $"La factura se ha modificado en otra sesion, es necesario recargar: {ex.Message}");
            }
            catch (Exception ex)
            {
                if (this._dataService.IsActiveTransaction) this._dataService.Rollback();
                return StatusCode(500, $"Error al guardar la factura: {ex.Message}");
            }
        }

        private async Task<FacturaResumenDto?> ActualizarFacturaAsync(int idFactura, int usuarioId, DateTime fecha)
        {
            var factura = await _dataService.FacturaRepository
                .GetAsync(FacturaProjections.BaseTable, idFactura);

            if (factura == null) return null;

            if (!validateInvoiceStatus(factura)) {
                throw new InvalidOperationException();
            }

            var lineas = await _dataService.LineaFacturaRepository
                .Query(LineaFacturaProjections.BaseTable)
                .Where(LineaFacturaFields.IdFactura, idFactura)
                .ToListAsync();

            factura.Importe = lineas.Sum(l => l.Importe * l.Cantidad);
            factura.Modificado = fecha;
            factura.ModificadoPor = usuarioId;

            await _dataService.FacturaRepository.SaveAsync(factura);

            var facturaActualizada = await _dataService.FacturaRepository
            .GetAsync(FacturaProjections.BaseTable, idFactura);


            return new FacturaResumenDto
            {
                Importe = facturaActualizada.Importe,
                ImporteIva = facturaActualizada.ImporteIva,
                ImporteTotal = facturaActualizada.ImporteTotal,
                Modificado = facturaActualizada.Modificado,
                ModificadoPor = facturaActualizada.ModificadoPor

            };
        }

        private Boolean validateInvoiceUser(int facturaId, int userId)
        {

            return _dataService.FacturaRepository
                      .Query(FacturaProjections.BaseTable)
                      .Where(FacturaFields.IdFactura, facturaId)
                      .And(FacturaFields.CreadoPor, userId)
                      .Any();

        }

        private Boolean validateInvoiceStatus(Factura factura)
        {
            if (factura.Status != InvoiceStatusExtension.statusToId(Enums.InvoiceStatusEn.EnCreacion))
            {
                return false;
            }
            return true;
        }
    }
}
