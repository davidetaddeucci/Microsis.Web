using Microsis.Names.Models;

namespace Microsis.Web.Reserved.Services
{
    /// <summary>
    /// Interfaccia per il servizio client di gestione dei banner nell'area amministrativa
    /// </summary>
    public interface IBannerService
    {
        /// <summary>
        /// Ottiene tutti i banner visibili
        /// </summary>
        /// <param name="includeHidden">Se true, include anche i banner nascosti</param>
        /// <returns>Lista di banner</returns>
        Task<IEnumerable<Banner>> GetAllAsync(bool includeHidden = false);
        
        /// <summary>
        /// Ottiene un banner tramite ID
        /// </summary>
        /// <param name="id">ID del banner</param>
        /// <returns>Banner o null</returns>
        Task<Banner?> GetByIdAsync(Guid id);
        
        /// <summary>
        /// Ottiene i banner visibili ordinati per campo Order
        /// </summary>
        /// <returns>Lista di banner visibili ordinati</returns>
        Task<IEnumerable<Banner>> GetVisibleOrderedAsync();
        
        /// <summary>
        /// Alias per compatibilità
        /// </summary>
        Task<IEnumerable<Banner>> GetOrderedAsync();
        
        /// <summary>
        /// Crea un nuovo banner
        /// </summary>
        /// <param name="banner">Dati del banner</param>
        /// <param name="author">Autore</param>
        /// <returns>Banner creato</returns>
        Task<Banner> CreateAsync(Banner banner, string author);
        
        /// <summary>
        /// Crea un nuovo banner (metodo di compatibilità)
        /// </summary>
        /// <param name="banner">Banner da creare</param>
        /// <returns>Banner creato</returns>
        Task<Banner?> CreateAsync(Banner banner);
        
        /// <summary>
        /// Aggiorna un banner esistente
        /// </summary>
        /// <param name="banner">Banner da aggiornare</param>
        /// <param name="author">Autore</param>
        /// <returns>Banner aggiornato</returns>
        Task<Banner> UpdateAsync(Banner banner, string author);
        
        /// <summary>
        /// Aggiorna un banner esistente (metodo di compatibilità)
        /// </summary>
        /// <param name="banner">Banner da aggiornare</param>
        /// <returns>Banner aggiornato</returns>
        Task<Banner?> UpdateAsync(Banner banner);
        
        /// <summary>
        /// Elimina un banner
        /// </summary>
        /// <param name="id">ID del banner</param>
        /// <returns>True se eliminato con successo</returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
