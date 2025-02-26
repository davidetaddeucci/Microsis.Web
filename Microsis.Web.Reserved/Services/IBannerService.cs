using Microsis.Names.Models;

namespace Microsis.Web.Reserved.Services
{
    /// <summary>
    /// Interfaccia per il servizio client di gestione dei banner nell'area riservata
    /// </summary>
    public interface IBannerService
    {
        /// <summary>
        /// Ottiene tutti i banner (visibili e nascosti)
        /// </summary>
        /// <returns>Lista di banner</returns>
        Task<IEnumerable<Banner>> GetAllAsync();
        
        /// <summary>
        /// Ottiene tutti i banner visibili ordinati per visualizzazione
        /// </summary>
        /// <returns>Lista di banner ordinati</returns>
        Task<IEnumerable<Banner>> GetOrderedAsync();
        
        /// <summary>
        /// Ottiene un banner tramite ID
        /// </summary>
        /// <param name="id">ID del banner</param>
        /// <returns>Banner o null</returns>
        Task<Banner?> GetByIdAsync(Guid id);
        
        /// <summary>
        /// Crea un nuovo banner
        /// </summary>
        /// <param name="banner">Dati del banner</param>
        /// <returns>Banner creato</returns>
        Task<Banner?> CreateAsync(Banner banner);
        
        /// <summary>
        /// Aggiorna un banner esistente
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
