using Microsoft.EntityFrameworkCore;
using Microsis.Names.Models;
using Microsis.Web.Services.Data;
using System.Linq;

namespace Microsis.Web.Services.Services
{
    /// <summary>
    /// Implementazione del servizio per la gestione delle news
    /// </summary>
    public class NewsService : INewsService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<NewsService> _logger;

        public NewsService(AppDbContext context, ILogger<NewsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Ottiene tutte le news
        /// </summary>
        /// <param name="includeHidden">Se true, include anche le news nascoste</param>
        /// <returns>Lista di news</returns>
        public async Task<IEnumerable<News>> GetAllAsync(bool includeHidden = false)
        {
            try
            {
                IQueryable<News> query = _context.News;
                
                if (!includeHidden)
                    query = query.Where(n => n.Visible);
                    
                return await query.OrderByDescending(n => n.DataPubblicazione).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle news");
                return Enumerable.Empty<News>();
            }
        }

        public async Task<IEnumerable<News>> GetLastAsync(bool includeHidden = false)
        {
            try
            {
                IQueryable<News> query = _context.News;

                if (!includeHidden)
                    query = query.Where(n => n.Visible);

                return await query.OrderByDescending(n => n.DataPubblicazione).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle news");
                return Enumerable.Empty<News>();
            }
        }

        public async Task<IEnumerable<News>> GetLatestAsync(int num_records_to_retrieve = 3)
        {
            try
            {
                IQueryable<News> query = _context.News;

                return await query.OrderByDescending(n => n.DataPubblicazione)
                          .Take(num_records_to_retrieve)
                          .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle news");
                return Enumerable.Empty<News>();
            }
        }

        /// <summary>
        /// Ottiene una news tramite ID
        /// </summary>
        /// <param name="id">ID della news</param>
        /// <returns>News o null</returns>
        public async Task<News?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.News
                    .Include(n => n.GalleriaFoto)
                    .FirstOrDefaultAsync(n => n.ID == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero della news con ID {Id}", id);
                return null;
            }
        }

        /// <summary>
        /// Crea una nuova news
        /// </summary>
        /// <param name="news">Dati della news</param>
        /// <param name="author">Autore</param>
        /// <returns>News creata</returns>
        public async Task<News> CreateAsync(News news, string author)
        {
            try
            {
                // Crea un nuovo ID per la news se non è già stato impostato
                if (news.ID == Guid.Empty)
                    news.ID = Guid.NewGuid();

                // Imposta i campi obbligatori
                news.Author = author;
                news.DataPubblicazione = DateTime.Now;
                news.LastUpdate = DateTime.Now;
                
                // Salva la news
                await _context.News.AddAsync(news);
                await _context.SaveChangesAsync();

                return news;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione della news");
                throw;
            }
        }

        /// <summary>
        /// Aggiorna una news esistente
        /// </summary>
        /// <param name="news">News da aggiornare</param>
        /// <param name="author">Autore</param>
        /// <returns>News aggiornata</returns>
        public async Task<News> UpdateAsync(News news, string author)
        {
            try
            {
                var newsToUpdate = await _context.News.FindAsync(news.ID)
                    ?? throw new KeyNotFoundException("News non trovata");

                // Aggiorna i campi della news
                newsToUpdate.Titolo = news.Titolo;
                newsToUpdate.Descrizione = news.Descrizione;
                newsToUpdate.Contenuto = news.Contenuto;
                newsToUpdate.Visible = news.Visible;
                newsToUpdate.Categoria = news.Categoria;
                newsToUpdate.Tags = news.Tags;
                newsToUpdate.ImmagineUrl = news.ImmagineUrl;
                newsToUpdate.SlugUrl = news.SlugUrl;
                newsToUpdate.Author = author;
                newsToUpdate.LastUpdate = DateTime.Now;

                // Salva le modifiche
                _context.News.Update(newsToUpdate);
                await _context.SaveChangesAsync();

                return newsToUpdate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento della news con ID {Id}", news.ID);
                throw;
            }
        }

        /// <summary>
        /// Elimina una news
        /// </summary>
        /// <param name="id">ID della news</param>
        /// <returns>True se eliminato con successo</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var news = await _context.News.FindAsync(id);
                if (news == null)
                    return false;

                _context.News.Remove(news);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'eliminazione della news con ID {Id}", id);
                return false;
            }
        }
    }
}
