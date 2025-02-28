using Microsis.Names.Models;

namespace Microsis.Web.Public.Services
{
    /// <summary>
    /// Interfaccia per il servizio client di gestione dei banner
    /// </summary>
    public interface IBannerService
    {
        /// <summary>
        /// Ottiene tutti i banner visibili
        /// </summary>
        /// <returns>Lista di banner</returns>
        Task<IEnumerable<Banner>> GetAllAsync(bool includeHidden = false, bool englishTranslation = false);
        
        /// <summary>
        /// Ottiene tutti i banner visibili ordinati per visualizzazione
        /// </summary>
        /// <returns>Lista di banner ordinati</returns>
        Task<IEnumerable<Banner>> GetOrderedAsync();
        
        /// <summary>
        /// Alias per GetOrderedAsync per compatibilità
        /// </summary>
        /// <returns>Lista di banner ordinati</returns>
        Task<IEnumerable<Banner>> GetVisibleOrderedAsync(bool englishTranslation = false);
        
        /// <summary>
        /// Ottiene un banner tramite ID
        /// </summary>
        /// <param name="id">ID del banner</param>
        /// <returns>Banner o null</returns>
        Task<Banner?> GetByIdAsync(Guid id, bool englishTranslation = false);
        
        /// <summary>
        /// Ottiene la data dell'ultimo banner modificato
        /// </summary>
        /// <returns>Data ultima modifica</returns>
        Task<DateTime> GetAsyncUltimoModifica();
    }
}
