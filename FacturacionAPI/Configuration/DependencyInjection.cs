using FacturacionAPI.Application.Auth;
using FacturacionAPI.Application.Auth.Dtos;
using FacturacionAPI.Application.Facturas;
using FacturacionAPI.Application.Products;
using FacturacionAPI.Infraestructure.Auth;
using FacturacionAPI.Shared.Abstractions;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddScoped(sp => new FacturacionDataService(connectionString));
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.Configure<JwtSettings>(config.GetSection("Jwt"));
        return services;
    }

    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IFacturaService, FacturaService>();
        return services;
    }
}