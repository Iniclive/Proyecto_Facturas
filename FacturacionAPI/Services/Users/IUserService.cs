using FacturacionAPI.Application.Auth.Dtos;
using FacturacionAPI.Services.Users.Dtos;
using FacturacionAPI.Shared.Results;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Application.Users
{
    public interface IUserService
    {
        Task<Result<List<User>>> GetAllAsync();
        Task<Result<User>> CreateAsync(RegisterUserDto userReg);
        Task<Result<User>> UpdateAsync(User updatedUser);

        Task<Result> UpdatePasswordAsync(UpdatePasswordRequestDto request);
        Task<Result> UpdateEmailAsync(UpdateEmailDto request);
        Task<Result> UpdateNameAsync(UpdateNameDto request);
        Task<Result> DeleteAsync(int id);
    }
}