using Microsoft.EntityFrameworkCore;
using Microsis.Names.Models;
using Microsis.Web.Services.Data;

namespace Microsis.Web.Services.Services
{
    /// <summary>
    /// Interfaccia per il servizio di gestione dei servizi.
    /// </summary>
    public interface IServizioService
    {
        /// <summary>
        /// Ottiene tutti i servizi.
        /// </summary>
        /// <param name="includeHidden">Se true, include anche i servizi nascosti.</param>
        /// <returns>Una lista di servizi.</returns>
        Task<IEnumerable<Servizio>> GetAllAsync(bool includeHidden = false);

        /// <summary>
        /// Ottiene un servizio tramite il suo ID.
        /// </summary>
        /// <param name="id">L'ID del servizio.</param>
        /// <returns>Il servizio, o null se non trovato.</returns>
        Task<Servizio?> GetByIdAsync(Guid id);

        /// <summary>
        /// Crea un nuovo servizio.
        /// </summary>
        /// <param name="servizio">I dati del servizio da creare.</param>
        /// <param name="author">L'autore del servizio.</param>
        /// <returns>Il servizio creato.</returns>
        Task<Servizio> CreateAsync(Servizio servizio, string author);

        /// <summary>
        /// Aggiorna un servizio esistente.
        /// </summary>
        /// <param name="servizio">Il servizio con i dati aggiornati.</param>
        /// <param name="author">L'autore che sta effettuando l'aggiornamento.</param>
        /// <returns>Il servizio aggiornato.</returns>
        /// <exception cref="KeyNotFoundException">Se il servizio con l'ID specificato non viene trovato.</exception>
        Task<Servizio> UpdateAsync(Servizio servizio, string author);

        /// <summary>
        /// Elimina un servizio.
        /// </summary>
        /// <param name="id">L'ID del servizio da eliminare.</param>
        /// <returns>True se l'eliminazione è avvenuta con successo, false altrimenti.</returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Aggiunge una foto alla galleria di un servizio.
        /// </summary>
        /// <param name="servizioId">L'ID del servizio.</param>
        /// <param name="fotoId">L'ID della foto da aggiungere.</param>
        /// <param name="author">L'autore dell'operazione.</param>
        /// <returns>Il servizio aggiornato.</returns>
        /// <exception cref="KeyNotFoundException">Se il servizio o la foto non vengono trovati.</exception>
        Task<Servizio> AddPhotoToGalleryAsync(Guid servizioId, Guid fotoId, string author);

        /// <summary>
        /// Rimuove una foto dalla galleria di un servizio.
        /// </summary>
        /// <param name="servizioId">L'ID del servizio.</param>
        /// <param name="fotoId">L'ID della foto da rimuovere.</param>
        /// <param name="author">L'autore dell'operazione.</param>
        /// <returns>Il servizio aggiornato.</returns>
        /// <exception cref="KeyNotFoundException">Se il servizio non viene trovato.</exception>
        Task<Servizio> RemovePhotoFromGalleryAsync(Guid servizioId, Guid fotoId, string author);
    }


    /// <summary>
    /// Implementazione del servizio per la gestione dei servizi
    /// </summary>
    public class ServizioService : IServizioService
    {
        private readonly AppDbContext _context;

        public ServizioService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Ottiene tutti i servizi
        /// </summary>
        /// <param name="includeHidden">Se true, include anche i servizi nascosti</param>
        /// <returns>Lista di servizi</returns>
        public async Task<IEnumerable<Servizio>> GetAllAsync(bool includeHidden = false)
        {
            IQueryable<Servizio> query = _context.Servizi;
            
            if (!includeHidden)
                query = query.Where(s => s.Visible);
                
            return await query.OrderBy(s => s.Titolo).ToListAsync();
        }

        /// <summary>
        /// Ottiene un servizio tramite ID
        /// </summary>
        /// <param name="id">ID del servizio</param>
        /// <returns>Servizio o null</returns>
        public async Task<Servizio?> GetByIdAsync(Guid id)
        {
            return await _context.Servizi.FindAsync(id);
        }

        /// <summary>
        /// Crea un nuovo servizio
        /// </summary>
        /// <param name="servizio">Dati del servizio</param>
        /// <param name="author">Autore</param>
        /// <returns>Servizio creato</returns>
        public async Task<Servizio> CreateAsync(Servizio servizio, string author)
        {
            // Crea un nuovo ID per il servizio se non è già stato impostato
            if (servizio.ID == Guid.Empty)
                servizio.ID = Guid.NewGuid();

            // Imposta i campi obbligatori
            servizio.LastUpdate = DateTime.Now;
            servizio.Author = author;

            // Salva il servizio
            await _context.Servizi.AddAsync(servizio);
            await _context.SaveChangesAsync();

            return servizio;
        }

        /// <summary>
        /// Aggiorna un servizio esistente
        /// </summary>
        /// <param name="servizio">Servizio da aggiornare</param>
        /// <param name="author">Autore</param>
        /// <returns>Servizio aggiornato</returns>
        public async Task<Servizio> UpdateAsync(Servizio servizio, string author)
        {
            var servizioToUpdate = await _context.Servizi.FindAsync(servizio.ID)
                ?? throw new KeyNotFoundException("Servizio non trovato");

            // Aggiorna i campi del servizio
            servizioToUpdate.Titolo = servizio.Titolo;
            servizioToUpdate.Descrizione = servizio.Descrizione;
            servizioToUpdate.Visible = servizio.Visible;
            servizioToUpdate.GalleriaFoto = servizio.GalleriaFoto;
            servizioToUpdate.LastUpdate = DateTime.Now;
            servizioToUpdate.Author = author;

            // Salva le modifiche
            _context.Servizi.Update(servizioToUpdate);
            await _context.SaveChangesAsync();

            return servizioToUpdate;
        }

        /// <summary>
        /// Elimina un servizio
        /// </summary>
        /// <param name="id">ID del servizio</param>
        /// <returns>True se eliminato con successo</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var servizio = await _context.Servizi.FindAsync(id);
            if (servizio == null)
                return false;

            _context.Servizi.Remove(servizio);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Aggiunge una foto alla galleria del servizio
        /// </summary>
        /// <param name="servizioId">ID del servizio</param>
        /// <param name="fotoId">ID della foto</param>
        /// <param name="author">Autore</param>
        /// <returns>Servizio aggiornato</returns>
        public async Task<Servizio> AddPhotoToGalleryAsync(Guid servizioId, Guid fotoId, string author)
        {
            var servizio = await _context.Servizi.FindAsync(servizioId)
                ?? throw new KeyNotFoundException("Servizio non trovato");
                
            var foto = await _context.Foto.FindAsync(fotoId)
                ?? throw new KeyNotFoundException("Foto non trovata");

            // Inizializza la galleria se null
            servizio.GalleriaFoto ??= new List<Guid>();
            
            // Aggiungi la foto se non è già presente
            if (!servizio.GalleriaFoto.Contains(fotoId))
            {
                servizio.GalleriaFoto.Add(fotoId);
                servizio.LastUpdate = DateTime.Now;
                servizio.Author = author;
                
                await _context.SaveChangesAsync();
            }

            return servizio;
        }

        /// <summary>
        /// Rimuove una foto dalla galleria del servizio
        /// </summary>
        /// <param name="servizioId">ID del servizio</param>
        /// <param name="fotoId">ID della foto</param>
        /// <param name="author">Autore</param>
        /// <returns>Servizio aggiornato</returns>
        public async Task<Servizio> RemovePhotoFromGalleryAsync(Guid servizioId, Guid fotoId, string author)
        {
            var servizio = await _context.Servizi.FindAsync(servizioId)
                ?? throw new KeyNotFoundException("Servizio non trovato");

            // Rimuovi la foto se presente
            if (servizio.GalleriaFoto != null && servizio.GalleriaFoto.Contains(fotoId))
            {
                servizio.GalleriaFoto.Remove(fotoId);
                servizio.LastUpdate = DateTime.Now;
                servizio.Author = author;
                
                await _context.SaveChangesAsync();
            }

            return servizio;
        }
    }
}
