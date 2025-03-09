using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Syncfusion.Blazor;
using Microsis.Web.Reserved.Services;
using System;

namespace Microsis.Web.Reserved
{
    public class Program
    {
        // Entry point esplicito per soddisfare i requisiti di compilazione
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    // Classe Startup per configurazione di base
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            // Registra Syncfusion Blazor Service
            services.AddSyncfusionBlazor();

            // Configura HttpClient per le chiamate API
            services.AddHttpClient("MicrosisAPI", client => 
            {
                client.BaseAddress = new Uri(Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7000/");
            });

            // Aggiungi HttpClient generico per i casi d'uso semplici
            services.AddScoped(sp => new HttpClient 
            { 
                BaseAddress = new Uri(Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7000/") 
            });

            // Registra servizi per l'autenticazione
            services.AddScoped<ProtectedSessionStorage>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            services.AddScoped<IAuthService, AuthService>();

            // Registra servizi per l'API
            services.AddScoped<IApiConfigService, ApiConfigService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IFotoService, FotoService>();
            services.AddScoped<IProgettoUEService, ProgettoUEService>();
            services.AddScoped<IServizioService, ServizioService>();

            // Aggiungi autorizzazione
            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Registra la licenza di Syncfusion (se disponibile)
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Configuration["SyncfusionLicense"]);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
