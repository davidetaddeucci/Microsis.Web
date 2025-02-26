using Microsis.Names.Models;
using System.Net.Http.Json;

namespace Microsis.Web.Public.Services
{
    /// <summary>
    /// Implementazione del servizio client per la gestione delle foto
    /// </summary>
    public class FotoService : IFotoService
    {
        private readonly HttpClient _httpClient;
        private readonly IApiConfigService _apiConfigService;
        private readonly ILogger<FotoService> _logger;

        public FotoService(
            HttpClient httpClient,
            IApiConfigService apiConfigService,
            ILogger<FotoService> logger)
        {
            _httpClient = httpClient;
            _apiConfigService = apiConfigService;
            _logger = logger;
        }

        /// <summary>
        /// Ottiene tutte le foto visibili
        /// </summary>
        /// <param name="includeHidden">Se true, include anche le foto nascoste</param>
        /// <returns>Lista di foto</returns>
        public async Task<IEnumerable<Foto>> GetAllAsync(bool includeHidden = false)
        {
            try
            {
                var url = _apiConfigService.GetUrl("api/Foto");
                if (includeHidden)
                {
                    url += "?includeHidden=true";
                }
                
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<Foto>>(url);
                return response ?? Enumerable.Empty<Foto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle foto");
                return Enumerable.Empty<Foto>();
            }
        }

        /// <summary>
        /// Ottiene una foto tramite ID
        /// </summary>
        /// <param name="id">ID della foto</param>
        /// <returns>Foto o null</returns>
        public async Task<Foto?> GetByIdAsync(Guid id)
        {
            try
            {
                var url = _apiConfigService.GetUrl($"api/Foto/{id}");
                return await _httpClient.GetFromJsonAsync<Foto>(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero della foto con ID {Id}", id);
                return null;
            }
        }
        
        /// <summary>
        /// Ottiene le foto per un'entità specifica
        /// </summary>
        /// <param name="entityId">ID dell'entità</param>
        /// <param name="entityType">Tipo dell'entità</param>
        /// <returns>Lista di foto</returns>
        public async Task<IEnumerable<Foto>> GetByEntityAsync(Guid entityId, string entityType)
        {
            try
            {
                var url = _apiConfigService.GetUrl($"api/Foto/entity/{entityId}?entityType={entityType}");
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<Foto>>(url);
                return response ?? Enumerable.Empty<Foto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle foto per l'entità {EntityType} con ID {EntityId}", entityType, entityId);
                return Enumerable.Empty<Foto>();
            }
        }
        
        /// <summary>
        /// Ottiene l'URL pubblico di una foto
        /// </summary>
        /// <param name="id">ID della foto</param>
        /// <returns>URL della foto</returns>
        public async Task<string> GetPhotoUrlAsync(Guid id)
        {
            try
            {
                var url = _apiConfigService.GetUrl($"api/Foto/{id}/url");
                var response = await _httpClient.GetFromJsonAsync<string>(url);
                return response ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dell'URL della foto con ID {Id}", id);
                return string.Empty;
            }
        }
    }
}
