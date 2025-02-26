using Microsis.Names.Models;

namespace Microsis.Web.Public.Services
{
    /// <summary>
    /// Interfaccia per il servizio client di gestione delle foto
    /// </summary>
    public interface IFotoService
    {
        /// <summary>
        /// Ottiene tutte le foto visibili
        /// </summary>
        /// <param name="includeHidden">Se true, include anche le foto nascoste</param>
        /// <returns>Lista di foto</returns>
        Task<IEnumerable<Foto>> GetAllAsync(bool includeHidden = false);
        
        /// <summary>
        /// Ottiene una foto tramite ID
        /// </summary>
        /// <param name="id">ID della foto</param>
        /// <returns>Foto o null</returns>
        Task<Foto?> GetByIdAsync(Guid id);
        
        /// <summary>
        /// Ottiene le foto per un'entità specifica
        /// </summary>
        /// <param name="entityId">ID dell'entità</param>
        /// <param name="entityType">Tipo dell'entità</param>
        /// <returns>Lista di foto</returns>
        Task<IEnumerable<Foto>> GetByEntityAsync(Guid entityId, string entityType);
        
        /// <summary>
        /// Ottiene l'URL pubblico di una foto
        /// </summary>
        /// <param name="id">ID della foto</param>
        /// <returns>URL della foto</returns>
        Task<string> GetPhotoUrlAsync(Guid id);
    }
}
