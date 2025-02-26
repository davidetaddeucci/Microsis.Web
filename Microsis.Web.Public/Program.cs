using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsis.Web.Public.Components;
using Microsis.Web.Public.Services;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Aggiungi Syncfusion UI
builder.Services.AddSyncfusionBlazor();

// Configura HttpClient per le chiamate API
builder.Services.AddHttpClient("MicrosisAPI", client => 
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7000/");
});

// Aggiungi HttpClient generico per i casi d'uso semplici
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7000/") 
});

// Registra servizi personalizzati
builder.Services.AddScoped<IApiConfigService, ApiConfigService>();
builder.Services.AddScoped<IBannerService, BannerService>();

// Configura CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowApiServer", 
        policy => policy
            .WithOrigins(builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7000")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors("AllowApiServer");

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
