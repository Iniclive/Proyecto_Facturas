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

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly FacturacionDataService _dataService;
        private readonly IConfiguration _configuration;
        public AuthController(FacturacionDataService dataService)
        {
            _dataService = dataService;
            _configuration = _configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto userReg)
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

               await  _dataService.UserRepository.SaveAsync(newUser);

                return Ok(newUser.Email);
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
            var refreshToken = GenerateRefreshTokenAsync(user.IdUser);
            return Ok(new { Token = token,
                            RefreshToken = refreshToken
                            });
        }

        private string GenerateAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("sub",   user.IdUser.ToString()),
                new Claim("email", user.Email),
                new Claim("role",  user.Role),
                new Claim("jti",   Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
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
    }
}
