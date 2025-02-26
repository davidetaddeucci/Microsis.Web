using Microsis.Names.Models;

namespace Microsis.Web.Reserved.Services
{
    /// <summary>
    /// Interfaccia per il servizio client di gestione delle news nell'area riservata
    /// </summary>
    public interface INewsService
    {
        /// <summary>
        /// Ottiene tutte le news
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
        
        /// <summary>
        /// Crea una nuova news
        /// </summary>
        /// <param name="news">News da creare</param>
        /// <param name="author">Autore della news</param>
        /// <returns>News creata</returns>
        Task<News> CreateAsync(News news, string author);
        
        /// <summary>
        /// Aggiorna una news esistente
        /// </summary>
        /// <param name="news">News da aggiornare</param>
        /// <param name="author">Autore della modifica</param>
        /// <returns>News aggiornata</returns>
        Task<News> UpdateAsync(News news, string author);
        
        /// <summary>
        /// Elimina una news
        /// </summary>
        /// <param name="id">ID della news da eliminare</param>
        /// <returns>True se eliminata con successo</returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
