using FacturacionAPI.Application.Facturas.Dtos;
using FacturacionAPI.Shared.Results;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Application.Facturas
{
    public interface IFacturaService
    {
        Task<Result<List<Factura>>> GetAllAsync();
        Task<Result<Factura>> GetByIdAsync(int id);
        Task<Result<Factura>> CreateAsync(Factura newInvoice);
        Task<Result<Factura>> UpdateAsync(Factura updatedInvoice);
        Task<Result> DeleteAsync(int id);

        Task<Result<Factura>> SendToValidateAsync(StatusTransitionRequest request);
        Task<Result<Factura>> CancelValidationAsync(StatusTransitionRequest request);
        Task<Result<Factura>> ApproveAsync(StatusTransitionRequest request);
    }
}
