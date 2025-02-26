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
        public async Task<IEnumerable<Banner>> GetAllAsync(bool includeHidden = false)
        {
            try
            {
                var url = includeHidden
                    ? $"{_baseUrl.TrimEnd('/')}/api/Banners/all"
                    : $"{_baseUrl.TrimEnd('/')}/api/Banners";
                
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
        /// Ottiene tutti i banner visibili ordinati per campo Order
        /// </summary>
        /// <returns>Lista di banner visibili ordinati</returns>
        public async Task<IEnumerable<Banner>> GetVisibleOrderedAsync()
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
        /// Alias per compatibilità con il vecchio codice
        /// </summary>
        public async Task<IEnumerable<Banner>> GetOrderedAsync()
        {
            return await GetVisibleOrderedAsync();
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
        /// <param name="author">Autore della modifica</param>
        /// <returns>Banner creato</returns>
        public async Task<Banner> CreateAsync(Banner banner, string author)
        {
            try
            {
                // Assicurati che l'autore sia impostato
                banner.Author = author;
                
                var url = $"{_baseUrl.TrimEnd('/')}/api/Banners";
                var content = new StringContent(
                    JsonSerializer.Serialize(banner),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync(url, content);
                
                if (response.IsSuccessStatusCode)
                {
                    var createdBanner = await response.Content.ReadFromJsonAsync<Banner>();
                    return createdBanner ?? banner;
                }
                
                _logger.LogWarning("Errore durante la creazione del banner: {StatusCode} - {ReasonPhrase}", 
                    response.StatusCode, response.ReasonPhrase);
                throw new HttpRequestException($"Errore durante la creazione del banner: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione del banner");
                throw;
            }
        }

        /// <summary>
        /// Metodo vecchio per compatibilità con il codice esistente
        /// </summary>
        public async Task<Banner?> CreateAsync(Banner banner)
        {
            return await CreateAsync(banner, "system");
        }

        /// <summary>
        /// Aggiorna un banner esistente
        /// </summary>
        /// <param name="banner">Banner da aggiornare</param>
        /// <param name="author">Autore della modifica</param>
        /// <returns>Banner aggiornato</returns>
        public async Task<Banner> UpdateAsync(Banner banner, string author)
        {
            try
            {
                // Assicurati che l'autore sia impostato
                banner.Author = author;
                
                var url = $"{_baseUrl.TrimEnd('/')}/api/Banners/{banner.ID}";
                var content = new StringContent(
                    JsonSerializer.Serialize(banner),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync(url, content);
                
                if (response.IsSuccessStatusCode)
                {
                    var updatedBanner = await response.Content.ReadFromJsonAsync<Banner>();
                    return updatedBanner ?? banner;
                }
                
                _logger.LogWarning("Errore durante l'aggiornamento del banner: {StatusCode} - {ReasonPhrase}", 
                    response.StatusCode, response.ReasonPhrase);
                throw new HttpRequestException($"Errore durante l'aggiornamento del banner: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento del banner con ID {Id}", banner.ID);
                throw;
            }
        }

        /// <summary>
        /// Metodo vecchio per compatibilità con il codice esistente
        /// </summary>
        public async Task<Banner?> UpdateAsync(Banner banner)
        {
            try
            {
                return await UpdateAsync(banner, "system");
            }
            catch
            {
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
