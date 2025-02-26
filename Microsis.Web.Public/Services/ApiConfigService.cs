namespace Microsis.Web.Public.Services
{
    /// <summary>
    /// Implementazione del servizio per la gestione della configurazione dell'API
    /// </summary>
    public class ApiConfigService : IApiConfigService
    {
        private readonly IConfiguration _configuration;

        public ApiConfigService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Ottiene l'URL base dell'API dalle impostazioni di configurazione
        /// </summary>
        public string BaseUrl => _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7000/";

        /// <summary>
        /// Costruisce l'URL completo per un endpoint specifico
        /// </summary>
        /// <param name="endpoint">Endpoint dell'API (es. "api/News")</param>
        /// <returns>URL completo</returns>
        public string GetUrl(string endpoint)
        {
            return $"{BaseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
        }
    }
}
