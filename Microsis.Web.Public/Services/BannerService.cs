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
        /// <param name="includeHidden">Se true, include anche i banner nascosti</param>
        /// <param name="englishTranslation">Se true, utilizza le traduzioni in inglese</param>
        /// <returns>Lista di banner</returns>
        public async Task<IEnumerable<Banner>> GetAllAsync(bool includeHidden = false, bool englishTranslation = false)
        {
            try
            {
                var url = _apiConfigService.GetUrl("api/Banners");
                var queryParams = new List<string>();
                
                if (includeHidden)
                {
                    queryParams.Add("includeHidden=true");
                }
                
                if (englishTranslation)
                {
                    queryParams.Add("englishTranslation=true");
                }
                
                if (queryParams.Any())
                {
                    url += "?" + string.Join("&", queryParams);
                }
                
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<Banner>>(url);
                var banners = response ?? Enumerable.Empty<Banner>();
                
                // Se server API non supporta ancora il parametro englishTranslation, 
                // applichiamo la traduzione lato client
                if (englishTranslation && response != null)
                {
                    foreach (var banner in banners)
                    {
                        if (!string.IsNullOrEmpty(banner.Title_EN))
                        {
                            banner.Title = banner.Title_EN;
                        }
                        
                        if (!string.IsNullOrEmpty(banner.Subtitle_EN))
                        {
                            banner.Subtitle = banner.Subtitle_EN;
                        }
                    }
                }
                
                return banners;
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
        /// <param name="englishTranslation">Se true, utilizza le traduzioni in inglese</param>
        /// <returns>Lista di banner ordinati</returns>
        public async Task<IEnumerable<Banner>> GetVisibleOrderedAsync(bool englishTranslation = false)
        {
            try
            {
                var url = _apiConfigService.GetUrl("api/Banners/ordered");
                
                if (englishTranslation)
                {
                    url += "?englishTranslation=true";
                }
                
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<Banner>>(url);
                var banners = response ?? Enumerable.Empty<Banner>();
                
                // Se server API non supporta ancora il parametro englishTranslation, 
                // applichiamo la traduzione lato client
                if (englishTranslation && response != null)
                {
                    foreach (var banner in banners)
                    {
                        if (!string.IsNullOrEmpty(banner.Title_EN))
                        {
                            banner.Title = banner.Title_EN;
                        }
                        
                        if (!string.IsNullOrEmpty(banner.Subtitle_EN))
                        {
                            banner.Subtitle = banner.Subtitle_EN;
                        }
                    }
                }
                
                return banners;
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
        /// <param name="englishTranslation">Se true, utilizza le traduzioni in inglese</param>
        /// <returns>Banner o null</returns>
        public async Task<Banner?> GetByIdAsync(Guid id, bool englishTranslation = false)
        {
            try
            {
                var url = _apiConfigService.GetUrl($"api/Banners/{id}");
                
                if (englishTranslation)
                {
                    url += "?englishTranslation=true";
                }
                
                var banner = await _httpClient.GetFromJsonAsync<Banner>(url);
                
                // Se server API non supporta ancora il parametro englishTranslation, 
                // applichiamo la traduzione lato client
                if (englishTranslation && banner != null)
                {
                    if (!string.IsNullOrEmpty(banner.Title_EN))
                    {
                        banner.Title = banner.Title_EN;
                    }
                    
                    if (!string.IsNullOrEmpty(banner.Subtitle_EN))
                    {
                        banner.Subtitle = banner.Subtitle_EN;
                    }
                }
                
                return banner;
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
