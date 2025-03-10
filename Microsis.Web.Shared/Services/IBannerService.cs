using Microsis.Names.Models;

namespace Microsis.Web.Shared.Services
{
    /// <summary>
    /// Interfaccia condivisa per il servizio di gestione dei banner
    /// </summary>
    public interface IBannerService
    {
        /// <summary>
        /// Ottiene tutti i banner visibili
        /// </summary>
        /// <param name="includeHidden">Se true, include anche i banner nascosti</param>
        /// <param name="englishTranslation">Se true, utilizza le traduzioni in inglese</param>
        /// <returns>Lista di banner</returns>
        Task<IEnumerable<Banner>> GetAllAsync(bool includeHidden = false, bool englishTranslation = false);
        
        /// <summary>
        /// Ottiene un banner tramite ID
        /// </summary>
        /// <param name="id">ID del banner</param>
        /// <param name="englishTranslation">Se true, utilizza le traduzioni in inglese</param>
        /// <returns>Banner o null</returns>
        Task<Banner?> GetByIdAsync(Guid id, bool englishTranslation = false);
        
        /// <summary>
        /// Ottiene i banner visibili ordinati per campo Order
        /// </summary>
        /// <param name="englishTranslation">Se true, utilizza le traduzioni in inglese</param>
        /// <returns>Lista di banner visibili ordinati</returns>
        Task<IEnumerable<Banner>> GetVisibleOrderedAsync(bool englishTranslation = false);
    }
    
    /// <summary>
    /// Estensione dell'interfaccia IBannerService per funzionalità di amministrazione
    /// </summary>
    public interface IBannerAdminService : IBannerService
    {
        /// <summary>
        /// Crea un nuovo banner
        /// </summary>
        /// <param name="banner">Dati del banner</param>
        /// <param name="author">Autore</param>
        /// <returns>Banner creato</returns>
        Task<Banner> CreateAsync(Banner banner, string author);
        
        /// <summary>
        /// Aggiorna un banner esistente
        /// </summary>
        /// <param name="banner">Banner da aggiornare</param>
        /// <param name="author">Autore</param>
        /// <returns>Banner aggiornato</returns>
        Task<Banner> UpdateAsync(Banner banner, string author);
        
        /// <summary>
        /// Elimina un banner
        /// </summary>
        /// <param name="id">ID del banner</param>
        /// <returns>True se eliminato con successo</returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
