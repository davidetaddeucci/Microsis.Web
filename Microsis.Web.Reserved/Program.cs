using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

            // Registra servizi personalizzati
            services.AddScoped<IApiConfigService, ApiConfigService>();
            services.AddScoped<IBannerService, BannerService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
