using inercya.EntityLite;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Configuration
{
    public static class SeedingConfig
    {
        public static async Task SeedDefaultAdminAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<WebApplication>>();

            try
            {
                var dataService = services.GetRequiredService<FacturacionDataService>();
                var config = services.GetRequiredService<IConfiguration>();

                var adminEmail = config["DefaultAdmin:Email"];
                var adminName = config["DefaultAdmin:Name"];
                var adminPassword = config["DefaultAdmin:Password"];

                if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
                    return;

                bool adminExists = await dataService.UserRepository
                .Query(UserProjections.BaseTable)
                .Where(UserFields.Email, adminEmail)
                .AnyAsync();

                if (adminExists)
                {
                    logger.LogInformation("El usuario administrador ya existe.");
                    return;
                }

                var newAdmin = new User
                {
                    Name = adminName,
                    Email = adminEmail,
                    Password = BCrypt.Net.BCrypt.HashPassword(adminPassword),
                    Role = "admin"
                };

                await dataService.UserRepository.SaveAsync(newAdmin);
                logger.LogInformation("Usuario administrador por defecto creado.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al crear el administrador por defecto.");
            }
        }
    }
}
