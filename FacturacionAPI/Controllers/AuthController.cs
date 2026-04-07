using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using inercya.EntityLite;
using Proyecto_Facturas.Data;
using FacturacionAPI.DTOs.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using FacturacionAPI.DTOs.Lineas;
using Microsoft.AspNetCore.Authorization;

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly FacturacionDataService _dataService;
        private readonly IConfiguration _configuration;
        public AuthController(FacturacionDataService dataService, IConfiguration configuration)
        {
            _dataService = dataService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisteredDto>> Register(RegisterUserDto userReg)
        {
            try
            {
                bool email = await _dataService.UserRepository
                    .Query(UserProjections.BaseTable)
                    .Where(UserFields.Email, userReg.Email)
                    .AnyAsync();
                if (email)
                {
                    return BadRequest("El email ya está registrado.");
                }

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(userReg.Password);
                var newUser = new User
                {
                    Name = userReg.Name,
                    Email = userReg.Email,
                    Password = passwordHash,
                    Role = "user"
                };

                await _dataService.UserRepository.SaveAsync(newUser);

                return Ok(new UserRegisteredDto
                {
                    Name = newUser.Name,
                    Email = newUser.Email,
                });
            }
            catch (Exception ex) {
                return StatusCode(500, $"Error al crear el usuario: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto usrLogin)
        {
     
            var user = await _dataService.UserRepository
            .Query(UserProjections.BaseTable)
             .Where(UserFields.Email, usrLogin.Email)
             .FirstOrDefaultAsync();
            if (user == null) return Unauthorized("Credenciales inválidas");

          
            if (!BCrypt.Net.BCrypt.Verify(usrLogin.Password, user.Password))
                return Unauthorized("Credenciales inválidas");

           
            var token = GenerateAccessToken(user);
            //var refreshToken = GenerateRefreshTokenAsync(user.IdUser);
            /*var cookieOptions = new CookieOptions //Queda pendiente de gestionar uso https en el front
            {
                HttpOnly = true,   // JavaScript no puede leerla
                Secure = true,     // Solo viaja por HTTPS
                SameSite = SameSiteMode.None, // Evita que se envíe desde otros sitios (protección CSRF)
                Expires = DateTime.UtcNow.AddDays(1)
            };*/
            
            //var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(1),
                Path = "/"
            };

            Response.Cookies.Append("jwt_token", token, cookieOptions);

            return Ok(new
            {
                id = user.IdUser.ToString(),
                name = user.Name,
                email = user.Email,
                role = user.Role
            });
            //return Ok(new { AccessToken = token });
            /*return Ok(new { AccessToken = token,
                            RefreshToken = refreshToken
                            });*/
        }

        private string GenerateAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("sub",   user.IdUser.ToString()),
                new Claim("name",  user.Name),
                new Claim("email", user.Email),
                new Claim("role",  user.Role),
                //new Claim("jti",   Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<RefreshToken> GenerateRefreshTokenAsync(int idUser)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid(), 
                UserId = idUser,
                ExpireAt = DateTime.UtcNow.AddDays(7), 
                Revoked = false
            };
            await _dataService.RefreshTokenRepository.SaveAsync(refreshToken);

            return refreshToken;
        }

        [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto requestToken)
    {
    try
    {
        
        var validRefreshToken = await ValidateRefreshTokenAsync(requestToken.RefreshToken);
        if (validRefreshToken == null) 
            return Unauthorized("Refresh token inválido, expirado o revocado. Vuelve a iniciar sesión.");

        // 2. Obtener el usuario asociado a ese token
        var user = await _dataService.UserRepository
            .Query(UserProjections.BaseTable)
            .Where(UserFields.IdUser, validRefreshToken.UserId)
            .FirstOrDefaultAsync();

        if (user == null) 
            return Unauthorized("Usuario no encontrado.");

        // 3. (Opcional pero recomendado): Rotación de Refresh Tokens
        // Revocamos el token actual y creamos uno nuevo por seguridad
        validRefreshToken.Revoked = true;
        await _dataService.RefreshTokenRepository.SaveAsync(validRefreshToken);
        var newRefreshToken = await GenerateRefreshTokenAsync(user.IdUser);

        // 4. Generar el nuevo Access Token
        var newAccessToken = GenerateAccessToken(user);

        // 5. Devolver el nuevo par de tokens
        return Ok(new 
        { 
            Token = newAccessToken, 
            RefreshToken = newRefreshToken.Token 
        });
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error al refrescar el token: {ex.Message}");
    }
}
        private async Task<RefreshToken> ValidateRefreshTokenAsync(Guid token)
        {
            var refreshToken = await _dataService.RefreshTokenRepository
                .Query(RefreshTokenProjections.BaseTable)
                .Where(RefreshTokenFields.Token, token)
                .FirstOrDefaultAsync();

            if (refreshToken == null ||
                refreshToken.ExpireAt <= DateTime.UtcNow ||
                refreshToken.Revoked)
            {
                return null; 
            }

            return refreshToken;
        }

        [Authorize]
        [HttpGet("me")]
        public ActionResult GetMe()
        {
            return Ok(new UserInfoDto
            {
                Id    = User.FindFirst("sub")?.Value,
                Name  = User.FindFirst("name")?.Value,
                Email = User.FindFirst("email")?.Value,
                Role  = User.FindFirst("role")?.Value
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Si no existe la cookie, no hay sesión activa
            if (!Request.Cookies.ContainsKey("jwt_token"))
                return BadRequest("No hay sesión activa.");

            // Eliminamos la cookie con las mismas opciones con las que fue creada
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(-1), // Fecha en el pasado para forzar expiración
                Path = "/"
            };

            Response.Cookies.Append("jwt_token", "", cookieOptions);

            return Ok(new { message = "Sesión cerrada correctamente." });
        }

    }
}

