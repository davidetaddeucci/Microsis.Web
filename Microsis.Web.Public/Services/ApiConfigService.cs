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

        public string BaseUrl => _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7000/";

        public string GetUrl(string endpoint)
        {
            // TrimEnd('/') rimuove eventuali slash finali da BaseUrl
            // TrimStart('/') rimuove eventuali slash iniziali da endpoint
            // Questo evita URL come "https://localhost:7000//api/news"
            return $"{BaseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
        }
    }
}
