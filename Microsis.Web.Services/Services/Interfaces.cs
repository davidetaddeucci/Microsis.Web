using Microsis.Names.Models;
using Microsoft.AspNetCore.Http;

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

        Task<IEnumerable<News>> GetLatestAsync(int num_records_to_retrieve=3);
    }
    
    /// <summary>
    /// Interfaccia per il servizio di gestione delle foto
    /// </summary>
    public interface IFotoService
    {
        /// <summary>
        /// Ottiene tutte le foto
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
        /// Crea una nuova foto
        /// </summary>
        /// <param name="foto">Dati della foto</param>
        /// <param name="file">File della foto</param>
        /// <param name="author">Autore</param>
        /// <returns>Foto creata</returns>
        Task<Foto> CreateAsync(Foto foto, IFormFile file, string author);
        
        /// <summary>
        /// Aggiorna una foto esistente
        /// </summary>
        /// <param name="foto">Foto da aggiornare</param>
        /// <param name="author">Autore</param>
        /// <returns>Foto aggiornata</returns>
        Task<Foto> UpdateAsync(Foto foto, string author);
        
        /// <summary>
        /// Elimina una foto
        /// </summary>
        /// <param name="id">ID della foto</param>
        /// <returns>True se eliminato con successo</returns>
        Task<bool> DeleteAsync(Guid id);
        
        /// <summary>
        /// Ottiene l'URL pubblico di una foto
        /// </summary>
        /// <param name="id">ID della foto</param>
        /// <returns>URL della foto</returns>
        Task<string> GetPhotoUrlAsync(Guid id);
    }
}
