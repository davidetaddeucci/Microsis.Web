using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor;
using Microsis.Web.Public.Components;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi i servizi per Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registra i servizi Syncfusion Blazor
builder.Services.AddSyncfusionBlazor();

// Aggiungi altri servizi dell'applicazione qui
builder.Services.AddHttpClient();

// Configura le licenze Syncfusion (sostituisci con la tua chiave di licenza)
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDAxNEAzMjM4MkUzMTJFMzlMeXJkaVJFV2Z5R3o5ZXNEVnNOQjFqUmx2MW0xZkR2TGdud2MrVGNJRlBzPQ==");

var app = builder.Build();

// Configura la pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();