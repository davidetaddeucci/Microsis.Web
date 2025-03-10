using Microsis.Names.Models;

namespace Microsis.Web.Public.Services
{
    /// <summary>
    /// Interfaccia per il servizio client di gestione delle news
    /// </summary>
    public interface INewsPublicService
    {
        /// <summary>
        /// Ottiene tutte le news visibili
        /// </summary>
        /// <param name="includeHidden">Se true, include anche le news nascoste</param>
        /// <returns>Lista di news</returns>
        Task<IEnumerable<News>> GetAllAsync(bool includeHidden = false);
        
        /// <summary>
        /// Ottiene una news tramite ID
        /// </summary>
        /// <param name="id">ID della news</param>
        /// <returns>News o null</returns>
        Task<News?> GetByIdAsync(Guid id);
        Task<IEnumerable<News>> GetLatestAsync(int? num_records_to_retrieve);  
    }
}
