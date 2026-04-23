using FacturacionAPI.Shared.Results;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Application.Insurances
{
    public interface IInsuranceService
    {
        Task<Result<List<Insurance>>> GetAllAsync();
        Task<Result<List<Insurance>>> GetBySearchStringAsync(string searchString);
    }
}