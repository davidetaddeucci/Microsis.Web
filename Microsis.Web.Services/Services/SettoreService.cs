using Microsis.Names.Models;
using Microsis.Web.Services.Data;

namespace Microsis.Web.Services.Services
{
    public interface ISettoreService
    {
        /// <summary>
        /// Ottiene tutti i servizi.
        /// </summary>
        /// <param name="includeHidden">Se true, include anche i servizi nascosti.</param>
        /// <returns>Una lista di servizi.</returns>
        //Task<IEnumerable<Settore>> GetAllAsync(bool includeHidden = false);

        ///// <summary>
        ///// Ottiene un servizio tramite il suo ID.
        ///// </summary>
        ///// <param name="id">L'ID del servizio.</param>
        ///// <returns>Il servizio, o null se non trovato.</returns>
        //Task<Settore?> GetByIdAsync(Guid id);

        ///// <summary>
        ///// Crea un nuovo servizio.
        ///// </summary>
        ///// <param name="servizio">I dati del servizio da creare.</param>
        ///// <param name="author">L'autore del servizio.</param>
        ///// <returns>Il servizio creato.</returns>
        //Task<Settore> CreateAsync(Settore servizio, string author);

        ///// <summary>
        ///// Aggiorna un servizio esistente.
        ///// </summary>
        ///// <param name="servizio">Il servizio con i dati aggiornati.</param>
        ///// <param name="author">L'autore che sta effettuando l'aggiornamento.</param>
        ///// <returns>Il servizio aggiornato.</returns>
        ///// <exception cref="KeyNotFoundException">Se il servizio con l'ID specificato non viene trovato.</exception>
        //Task<Servizio> UpdateAsync(Settore servizio, string author);

        ///// <summary>
        ///// Elimina un servizio.
        ///// </summary>
        ///// <param name="id">L'ID del servizio da eliminare.</param>
        ///// <returns>True se l'eliminazione è avvenuta con successo, false altrimenti.</returns>
        //Task<bool> DeleteAsync(Guid settoreID);

        ///// <summary>
        ///// Aggiunge una foto alla galleria di un servizio.
        ///// </summary>
        ///// <param name="servizioId">L'ID del servizio.</param>
        ///// <param name="fotoId">L'ID della foto da aggiungere.</param>
        ///// <param name="author">L'autore dell'operazione.</param>
        ///// <returns>Il servizio aggiornato.</returns>
        ///// <exception cref="KeyNotFoundException">Se il servizio o la foto non vengono trovati.</exception>
        //Task<Servizio> AddPhotoToGalleryAsync(Guid settoreID, Guid fotoId, string author);

        ///// <summary>
        ///// Rimuove una foto dalla galleria di un servizio.
        ///// </summary>
        ///// <param name="servizioId">L'ID del servizio.</param>
        ///// <param name="fotoId">L'ID della foto da rimuovere.</param>
        ///// <param name="author">L'autore dell'operazione.</param>
        ///// <returns>Il servizio aggiornato.</returns>
        ///// <exception cref="KeyNotFoundException">Se il servizio non viene trovato.</exception>
        //Task<Servizio> RemovePhotoFromGalleryAsync(Guid settoreID, Guid fotoId, string author);
    }
    public class SettoreService: ISettoreService
    {
        private readonly AppDbContext _context;

        public SettoreService(AppDbContext context)
        {
            _context = context;
        }
    }
}
