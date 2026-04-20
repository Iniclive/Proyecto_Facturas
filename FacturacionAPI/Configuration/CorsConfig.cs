namespace FacturacionAPI.Configuration
{
    public static class CorsConfig
    {
        public const string AngularPolicy = "AllowAngular";

        public static IServiceCollection AddAngularCors(
            this IServiceCollection services, IConfiguration config)
        {
            var allowedOrigin = config["Cors:AngularOrigin"] ?? "http://localhost:4200";

            services.AddCors(options =>
            {
                options.AddPolicy(AngularPolicy, policy =>
                {
                    policy.WithOrigins(allowedOrigin)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .WithExposedHeaders("If-Match");
                });
            });
            return services;
        }
    }
}
