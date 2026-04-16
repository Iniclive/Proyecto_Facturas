using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Proyecto_Facturas.Data;
using Newtonsoft.Json.Linq;
using Microsoft.IdentityModel.JsonWebTokens;
using inercya.EntityLite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped(sp =>
    new FacturacionDataService(connectionString));
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // URL de tu proyecto Angular
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithExposedHeaders("If-Match"); ;
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var rawToken = context.Request.Cookies["jwt_token"];

                if (!string.IsNullOrEmpty(rawToken))
                {
                    
                    var cleanToken = rawToken.Trim('"').Trim();                    
                    context.Token = cleanToken;
                }

                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"--- Auth Failed ---");
                Console.WriteLine($"Error: {context.Exception.Message}");

               
                if (context.Exception.GetType() == typeof(SecurityTokenInvalidSignatureException))
                {
                    Console.WriteLine("La firma del token no coincide con la Key configurada.");
                }

                return Task.CompletedTask;
            }
        };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty)),         
            RoleClaimType = "role",
            NameClaimType = "name",

        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();


// =========================================================================
// INICIO SEEDING: Crear Administrador por defecto si no existe
// =========================================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Resolvemos el servicio de datos y la configuración
        var dataService = services.GetRequiredService<FacturacionDataService>();
        var configuration = services.GetRequiredService<IConfiguration>();

        // Leemos las credenciales del appsettings.json
        var adminEmail = configuration["DefaultAdmin:Email"];
        var adminName = configuration["DefaultAdmin:Name"];
        var adminPassword = configuration["DefaultAdmin:Password"];

        if (!string.IsNullOrEmpty(adminEmail) && !string.IsNullOrEmpty(adminPassword))
        {
            // Comprobamos si ya existe usando la misma lógica de tu endpoint
            bool adminExists = await dataService.UserRepository
                .Query(UserProjections.BaseTable) // Asegúrate de tener los using necesarios si da error aquí
                .Where(UserFields.Email, adminEmail)
                .AnyAsync();

            if (!adminExists)
            {
                // Hasheamos la contraseña
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(adminPassword);

                var newAdmin = new User
                {
                    Name = adminName,
                    Email = adminEmail,
                    Password = passwordHash,
                    Role = "admin" // Asignamos el rol de administrador explícitamente
                };

                // Guardamos el usuario
                await dataService.UserRepository.SaveAsync(newAdmin);
                Console.WriteLine("Usuario administrador por defecto creado exitosamente.");
            }
            else
            {
                Console.WriteLine("El usuario administrador ya existe. Se omite la creación.");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error al crear el administrador por defecto: {ex.Message}");
    }
}
// =========================================================================


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseCors("AllowAngular");

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection(); // solo en producci�n donde hay HTTPS real
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
