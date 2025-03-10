using Microsis.Names.Models;

namespace Microsis.Web.Reserved.Services
{
    /// <summary>
    /// Interfaccia per il servizio di gestione delle news nell'area riservata
    /// </summary>
    public interface INewsService
    {
        /// <summary>
        /// Ottiene tutte le news
        /// </summary>
        /// <returns>Lista di news</returns>
        Task<List<News>> GetAllNewsAsync();
        
        /// <summary>
        /// Ottiene una news tramite ID
        /// </summary>
        /// <param name="id">ID della news</param>
        /// <returns>News o null</returns>
        Task<News?> GetNewsByIdAsync(Guid id);
        
        /// <summary>
        /// Crea una nuova news
        /// </summary>
        /// <param name="news">News da creare</param>
        /// <returns>True se creata con successo</returns>
        Task<bool> CreateNewsAsync(News news);
        
        /// <summary>
        /// Aggiorna una news esistente
        /// </summary>
        /// <param name="news">News da aggiornare</param>
        /// <returns>True se aggiornata con successo</returns>
        Task<bool> UpdateNewsAsync(News news);
        
        /// <summary>
        /// Elimina una news
        /// </summary>
        /// <param name="id">ID della news da eliminare</param>
        /// <returns>True se eliminata con successo</returns>
        Task<bool> DeleteNewsAsync(Guid id);
        
        /// <summary>
        /// Cambia lo stato di attivazione di una news
        /// </summary>
        /// <param name="id">ID della news</param>
        /// <param name="isActive">Nuovo stato di attivazione</param>
        /// <returns>True se l'operazione ha avuto successo</returns>
        Task<bool> ToggleNewsStatusAsync(Guid id, bool isActive);
        
        /// <summary>
        /// Ottiene le ultime news
        /// </summary>
        /// <param name="count">Numero di news da ottenere</param>
        /// <returns>Lista di news</returns>
        Task<List<News>> GetLatestNewsAsync(int count = 5);
    }
}
