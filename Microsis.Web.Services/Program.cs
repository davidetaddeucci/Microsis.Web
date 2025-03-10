using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsis.Web.Services.Data;
using Microsis.Web.Services.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurazione di Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microsis API", Version = "v1" });
    
    // Configurazione per l'autenticazione JWT in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configurazione della connessione al database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
    
    // Configura i warnings per ignorare l'avviso di migrazione in corso
    options.ConfigureWarnings(warnings => 
        warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
});

// Configurazione dell'autenticazione JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured"))
        )
    };
});

// Registrazione dei servizi personalizzati
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProgettoUEService, ProgettoUEService>();
builder.Services.AddScoped<IServizioService, ServizioService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IFotoService, FotoService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IBannerAdminService, BannerService>();
builder.Services.AddScoped<ISettoreService, SettoreService>();

// Configurazione CORS per permettere le richieste dai client Blazor
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy => policy
            .WithOrigins(
                builder.Configuration["AllowedOrigins:PublicSite"] ?? "http://localhost:5100",
                builder.Configuration["AllowedOrigins:ReservedSite"] ?? "http://localhost:5200"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Inizializzazione del database con i dati di seed realistici
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Modifica qui per evitare errori di migrazione
        var context = services.GetRequiredService<AppDbContext>();
        
        // Controlla se ci sono migrazioni da applicare ma non generare errori
        if (context.Database.GetPendingMigrations().Any())
        {
            // Log che informa sulle migrazioni in attesa ma non bloccante
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogWarning("Ci sono migrazioni in attesa. Eseguire 'Add-Migration' e 'Update-Database' con EF Tools.");
        }
        else
        {
            // Esegui il seeding solo se non ci sono migrazioni in attesa
            RealisticSeedData.Initialize(services);
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Si è verificato un errore durante il seeding realistico del DB.");
    }
}

app.Run();
