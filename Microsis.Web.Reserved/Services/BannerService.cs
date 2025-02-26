using Microsis.Names.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Microsis.Web.Reserved.Services
{
    /// <summary>
    /// Implementazione del servizio client per la gestione dei banner nell'area riservata
    /// </summary>
    public class BannerService : IBannerService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BannerService> _logger;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public BannerService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<BannerService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _baseUrl = configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7000/";
        }

        /// <summary>
        /// Ottiene tutti i banner (visibili e nascosti)
        /// </summary>
        /// <returns>Lista di banner</returns>
        public async Task<IEnumerable<Banner>> GetAllAsync()
        {
            try
            {
                var url = $"{_baseUrl.TrimEnd('/')}/api/Banners/all";
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<Banner>>(url);
                return response ?? Enumerable.Empty<Banner>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero di tutti i banner");
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
                var url = $"{_baseUrl.TrimEnd('/')}/api/Banners/ordered";
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
                var url = $"{_baseUrl.TrimEnd('/')}/api/Banners/{id}";
                return await _httpClient.GetFromJsonAsync<Banner>(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero del banner con ID {Id}", id);
                return null;
            }
        }

        /// <summary>
        /// Crea un nuovo banner
        /// </summary>
        /// <param name="banner">Dati del banner</param>
        /// <returns>Banner creato</returns>
        public async Task<Banner?> CreateAsync(Banner banner)
        {
            try
            {
                var url = $"{_baseUrl.TrimEnd('/')}/api/Banners";
                var content = new StringContent(
                    JsonSerializer.Serialize(banner),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync(url, content);
                
                if (response.IsSuccessStatusCode)
                {
                    var createdBanner = await response.Content.ReadFromJsonAsync<Banner>();
                    return createdBanner;
                }
                
                _logger.LogWarning("Errore durante la creazione del banner: {StatusCode} - {ReasonPhrase}", 
                    response.StatusCode, response.ReasonPhrase);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione del banner");
                return null;
            }
        }

        /// <summary>
        /// Aggiorna un banner esistente
        /// </summary>
        /// <param name="banner">Banner da aggiornare</param>
        /// <returns>Banner aggiornato</returns>
        public async Task<Banner?> UpdateAsync(Banner banner)
        {
            try
            {
                var url = $"{_baseUrl.TrimEnd('/')}/api/Banners/{banner.ID}";
                var content = new StringContent(
                    JsonSerializer.Serialize(banner),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync(url, content);
                
                if (response.IsSuccessStatusCode)
                {
                    var updatedBanner = await response.Content.ReadFromJsonAsync<Banner>();
                    return updatedBanner;
                }
                
                _logger.LogWarning("Errore durante l'aggiornamento del banner: {StatusCode} - {ReasonPhrase}", 
                    response.StatusCode, response.ReasonPhrase);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento del banner con ID {Id}", banner.ID);
                return null;
            }
        }

        /// <summary>
        /// Elimina un banner
        /// </summary>
        /// <param name="id">ID del banner</param>
        /// <returns>True se eliminato con successo</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var url = $"{_baseUrl.TrimEnd('/')}/api/Banners/{id}";
                var response = await _httpClient.DeleteAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'eliminazione del banner con ID {Id}", id);
                return false;
            }
        }
    }
}
