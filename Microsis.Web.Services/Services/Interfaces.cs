using Microsis.Names.Models;

namespace Microsis.Web.Services.Services
{
    /// <summary>
    /// Interfaccia base per il servizio di gestione dei banner
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

    /// <summary>
    /// Interfaccia per il servizio di gestione delle news
    /// </summary>
    public interface INewsService
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
        
        /// <summary>
        /// Crea una nuova news
        /// </summary>
        /// <param name="news">Dati della news</param>
        /// <param name="author">Autore</param>
        /// <returns>News creata</returns>
        Task<News> CreateAsync(News news, string author);
        
        /// <summary>
        /// Aggiorna una news esistente
        /// </summary>
        /// <param name="news">News da aggiornare</param>
        /// <param name="author">Autore</param>
        /// <returns>News aggiornata</returns>
        Task<News> UpdateAsync(News news, string author);
        
        /// <summary>
        /// Elimina una news
        /// </summary>
        /// <param name="id">ID della news</param>
        /// <returns>True se eliminato con successo</returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
