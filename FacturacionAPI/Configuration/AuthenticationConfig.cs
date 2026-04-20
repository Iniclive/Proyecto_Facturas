using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FacturacionAPI.Configuration
{
    public static class AuthenticationConfig
    {
        public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services, IConfiguration config)
        {
            JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var rawToken = context.Request.Cookies["jwt_token"];
                            if (!string.IsNullOrEmpty(rawToken))
                                context.Token = rawToken.Trim('"').Trim();
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"--- Auth Failed --- {context.Exception.Message}");
                            return Task.CompletedTask;
                        }
                    };

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? string.Empty)),
                        RoleClaimType = "role",
                        NameClaimType = "name",
                    };
                });

            services.AddAuthorization();
            return services;
        }
    }
}
