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
        public async Task<IEnumerable<Banner>> GetAllAsync(bool includeHidden = false)
        {
            try
            {
                var url = _apiConfigService.GetUrl("api/Banners");
                if (includeHidden)
                {
                    url += "?includeHidden=true";
                }
                
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
            return await GetVisibleOrderedAsync();
        }
        
        /// <summary>
        /// Ottiene tutti i banner visibili ordinati per visualizzazione
        /// </summary>
        /// <returns>Lista di banner ordinati</returns>
        public async Task<IEnumerable<Banner>> GetVisibleOrderedAsync()
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
        
        /// <summary>
        /// Ottiene la data dell'ultimo banner modificato
        /// </summary>
        /// <returns>Data ultima modifica</returns>
        public async Task<DateTime> GetAsyncUltimoModifica()
        {
            try
            {
                var banners = await GetAllAsync(true);
                if (!banners.Any())
                    return DateTime.MinValue;
                    
                return banners.Max(b => b.UpdateDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero della data ultima modifica dei banner");
                return DateTime.MinValue;
            }
        }
    }
}
