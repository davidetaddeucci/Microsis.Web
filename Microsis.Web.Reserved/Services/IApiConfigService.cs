namespace Microsis.Web.Reserved.Services
{
    /// <summary>
    /// Interfaccia per la gestione della configurazione dell'API nell'area riservata
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
        
        /// <summary>
        /// Costruisce il percorso API per un endpoint specifico
        /// </summary>
        /// <param name="endpoint">Endpoint dell'API (es. "News")</param>
        /// <returns>URL API completo</returns>
        string GetApiEndpoint(string endpoint);
        
        /// <summary>
        /// Ottiene il token JWT corrente (se presente)
        /// </summary>
        string? Token { get; }
        
        /// <summary>
        /// Imposta il token JWT per le richieste autenticate
        /// </summary>
        /// <param name="token">Token JWT</param>
        void SetToken(string token);
        
        /// <summary>
        /// Cancella il token JWT corrente
        /// </summary>
        void ClearToken();
    }
}
