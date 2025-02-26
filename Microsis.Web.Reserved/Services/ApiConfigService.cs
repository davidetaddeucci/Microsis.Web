using System.Net.Http.Headers;

namespace Microsis.Web.Reserved.Services
{
    /// <summary>
    /// Implementazione del servizio per la gestione della configurazione dell'API nell'area riservata
    /// </summary>
    public class ApiConfigService : IApiConfigService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private string? _token;

        public ApiConfigService(
            IConfiguration configuration,
            HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Ottiene l'URL base dell'API dalle impostazioni di configurazione
        /// </summary>
        public string BaseUrl => _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7000/";

        /// <summary>
        /// Ottiene il token JWT corrente (se presente)
        /// </summary>
        public string? Token => _token;

        /// <summary>
        /// Costruisce l'URL completo per un endpoint specifico
        /// </summary>
        /// <param name="endpoint">Endpoint dell'API (es. "api/News")</param>
        /// <returns>URL completo</returns>
        public string GetUrl(string endpoint)
        {
            return $"{BaseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
        }

        /// <summary>
        /// Imposta il token JWT per le richieste autenticate
        /// </summary>
        /// <param name="token">Token JWT</param>
        public void SetToken(string token)
        {
            _token = token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        /// <summary>
        /// Cancella il token JWT corrente
        /// </summary>
        public void ClearToken()
        {
            _token = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
