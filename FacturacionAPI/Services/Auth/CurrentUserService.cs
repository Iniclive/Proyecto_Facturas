using System.Security.Claims;
using FacturacionAPI.Shared.Abstractions;

namespace FacturacionAPI.Services.Auth
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId => GetUserId();
        public bool IsAdmin => _httpContextAccessor.HttpContext!.User.IsInRole("admin");

        private int GetUserId()
        {
            var claim = _httpContextAccessor.HttpContext!.User.FindFirst("sub")?.Value;
            if (int.TryParse(claim, out var userId))
                return userId;

            throw new UnauthorizedAccessException("El token no contiene un ID de usuario válido.");
        }
    }
}
