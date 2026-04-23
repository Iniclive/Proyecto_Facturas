using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FacturacionAPI.Application.Auth.Dtos;

using FacturacionAPI.Shared.Results;
using inercya.EntityLite;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Application.Auth;

public class AuthService : IAuthService
{
    private readonly UserRepository _userRepository;
    private readonly JwtSettings _jwtSettings;

    public AuthService(FacturacionDataService dataService, IOptions<JwtSettings> jwtOptions)
    {
        _userRepository = dataService.UserRepository;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<Result<UserRegisteredDto>> RegisterAsync(
        RegisterUserDto dto)
    {
        var emailExists = await _userRepository
            .Query(UserProjections.BaseTable)
            .Where(UserFields.Email, dto.Email)
            .AnyAsync();

        if (emailExists)
        {
            return Result<UserRegisteredDto>.Failure(new Error(
                Code: "EmailAlreadyRegistered",
                Message: "El email ya está registrado.",
                Type: ErrorType.Conflict
            ));
        }

        var newUser = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "user"
        };

        await _userRepository.SaveAsync(newUser);

        return Result<UserRegisteredDto>.Success(new UserRegisteredDto
        {
            Name = newUser.Name,
            Email = newUser.Email
        });
    }

    public async Task<Result<LoginResultDto>> LoginAsync(
        LoginUserDto dto)
    {
        var user = await _userRepository
            .Query(UserProjections.BaseTable)
            .Where(UserFields.Email, dto.Email)
            .FirstOrDefaultAsync();

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            return Result<LoginResultDto>.Failure(new Error(
                Code: "InvalidCredentials",
                Message: "Credenciales inválidas",
                Type: ErrorType.Unauthorized
            ));
        }

        var expiresAt = DateTime.UtcNow.AddDays(_jwtSettings.ExpirationDays);
        var token = GenerateAccessToken(user, expiresAt);

        return Result<LoginResultDto>.Success(new LoginResultDto(
            Token: token,
            UserId: user.IdUser,
            Name: user.Name,
            Email: user.Email,
            Role: user.Role,
            ExpiresAt: expiresAt
        ));
    }

    private string GenerateAccessToken(User user, DateTime expiresAt)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("sub",   user.IdUser.ToString()),
            new Claim("name",  user.Name),
            new Claim("email", user.Email),
            new Claim("role",  user.Role),
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}