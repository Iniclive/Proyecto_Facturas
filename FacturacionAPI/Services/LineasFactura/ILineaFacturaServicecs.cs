using FacturacionAPI.Application.LineasFactura.Dtos;
using FacturacionAPI.DTOs.Facturas;
using FacturacionAPI.Shared.Results;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Application.LineasFactura
{
    public interface ILineaFacturaService
    {
        Task<Result<List<LineaFactura>>> GetByFacturaAsync(int idFactura);
        Task<Result<LineaFacturaResponseDto>> CreateAsync(LineaFactura nuevaLinea);
        Task<Result<LineaFacturaResponseDto>> UpdateAsync(LineaFactura updatedLinea);
        Task<Result<FacturaResumenDto>> DeleteAsync(int id);
    }
}