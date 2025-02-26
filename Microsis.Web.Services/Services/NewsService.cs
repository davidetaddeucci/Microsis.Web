using Microsoft.EntityFrameworkCore;
using Microsis.Names.Models;
using Microsis.Web.Services.Data;

namespace Microsis.Web.Services.Services
{
    /// <summary>
    /// Implementazione del servizio per la gestione delle news
    /// </summary>
    public class NewsService : INewsService
    {
        private readonly AppDbContext _context;

        public NewsService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Ottiene tutte le news
        /// </summary>
        /// <param name="includeHidden">Se true, include anche le news nascoste</param>
        /// <returns>Lista di news</returns>
        public async Task<IEnumerable<News>> GetAllAsync(bool includeHidden = false)
        {
            IQueryable<News> query = _context.News;
            
            if (!includeHidden)
                query = query.Where(n => n.Visible);
                
            return await query.OrderByDescending(n => n.DataPubblicazione).ToListAsync();
        }

        /// <summary>
        /// Ottiene una news tramite ID
        /// </summary>
        /// <param name="id">ID della news</param>
        /// <returns>News o null</returns>
        public async Task<News?> GetByIdAsync(Guid id)
        {
            return await _context.News.FindAsync(id);
        }

        /// <summary>
        /// Crea una nuova news
        /// </summary>
        /// <param name="news">Dati della news</param>
        /// <param name="author">Autore</param>
        /// <returns>News creata</returns>
        public async Task<News> CreateAsync(News news, string author)
        {
            // Crea un nuovo ID per la news se non è già stato impostato
            if (news.ID == Guid.Empty)
                news.ID = Guid.NewGuid();

            // Imposta i campi obbligatori
            news.LastUpdate = DateTime.Now;
            news.Author = author;
            
            // Se non specificato, imposta la data di pubblicazione a oggi
            if (news.DataPubblicazione == DateTime.MinValue)
                news.DataPubblicazione = DateTime.Now;

            // Salva la news
            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();

            return news;
        }

        /// <summary>
        /// Aggiorna una news esistente
        /// </summary>
        /// <param name="news">News da aggiornare</param>
        /// <param name="author">Autore</param>
        /// <returns>News aggiornata</returns>
        public async Task<News> UpdateAsync(News news, string author)
        {
            var newsToUpdate = await _context.News.FindAsync(news.ID)
                ?? throw new KeyNotFoundException("News non trovata");

            // Aggiorna i campi della news
            newsToUpdate.Titolo = news.Titolo;
            newsToUpdate.Contenuto = news.Contenuto;
            newsToUpdate.Descrizione = news.Descrizione;
            newsToUpdate.Visible = news.Visible;
            newsToUpdate.DataPubblicazione = news.DataPubblicazione;
            newsToUpdate.GalleriaFoto = news.GalleriaFoto;
            newsToUpdate.LastUpdate = DateTime.Now;
            newsToUpdate.Author = author;

            // Salva le modifiche
            _context.News.Update(newsToUpdate);
            await _context.SaveChangesAsync();

            return newsToUpdate;
        }

        /// <summary>
        /// Elimina una news
        /// </summary>
        /// <param name="id">ID della news</param>
        /// <returns>True se eliminata con successo</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null)
                return false;

            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Aggiunge una foto alla galleria della news
        /// </summary>
        /// <param name="newsId">ID della news</param>
        /// <param name="fotoId">ID della foto</param>
        /// <param name="author">Autore</param>
        /// <returns>News aggiornata</returns>
        public async Task<News> AddPhotoToGalleryAsync(Guid newsId, Guid fotoId, string author)
        {
            var news = await _context.News.FindAsync(newsId)
                ?? throw new KeyNotFoundException("News non trovata");
                
            var foto = await _context.Foto.FindAsync(fotoId)
                ?? throw new KeyNotFoundException("Foto non trovata");

            // Inizializza la galleria se null
            news.GalleriaFoto ??= new List<Guid>();
            
            // Aggiungi la foto se non è già presente
            if (!news.GalleriaFoto.Contains(fotoId))
            {
                news.GalleriaFoto.Add(fotoId);
                news.LastUpdate = DateTime.Now;
                news.Author = author;
                
                await _context.SaveChangesAsync();
            }

            return news;
        }

        /// <summary>
        /// Rimuove una foto dalla galleria della news
        /// </summary>
        /// <param name="newsId">ID della news</param>
        /// <param name="fotoId">ID della foto</param>
        /// <param name="author">Autore</param>
        /// <returns>News aggiornata</returns>
        public async Task<News> RemovePhotoFromGalleryAsync(Guid newsId, Guid fotoId, string author)
        {
            var news = await _context.News.FindAsync(newsId)
                ?? throw new KeyNotFoundException("News non trovata");

            // Rimuovi la foto se presente
            if (news.GalleriaFoto != null && news.GalleriaFoto.Contains(fotoId))
            {
                news.GalleriaFoto.Remove(fotoId);
                news.LastUpdate = DateTime.Now;
                news.Author = author;
                
                await _context.SaveChangesAsync();
            }

            return news;
        }
    }
}
