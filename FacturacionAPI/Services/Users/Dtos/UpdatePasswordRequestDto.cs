namespace FacturacionAPI.Services.Users.Dtos
{
    public record UpdatePasswordRequestDto(
    string CurrentPassword,
    string NewPassword
);
}
