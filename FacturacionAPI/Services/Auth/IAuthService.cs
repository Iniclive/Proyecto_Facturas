using FacturacionAPI.Application.Auth.Dtos;
using FacturacionAPI.Shared.Results;

namespace FacturacionAPI.Application.Auth
{
    public interface IAuthService
    {
        Task<Result<UserRegisteredDto>> RegisterAsync(RegisterUserDto dto);
        Task<Result<LoginResultDto>> LoginAsync(LoginUserDto dto);
    }
}
