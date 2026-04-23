using FacturacionAPI.Application.Auth;
using FacturacionAPI.Application.Auth.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        return FromResult(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        if (!result.IsSuccess) return FromResult(result);

        var login = result.Value!;

        // Cookie HTTP-only: responsabilidad del controller
        SetJwtCookie(login.Token, login.ExpiresAt);

        // Devolvemos datos públicos del usuario, SIN el token
        return Ok(new
        {
            id = login.UserId.ToString(),
            name = login.Name,
            email = login.Email,
            role = login.Role
        });
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult GetMe()
    {
        return Ok(new UserInfoDto
        {
            Id = User.FindFirst("sub")?.Value,
            Name = User.FindFirst("name")?.Value,
            Email = User.FindFirst("email")?.Value,
            Role = User.FindFirst("role")?.Value
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        if (!Request.Cookies.ContainsKey("jwt_token"))
            return BadRequest("No hay sesión activa.");

        ClearJwtCookie();
        return Ok(new { message = "Sesión cerrada correctamente." });
    }

    // ─── Helpers privados de cookies ──────────────────────────────────
    private void SetJwtCookie(string token, DateTime expiresAt)
    {
        Response.Cookies.Append("jwt_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,                 // TODO: true en producción
            SameSite = SameSiteMode.Lax,
            Expires = expiresAt,
            Path = "/"
        });
    }

    private void ClearJwtCookie()
    {
        Response.Cookies.Append("jwt_token", "", new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Lax,
            Expires = DateTime.UtcNow.AddDays(-1),
            Path = "/"
        });
    }
}