using FacturacionAPI.Application.DashboardSum.Dtos;
using FacturacionAPI.Shared.Results;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Application.Dashboard  
{
    public interface IDashboardService          
    {
        Task<Result<DashboardPayloadDto>> GetSummaryAsync();
    }
}