namespace Microsis.Web.Public.Services
{
    /// <summary>
    /// Interfaccia per la gestione della configurazione dell'API
    /// </summary>
    public interface IApiConfigService
    {
        /// <summary>
        /// Ottiene l'URL base dell'API
        /// </summary>
        string BaseUrl { get; }
        
        /// <summary>
        /// Costruisce l'URL completo per un endpoint specifico
        /// </summary>
        /// <param name="endpoint">Endpoint dell'API (es. "api/News")</param>
        /// <returns>URL completo</returns>
        string GetUrl(string endpoint);
    }
}
