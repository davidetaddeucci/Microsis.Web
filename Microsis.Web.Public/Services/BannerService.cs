using Microsis.Names.Models;
using System.Net.Http.Json;

namespace Microsis.Web.Public.Services
{
    /// <summary>
    /// Implementazione del servizio client per la gestione dei banner
    /// </summary>
    public class BannerService : IBannerService
    {
        private readonly HttpClient _httpClient;
        private readonly IApiConfigService _apiConfigService;
        private readonly ILogger<BannerService> _logger;

        public BannerService(
            HttpClient httpClient,
            IApiConfigService apiConfigService,
            ILogger<BannerService> logger)
        {
            _httpClient = httpClient;
            _apiConfigService = apiConfigService;
            _logger = logger;
        }

        /// <summary>
        /// Ottiene tutti i banner visibili
        /// </summary>
        /// <returns>Lista di banner</returns>
        public async Task<IEnumerable<Banner>> GetAllAsync()
        {
            try
            {
                var url = _apiConfigService.GetUrl("api/Banners");
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<Banner>>(url);
                return response ?? Enumerable.Empty<Banner>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei banner");
                return Enumerable.Empty<Banner>();
            }
        }

        /// <summary>
        /// Ottiene tutti i banner visibili ordinati per visualizzazione
        /// </summary>
        /// <returns>Lista di banner ordinati</returns>
        public async Task<IEnumerable<Banner>> GetOrderedAsync()
        {
            try
            {
                var url = _apiConfigService.GetUrl("api/Banners/ordered");
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<Banner>>(url);
                return response ?? Enumerable.Empty<Banner>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei banner ordinati");
                return Enumerable.Empty<Banner>();
            }
        }

        /// <summary>
        /// Ottiene un banner tramite ID
        /// </summary>
        /// <param name="id">ID del banner</param>
        /// <returns>Banner o null</returns>
        public async Task<Banner?> GetByIdAsync(Guid id)
        {
            try
            {
                var url = _apiConfigService.GetUrl($"api/Banners/{id}");
                return await _httpClient.GetFromJsonAsync<Banner>(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero del banner con ID {Id}", id);
                return null;
            }
        }
    }
}
