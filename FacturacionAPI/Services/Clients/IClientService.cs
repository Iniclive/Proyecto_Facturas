using FacturacionAPI.Shared.Results;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Application.Clients
{
    public interface IClientService
    {
        Task<Result<List<Client>>> GetAllAsync();
        Task<Result<Client>> GetByIdAsync(int id);
        Task<Result<Client>> CreateAsync(Client newClient);
        Task<Result<Client>> UpdateAsync(Client updatedClient);
        Task<Result> DeleteAsync(int id);
    }
}