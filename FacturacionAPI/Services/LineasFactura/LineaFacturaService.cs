using FacturacionAPI.Application.LineasFactura.Dtos;
using FacturacionAPI.Domain.Enums;
using FacturacionAPI.Domain.Extensions;
using FacturacionAPI.DTOs.Facturas;
using FacturacionAPI.Shared.Abstractions;
using FacturacionAPI.Shared.Results;
using inercya.EntityLite;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Application.LineasFactura
{
    public class LineaFacturaService : ILineaFacturaService
    {
        private readonly FacturacionDataService _dataService;
        private readonly ICurrentUserService _currentUser;

        public LineaFacturaService(
            FacturacionDataService dataService,
            ICurrentUserService currentUser)
        {
            _dataService = dataService;
            _currentUser = currentUser;
        }


        public async Task<Result<List<LineaFactura>>> GetByFacturaAsync(int idFactura)
        {
            var lineas = await _dataService.LineaFacturaRepository
                .Query(LineaFacturaProjections.Basic)
                .Where(LineaFacturaFields.IdFactura, idFactura)
                .ToListAsync();

            return Result<List<LineaFactura>>.Success(lineas);
        }


        public async Task<Result<LineaFacturaResponseDto>> CreateAsync(LineaFactura nuevaLinea)
        {
            if (nuevaLinea == null)
                return Failure<LineaFacturaResponseDto>("LineaBadRequest", "Los datos de la linea son nulos.", ErrorType.BadRequest);

            var accessResult = await CheckInvoiceAccessAsync(nuevaLinea.IdFactura);
            if (!accessResult.IsSuccess) return accessResult;

            _dataService.BeginTransaction();
            try
            {
                var fechaActual = DateTime.UtcNow;
                nuevaLinea.Creado = fechaActual;
                nuevaLinea.Modificado = fechaActual;
                nuevaLinea.CreadoPor = _currentUser.UserId;
                nuevaLinea.ModificadoPor = _currentUser.UserId;

                await _dataService.LineaFacturaRepository.SaveAsync(nuevaLinea);

                var facturaResumen = await ActualizarFacturaAsync(nuevaLinea.IdFactura, _currentUser.UserId, fechaActual);
                if (facturaResumen == null)
                    return Failure<LineaFacturaResponseDto>("FacturaNotFound", "No se encontró la factura asociada.", ErrorType.NotFound);

                var lineaCompleta = await _dataService.LineaFacturaRepository
                    .GetAsync(LineaFacturaProjections.BaseTable, nuevaLinea.IdLineaFactura);

                if (_dataService.IsActiveTransaction) _dataService.Commit();

                return Result<LineaFacturaResponseDto>.Success(new LineaFacturaResponseDto
                {
                    Linea = lineaCompleta,
                    Factura = facturaResumen,
                });
            }
            catch (InvalidOperationException)
            {
                if (_dataService.IsActiveTransaction) _dataService.Rollback();
                return Failure<LineaFacturaResponseDto>("ConcurrencyConflict", "La factura se ha modificado en otra sesión, es necesario recargar.", ErrorType.Conflict);
            }
            catch
            {
                if (_dataService.IsActiveTransaction) _dataService.Rollback();
                throw;
            }
        }

        public async Task<Result<LineaFacturaResponseDto>> UpdateAsync(LineaFactura updatedLinea)
        {
            if (updatedLinea == null)
                return Failure<LineaFacturaResponseDto>("LineaBadRequest", "Los datos de la linea son nulos.", ErrorType.BadRequest);

            var accessResult = await CheckInvoiceAccessAsync(updatedLinea.IdFactura);
            if (!accessResult.IsSuccess) return accessResult;

            _dataService.BeginTransaction();
            try
            {
                var lineaOriginal = await _dataService.LineaFacturaRepository
                    .GetAsync(LineaFacturaProjections.BaseTable, updatedLinea.IdLineaFactura);

                if (lineaOriginal == null)
                    return Failure<LineaFacturaResponseDto>("LineaNotFound", $"Línea con id '{updatedLinea.IdLineaFactura}' no encontrada.", ErrorType.NotFound);

                var fechaActual = DateTime.UtcNow;
                lineaOriginal.Modificado = fechaActual;
                lineaOriginal.ModificadoPor = _currentUser.UserId;
                lineaOriginal.Cantidad = updatedLinea.Cantidad;
                lineaOriginal.Importe = updatedLinea.Importe;
                lineaOriginal.ProductId = updatedLinea.ProductId;

                _dataService.LineaFacturaRepository.Save(lineaOriginal);

                var facturaResumen = await ActualizarFacturaAsync(lineaOriginal.IdFactura, _currentUser.UserId, fechaActual);
                if (facturaResumen == null)
                    return Failure<LineaFacturaResponseDto>("FacturaNotFound", "No se encontró la factura asociada.", ErrorType.NotFound);

                if (_dataService.IsActiveTransaction) _dataService.Commit();

                return Result<LineaFacturaResponseDto>.Success(new LineaFacturaResponseDto
                {
                    Linea = lineaOriginal,
                    Factura = facturaResumen,
                });
            }
            catch (InvalidOperationException)
            {
                if (_dataService.IsActiveTransaction) _dataService.Rollback();
                return Failure<LineaFacturaResponseDto>("ConcurrencyConflict", "La factura se ha modificado en otra sesión, es necesario recargar.", ErrorType.Conflict);
            }
            catch
            {
                if (_dataService.IsActiveTransaction) _dataService.Rollback();
                throw;
            }
        }

        public async Task<Result<FacturaResumenDto>> DeleteAsync(int id)
        {
            if (id <= 0)
                return Failure<FacturaResumenDto>("LineaBadRequest", "El id de la línea no es válido.", ErrorType.BadRequest);

            var lineaActual = await _dataService.LineaFacturaRepository
                .GetAsync(LineaFacturaProjections.BaseTable, id);

            if (lineaActual == null)
                return Failure<FacturaResumenDto>("LineaNotFound", $"Línea con id '{id}' no encontrada.", ErrorType.NotFound);

            var accessResult = await CheckInvoiceAccessAsync(lineaActual.IdFactura);
            if (!accessResult.IsSuccess) return accessResult;

            _dataService.BeginTransaction();
            try
            {
                var fechaActual = DateTime.UtcNow;
                await _dataService.LineaFacturaRepository.DeleteAsync(id);

                var facturaResumen = await ActualizarFacturaAsync(lineaActual.IdFactura, _currentUser.UserId, fechaActual);
                if (facturaResumen == null)
                    return Failure<FacturaResumenDto>("FacturaNotFound", "No se encontró la factura asociada.", ErrorType.NotFound);

                if (_dataService.IsActiveTransaction) _dataService.Commit();

                return Result<FacturaResumenDto>.Success(facturaResumen);
            }
            catch (InvalidOperationException)
            {
                if (_dataService.IsActiveTransaction) _dataService.Rollback();
                return Failure<FacturaResumenDto>("ConcurrencyConflict", "La factura se ha modificado en otra sesión, es necesario recargar.", ErrorType.Conflict);
            }
            catch
            {
                if (_dataService.IsActiveTransaction) _dataService.Rollback();
                throw;
            }
        }



        private async Task<Result> CheckInvoiceAccessAsync(int facturaId)
        {
            if (_currentUser.IsAdmin) return Result.Success();

            var hasAccess = _dataService.FacturaRepository
                .Query(FacturaProjections.BaseTable)
                .Where(FacturaFields.IdFactura, facturaId)
                .And(FacturaFields.CreadoPor, _currentUser.UserId)
                .Any();

            return hasAccess
                ? Result.Success()
                : Result.Failure(new Error("Forbidden", "El usuario no tiene acceso a esa factura.", ErrorType.Forbidden));
        }

        private async Task<FacturaResumenDto?> ActualizarFacturaAsync(int idFactura, int usuarioId, DateTime fecha)
        {
            var factura = await _dataService.FacturaRepository
                .GetAsync(FacturaProjections.BaseTable, idFactura);

            if (factura == null) return null;

            if (factura.Status != InvoiceStatusEn.EnCreacion.statusToId())
                throw new InvalidOperationException();

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
                ModificadoPor = facturaActualizada.ModificadoPor,
                EntityRowVersion = facturaActualizada.EntityRowVersion
            };
        }

        private static Result<T> Failure<T>(string code, string message, ErrorType type)
            => Result<T>.Failure(new Error(code, message, type));
    }
}