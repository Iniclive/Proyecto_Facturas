namespace FacturacionAPI.Application.Auth.Dtos
{
    public record LoginResultDto(
    string Token,
    int UserId,
    string Name,
    string Email,
    string Role,
    DateTime ExpiresAt
        );
}
