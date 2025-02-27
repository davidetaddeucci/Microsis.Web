using Microsis.Names.Models;
using System.Net.Http.Json;

namespace Microsis.Web.Public.Services
{
    /// <summary>
    /// Implementazione del servizio client per la gestione delle news
    /// </summary>
    public class NewsPublicService : INewsPublicService
    {
        private readonly HttpClient _httpClient;
        private readonly IApiConfigService _apiConfigService;
        private readonly ILogger<NewsPublicService> _logger;

        public NewsPublicService(
            HttpClient httpClient,
            IApiConfigService apiConfigService,
            ILogger<NewsPublicService> logger)
        {
            _httpClient = httpClient;
            _apiConfigService = apiConfigService;
            _logger = logger;
        }

        /// <summary>
        /// Ottiene tutte le news visibili
        /// </summary>
        /// <returns>Lista di news</returns>
        public async Task<IEnumerable<News>> GetAllAsync(bool includeHidden = false)
        {
            try
            {
                var url = _apiConfigService.GetUrl("api/News");
                if (includeHidden)
                {
                    url += "?includeHidden=true";
                }

                var response = await _httpClient.GetFromJsonAsync<IEnumerable<News>>(url);
                return response ?? Enumerable.Empty<News>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle news");
                return Enumerable.Empty<News>();
            }
        }
        /// <summary>
        /// Ottiene tutte le news visibili
        /// </summary>
        /// <returns>Lista di news</returns>
        public async Task<IEnumerable<News>> GetLatestAsync(int? num_records_to_retrieve = 3)
        {
            try
            {
                var url = _apiConfigService.GetUrl("api/news/latest");

                url += "?num_records_to_retrieve=" + num_records_to_retrieve;


                var response = await _httpClient.GetFromJsonAsync<IEnumerable<News>>(url);
                return response ?? Enumerable.Empty<News>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle news");
                return Enumerable.Empty<News>();
            }
        }


        /// <summary>
        /// Ottiene una news tramite ID
        /// </summary>
        /// <param name="id">ID della news</param>
        /// <returns>News o null</returns>
        public async Task<News?> GetByIdAsync(Guid id)
        {
            try
            {
                var url = _apiConfigService.GetUrl($"api/News/{id}");
                return await _httpClient.GetFromJsonAsync<News>(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero della news con ID {Id}", id);
                return null;
            }
        }
    }
}
