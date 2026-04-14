using inercya.EntityLite;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Facturas.Data;
using FacturacionAPI.DTOs.Facturas;
using FacturacionAPI.DTOs.Lineas;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq.Expressions;
using FacturacionAPI.Extensions;

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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {

            int userId = User.GetUserId();

            var query = _dataService.FacturaRepository.Query(FacturaProjections.Basic);

            if (!User.IsInRole("admin")) //Como esta asignado desde el builder podemos usar IsInRole
            {
                query.Where(FacturaFields.CreadoPor, userId);
            }

            var facturas = await query.OrderBy(FacturaFields.Status)
                .ToListAsync();
            return Ok(facturas);
        }

        // GET api/<FacturasController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFacturaID(int id)
        {
            try
            {
                var factura = await _dataService.FacturaRepository.GetAsync(FacturaProjections.Basic, id);
                int userId = User.GetUserId();
                if (factura == null) {
                    return BadRequest("No se ha encontrado la factura");
                }
                if (!User.IsInRole("admin") && factura.CreadoPor != userId)
                {
                    return Forbid();
                }
                return Ok(factura);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized("No se pudo identificar al usuario.");
            }
        }


        // POST api/<FacturasController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Factura>> SaveNewFactura([FromBody] Factura nuevaFactura)
        {
            try
            {
                if (nuevaFactura == null)
                {
                    return BadRequest("Los datos de la factura son nulos.");
                }
                int userId = User.GetUserId();

                bool clientUserIdExists = await _dataService.UserClientsRepository
                    .Query(UserClientsProjections.BaseTable)
                    .Where(UserClientsFields.ClientId, nuevaFactura.ClientId)
                    .And(UserClientsFields.UserId, userId)
                    .AnyAsync();

                if (!clientUserIdExists && !User.IsInRole("admin"))
                {
                    return StatusCode(403, $"El usuario no tiene acceso a este cliente");
                }

                nuevaFactura.Creado = DateTime.UtcNow;
                nuevaFactura.Modificado = DateTime.UtcNow;

                nuevaFactura.CreadoPor = userId;
                nuevaFactura.ModificadoPor = userId;
                nuevaFactura.Status = InvoiceStatusExtension.statusToId(Enums.InvoiceStatusEn.EnCreacion);

                _dataService.FacturaRepository.Save(nuevaFactura);

                return Ok(nuevaFactura);
            }
            catch (UnauthorizedAccessException ex) {
                return Unauthorized("No se pudo identificar al usuario.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar la factura: {ex.Message}");
            }
        }

        // PUT api/<FacturasController>/5
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Factura>> UpdateFactura([FromBody] Factura facturaActualizada)
        {
            try
            {
                if (facturaActualizada == null)
                {
                    return BadRequest("Los datos de la factura son nulos.");
                }
                var facturaAntigua = await _dataService.FacturaRepository
                .GetAsync(FacturaProjections.BaseTable, facturaActualizada.IdFactura);

                if (facturaAntigua == null)
                {
                    return BadRequest("Los datos de la factura son nulos.");
                }

                int userId = User.GetUserId();
                var fechaActual = DateTime.UtcNow;
                facturaAntigua.Modificado = fechaActual;
                facturaAntigua.ModificadoPor = userId;
                facturaAntigua.FechaFactura = facturaActualizada.FechaFactura;
                facturaAntigua.TipoIva = facturaActualizada.TipoIva;
                facturaAntigua.Aseguradora = facturaActualizada.Aseguradora;
                facturaAntigua.NumeroFactura = facturaActualizada.NumeroFactura;

                await _dataService.FacturaRepository.SaveAsync(facturaAntigua);
                var facturaCalculada = await _dataService.FacturaRepository
                .GetAsync(FacturaProjections.Basic, facturaActualizada.IdFactura);

                return Ok(facturaCalculada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar la factura: {ex.Message}");
            }
        }

        // DELETE api/<FacturasController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteFactura(int id)
        {
            try
            {
                _dataService.FacturaRepository.EliminarLineasFacturaAsociadas(id);
                var eliminado = _dataService.FacturaRepository.Delete(id);

                if (!eliminado)
                {
                    return NotFound($"No se encontró la factura con id {id}");
                }

                return NoContent(); // 204 -> eliminación correcta
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la factura: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut("{id}/sendToValidate")]
        public async Task<ActionResult<Factura>> UpdateFacturaStatusToPendingAproval(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Los datos de la factura son nulos.");
                }
                var previousInvoice = await _dataService.FacturaRepository
                    .GetAsync(FacturaProjections.Basic, id);
                if (previousInvoice == null)
                {
                    return BadRequest("Los datos de la factura son nulos.");
                }

                int userId = User.GetUserId();
                if (!verifyUserClient(userId, previousInvoice.ClientId) && !User.IsInRole("admin"))
                {
                    return StatusCode(403, $"El usuario no tiene acceso a este cliente");
                }                
                previousInvoice.Status = InvoiceStatusExtension.statusToId(Enums.InvoiceStatusEn.PdteAprobacion);
                await _dataService.FacturaRepository.SaveAsync(previousInvoice);            
                return Ok(previousInvoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar la factura: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut("{id}/sendToCancelValidate")]
        public async Task<ActionResult<Factura>> UpdateFacturaStatusToOnCreateFromPending(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Los datos de la factura son nulos.");
                }

                var previousInvoice = await _dataService.FacturaRepository
                    .GetAsync(FacturaProjections.Basic, id);


                if (previousInvoice == null)
                {
                    return BadRequest("Los datos de la factura son nulos.");
                }

                int userId = User.GetUserId();
                if (!verifyUserClient(userId, previousInvoice.ClientId) && !User.IsInRole("admin"))
                {
                    return StatusCode(403, $"El usuario no tiene acceso a este cliente");
                }
                if (previousInvoice.Status != InvoiceStatusExtension.statusToId(Enums.InvoiceStatusEn.PdteAprobacion)) {
                    return StatusCode(400, $"La factura no tenia el estado previo correcto");
                }

                previousInvoice.Status = InvoiceStatusExtension.statusToId(Enums.InvoiceStatusEn.EnCreacion);
                await _dataService.FacturaRepository.SaveAsync(previousInvoice);
                return Ok(previousInvoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar la factura: {ex.Message}");
            }
        }
        [Authorize]
        [HttpPut("{id}/sendToApprove")]
        public async Task<ActionResult<Factura>> UpdateFacturaStatusToApproved(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Los datos de la factura son nulos.");
                }
                var previousInvoice = await _dataService.FacturaRepository
                    .GetAsync(FacturaProjections.Basic, id);

                int userId = User.GetUserId();
                if (!verifyUserClient(userId, previousInvoice.ClientId) && !User.IsInRole("admin"))
                {
                    return StatusCode(403, $"El usuario no tiene acceso a este cliente");
                }
                if (previousInvoice.Status != InvoiceStatusExtension.statusToId(Enums.InvoiceStatusEn.PdteAprobacion))
                {
                    return StatusCode(403, $"La factura no tenia el estado previo correcto");
                }
                previousInvoice.Status = InvoiceStatusExtension.statusToId(Enums.InvoiceStatusEn.AprobadaCerrada);
                await _dataService.FacturaRepository.SaveAsync(previousInvoice);
                return Ok(previousInvoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar la factura: {ex.Message}");
            }
        }
        private bool verifyUserClient(int userId, int clientId) {
            return _dataService.UserClientsRepository
                     .Query(UserClientsProjections.BaseTable)
                     .Where(UserClientsFields.ClientId, clientId)
                     .And(UserClientsFields.UserId, userId)
                     .Any();
        }
    

    }
}
/* [HttpPut("{id}/lineas")]
        public async Task<IActionResult> UpdateWholeFacturaAsync(int id, [FromBody] FacturaLineasUpdateDto facturaConLineas)
        {
            if (id != facturaConLineas.IdFactura) return BadRequest("El ID de la ruta no coincide con el de la factura.");
            if (facturaConLineas.Lineas == null) return BadRequest("La lista de líneas no puede ser nula.");

            // TODO: Obtener el ID del usuario real desde el token JWT (User.Claims)
            // Por ahora usamos el mismo usuarioFake de tu método POST
            int usuarioActualId = 1;
            var fechaActual = DateTime.UtcNow;

            _dataService.BeginTransaction();
            try
            {
                // 1. Obtener la factura de la Base de Datos
                var facturaEntity = await _dataService.FacturaRepository
                    .GetAsync(FacturaProjections.Basic, id)
                    .ConfigureAwait(false);

                if (facturaEntity == null) return NotFound($"No se encontró la factura con id {id}");

                // 2. Obtener las líneas actuales en DB ANTES de mapear
                var lineasActuales = await _dataService.LineaFacturaRepository
                    .Query(LineaFacturaProjections.Basic)
                    .Where(nameof(LineaFactura.IdFactura), OperatorLite.Equals, id)
                    .ToListAsync()
                    .ConfigureAwait(false);

                // 3. Actualizar la cabecera (Factura)
                facturaEntity.Importe = facturaConLineas.Lineas.Sum(l => l.Importe * l.Cantidad);

                // Solo tocamos los campos de modificación en la cabecera
                facturaEntity.Modificado = fechaActual;
                facturaEntity.ModificadoPor = usuarioActualId;

                await _dataService.FacturaRepository.SaveAsync(facturaEntity).ConfigureAwait(false);

                // 4. Mapear los DTOs preservando/asignando la auditoría
                var lineasActualesDict = lineasActuales.ToDictionary(l => l.IdLineaFactura);
                var lineasNuevasMapeadas = new List<LineaFactura>();

                foreach (var dto in facturaConLineas.Lineas)
                {
                    var idLinea = dto.IdLineaFactura ?? 0;

                    var nuevaLinea = new LineaFactura
                    {
                        IdLineaFactura = idLinea,
                        IdFactura = id,
                        IdMaterial = dto.IdMaterial,
                        Importe = dto.Importe,
                        Cantidad = dto.Cantidad,

                        // Estos siempre se actualizan
                        Modificado = fechaActual,
                        ModificadoPor = usuarioActualId
                    };

                    if (idLinea == 0)
                    {
                        // ES UNA LÍNEA NUEVA: Asignamos campos de creación
                        nuevaLinea.Creado = fechaActual;
                        nuevaLinea.CreadoPor = usuarioActualId;
                    }
                    else
                    {
                        // ES UNA LÍNEA EXISTENTE: Rescatamos la fecha de creación de la base de datos
                        if (lineasActualesDict.TryGetValue(idLinea, out var lineaOriginal))
                        {
                            nuevaLinea.Creado = lineaOriginal.Creado;
                            nuevaLinea.CreadoPor = lineaOriginal.CreadoPor;
                        }
                    }

                    lineasNuevasMapeadas.Add(nuevaLinea);
                }

                // 5. Sincronizamos las líneas con EntityLite
                await _dataService.LineaFacturaRepository
                    .SaveAsync(lineasNuevasMapeadas, lineasActuales)
                    .ConfigureAwait(false);

                // 6. Confirmar transacción
                _dataService.Commit();

                var response = new FacturaUpdateResponseDto
                {
                    Factura = facturaEntity,
                    Lineas = lineasNuevasMapeadas // Estas ya tienen los IDs reales y auditoría
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                if (_dataService.IsActiveTransaction)
                {
                    _dataService.Rollback();
                }
                return StatusCode(500, $"Error al actualizar en bloque: {ex.Message}");
            }
        }

        */
