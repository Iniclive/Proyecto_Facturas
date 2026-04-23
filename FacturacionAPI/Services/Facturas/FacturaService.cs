namespace FacturacionAPI.Application.Facturas
{
    using FacturacionAPI.Application.Facturas.Dtos;
    using FacturacionAPI.Domain.Enums;
    using FacturacionAPI.Domain.Extensions;
    using FacturacionAPI.Shared.Abstractions;
    using FacturacionAPI.Shared.Results;
    using inercya.EntityLite;
    using Proyecto_Facturas.Data;


    public class FacturaService : IFacturaService
    {
        private readonly FacturaRepository _facturaRepository;
        private readonly UserClientsRepository _userClientsRepository;
        private readonly ICurrentUserService _currentUser;

        public FacturaService(
            FacturacionDataService dataservice,
            ICurrentUserService currentUser)
        {
            _facturaRepository = dataservice.FacturaRepository;
            _userClientsRepository = dataservice.UserClientsRepository;
            _currentUser = currentUser;
        }

        // ─── Lecturas ──────────────────────────────────────────────────────

        public async Task<Result<List<Factura>>> GetAllAsync()
        {
            var query = _facturaRepository.Query(FacturaProjections.Basic);

            if (!_currentUser.IsAdmin)
                query.Where(FacturaFields.CreadoPor, _currentUser.UserId);

            var facturas = await query
                .OrderBy(FacturaFields.Status)
                .ToListAsync();

            return Result<List<Factura>>.Success(facturas);
        }

        public async Task<Result<Factura>> GetByIdAsync(int id)
        {
            var result = await GetInvoiceOrFailureAsync(id);
            if (!result.IsSuccess) return result;

            var access = await CheckAccessAsync(result.Value!);
            if (!access.IsSuccess) return access;

            return result;
        }

        // ─── Escrituras ────────────────────────────────────────────────────

        public async Task<Result<Factura>> CreateAsync(Factura newInvoice)
        {
            if (newInvoice == null)
                return Failure<Factura>("InvoiceBadRequest", "Los datos de la factura son nulos.", ErrorType.BadRequest);

            var clientAccess = await CheckClientAccessAsync(newInvoice.ClientId);
            if (!clientAccess.IsSuccess) return clientAccess;

            var now = DateTime.UtcNow;
            newInvoice.Creado = now;
            newInvoice.Modificado = now;
            newInvoice.CreadoPor = _currentUser.UserId;
            newInvoice.ModificadoPor = _currentUser.UserId;
            newInvoice.Status = InvoiceStatusEn.EnCreacion.statusToId();

            await _facturaRepository.SaveAsync(newInvoice);

            return Result<Factura>.Success(newInvoice);
        }

        public async Task<Result<Factura>> UpdateAsync(Factura updatedInvoice)
        {
            if (updatedInvoice == null)
                return Failure<Factura>("InvoiceBadRequest", "Los datos de la factura son nulos.", ErrorType.BadRequest);

         
            var existingResult = await GetInvoiceOrFailureAsync(updatedInvoice.IdFactura);
            if (!existingResult.IsSuccess) return existingResult;

           
            var accessToExisting = await CheckAccessAsync(existingResult.Value!);
            if (!accessToExisting.IsSuccess) return accessToExisting;

       
            var accessToNewClient = await CheckClientAccessAsync(updatedInvoice.ClientId);
            if (!accessToNewClient.IsSuccess) return accessToNewClient;

            updatedInvoice.Modificado = DateTime.UtcNow;
            updatedInvoice.ModificadoPor = _currentUser.UserId;

            var fieldsToUpdate = new[]
            {
            FacturaFields.NumeroFactura,
            FacturaFields.Modificado,
            FacturaFields.ModificadoPor,
            FacturaFields.FechaFactura,
            FacturaFields.TipoIva,
            FacturaFields.Aseguradora,
            FacturaFields.ClientId
        };

            await _facturaRepository.UpdateAsync(updatedInvoice, fieldsToUpdate);

            var refreshed = await _facturaRepository
                .GetAsync(FacturaProjections.Basic, updatedInvoice.IdFactura);

            return Result<Factura>.Success(refreshed!);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var result = await GetInvoiceOrFailureAsync(id);
            if (!result.IsSuccess) return Result.Failure(result.Error!);

            var access = await CheckAccessAsync(result.Value!);
            if (!access.IsSuccess) return access;

            _facturaRepository.EliminarLineasFacturaAsociadas(id);
            return Result.Success();
        }

        // ─── Transiciones de estado ────────────────────────────────────────

        public Task<Result<Factura>> SendToValidateAsync(StatusTransitionRequest request)
            => ChangeStatusAsync(request,
        expectedCurrent: InvoiceStatusEn.EnCreacion,
        newStatus: InvoiceStatusEn.PdteAprobacion,
        errorMessage: "La factura debe estar en estado 'En creación' para enviarse a validar.");

        public Task<Result<Factura>> CancelValidationAsync(StatusTransitionRequest request)
            => ChangeStatusAsync(request,
                expectedCurrent: InvoiceStatusEn.PdteAprobacion,
                newStatus: InvoiceStatusEn.EnCreacion,
                errorMessage: "La factura debe estar pendiente de aprobación para cancelar la validación."
                );

        public Task<Result<Factura>> ApproveAsync(StatusTransitionRequest request)
            => ChangeStatusAsync(request,
                expectedCurrent: InvoiceStatusEn.PdteAprobacion,
                newStatus: InvoiceStatusEn.AprobadaCerrada,
                errorMessage: "La factura debe estar pendiente de aprobación para aprobarla."
                );

        // ─── Helpers privados ──────────────────────────────────────────────

        private async Task<Result<Factura>> ChangeStatusAsync(
        StatusTransitionRequest request,
        InvoiceStatusEn expectedCurrent,
        InvoiceStatusEn newStatus,
        string errorMessage)
        {
            if (request == null)
                return Failure<Factura>("InvoiceBadRequest", "Datos de transición inválidos.", ErrorType.BadRequest);

            // 1. Cargar la factura de BD (fuente de verdad)
            var dbResult = await GetInvoiceOrFailureAsync(request.IdFactura);
            if (!dbResult.IsSuccess) return dbResult;

            var invoice = dbResult.Value!;

            // 2. Validaciones sobre datos reales
            var access = await CheckAccessAsync(invoice);
            if (!access.IsSuccess) return access;

            if (invoice.Status != expectedCurrent.statusToId())
                return Failure<Factura>("InvalidStatusTransition", errorMessage, ErrorType.Conflict);

           
            invoice.EntityRowVersion = request.EntityRowVersion;
            invoice.Status = newStatus.statusToId();
            invoice.Modificado = DateTime.UtcNow;
            invoice.ModificadoPor = _currentUser.UserId;
            
            await _facturaRepository.UpdateAsync(invoice,
                    FacturaFields.Status,
                    FacturaFields.Modificado,
                    FacturaFields.ModificadoPor);
            
            return Result<Factura>.Success(invoice!);
        }

        private async Task<Result<Factura>> GetInvoiceOrFailureAsync(int id)
        {
            if (id <= 0)
                return Failure<Factura>("InvoiceBadRequest", "El id de la factura no es válido.", ErrorType.BadRequest);

            var invoice = await _facturaRepository.GetAsync(FacturaProjections.Basic, id);
            if (invoice == null)
                return Failure<Factura>("InvoiceNotFound", $"Factura con id '{id}' no encontrada.", ErrorType.NotFound);

            return Result<Factura>.Success(invoice);
        }

        private async Task<Result> CheckAccessAsync(Factura invoice)
        {
            if (_currentUser.IsAdmin) return Result.Success();

            // El creador siempre tiene acceso
            if (invoice.CreadoPor == _currentUser.UserId) return Result.Success();

            // Si no es creador, debe tener acceso al cliente de la factura
            return await CheckClientAccessAsync(invoice.ClientId);
        }

        private async Task<Result> CheckClientAccessAsync(int clientId)
        {
            if (_currentUser.IsAdmin) return Result.Success();

            var hasAccess = await _userClientsRepository
                .Query(UserClientsProjections.BaseTable)
                .Where(UserClientsFields.ClientId, clientId)
                .And(UserClientsFields.UserId, _currentUser.UserId)
                .AnyAsync();

            return hasAccess
                ? Result.Success()
                : Result.Failure(new Error("Forbidden", "El usuario no tiene acceso a este cliente.", ErrorType.Forbidden));
        }

        // Factoría de errores para reducir verbosidad
        private static Result<T> Failure<T>(string code, string message, ErrorType type)
            => Result<T>.Failure(new Error(code, message, type));
    }
}
