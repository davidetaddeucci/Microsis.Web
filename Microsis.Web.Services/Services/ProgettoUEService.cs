using Microsoft.EntityFrameworkCore;
using Microsis.Names.Models;
using Microsis.Web.Services.Data;

namespace Microsis.Web.Services.Services
{
    /// <summary>
    /// Interfaccia per il servizio di gestione dei progetti UE.
    /// </summary>
    public interface IProgettoUEService
    {
        /// <summary>
        /// Ottiene tutti i progetti UE.
        /// </summary>
        /// <param name="includeHidden">Se true, include anche i progetti nascosti.</param>
        /// <returns>Una lista di progetti UE.</returns>
        Task<IEnumerable<ProgettoUE>> GetAllAsync(bool includeHidden = false);

        /// <summary>
        /// Ottiene un progetto UE tramite il suo ID.
        /// </summary>
        /// <param name="id">L'ID del progetto.</param>
        /// <returns>Il progetto UE, o null se non trovato.</returns>
        Task<ProgettoUE?> GetByIdAsync(Guid id);

        /// <summary>
        /// Crea un nuovo progetto UE.
        /// </summary>
        /// <param name="progetto">I dati del progetto da creare.</param>
        /// <param name="author">L'autore del progetto.</param>
        /// <returns>Il progetto UE creato.</returns>
        Task<ProgettoUE> CreateAsync(ProgettoUE progetto, string author);

        /// <summary>
        /// Aggiorna un progetto UE esistente.
        /// </summary>
        /// <param name="progetto">Il progetto UE con i dati aggiornati.</param>
        /// <param name="author">L'autore che sta effettuando l'aggiornamento.</param>
        /// <returns>Il progetto UE aggiornato.</returns>
        /// <exception cref="KeyNotFoundException">Se il progetto con l'ID specificato non viene trovato.</exception>
        Task<ProgettoUE> UpdateAsync(ProgettoUE progetto, string author);

        /// <summary>
        /// Elimina un progetto UE.
        /// </summary>
        /// <param name="id">L'ID del progetto da eliminare.</param>
        /// <returns>True se l'eliminazione è avvenuta con successo, false altrimenti.</returns>
        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<ProgettoUE>> GetLatestAsync(int num_records_to_retrieve);
    }
    /// <summary>
    /// Implementazione del servizio per la gestione dei progetti UE
    /// </summary>
    public class ProgettoUEService : IProgettoUEService
    {
        private readonly AppDbContext _context;

        public ProgettoUEService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProgettoUE>> GetLatestAsync(int num_records_to_retrieve=3)
        {
            try
            {
                IQueryable<ProgettoUE> query = _context.ProgettiUE;

                return await query.OrderByDescending(p => p.LastUpdate).Take(num_records_to_retrieve).ToListAsync();
            }
            catch(Exception ecc)
            {
                return null;
            }
        }

        /// <summary>
        /// Ottiene tutti i progetti UE
        /// </summary>
        /// <param name="includeHidden">Se true, include anche i progetti nascosti</param>
        /// <returns>Lista di progetti UE</returns>
        public async Task<IEnumerable<ProgettoUE>> GetAllAsync(bool includeHidden = false)
        {
            IQueryable<ProgettoUE> query = _context.ProgettiUE;
            
            if (!includeHidden)
                query = query.Where(p => p.Visible);
                
            return await query.OrderByDescending(p => p.LastUpdate).ToListAsync();
        }

        /// <summary>
        /// Ottiene un progetto UE tramite ID
        /// </summary>
        /// <param name="id">ID del progetto</param>
        /// <returns>Progetto UE o null</returns>
        public async Task<ProgettoUE?> GetByIdAsync(Guid id)
        {
            return await _context.ProgettiUE.FindAsync(id);
        }

        /// <summary>
        /// Crea un nuovo progetto UE
        /// </summary>
        /// <param name="progetto">Dati del progetto</param>
        /// <param name="author">Autore</param>
        /// <returns>Progetto creato</returns>
        public async Task<ProgettoUE> CreateAsync(ProgettoUE progetto, string author)
        {
            // Crea un nuovo ID per il progetto se non è già stato impostato
            if (progetto.ID == Guid.Empty)
                progetto.ID = Guid.NewGuid();

            // Imposta i campi obbligatori
            progetto.LastUpdate = DateTime.Now;
            progetto.Author = author;

            // Salva il progetto
            await _context.ProgettiUE.AddAsync(progetto);
            await _context.SaveChangesAsync();

            return progetto;
        }

        /// <summary>
        /// Aggiorna un progetto UE esistente
        /// </summary>
        /// <param name="progetto">Progetto da aggiornare</param>
        /// <param name="author">Autore</param>
        /// <returns>Progetto aggiornato</returns>
        public async Task<ProgettoUE> UpdateAsync(ProgettoUE progetto, string author)
        {
            var progettoToUpdate = await _context.ProgettiUE.FindAsync(progetto.ID)
                ?? throw new KeyNotFoundException("Progetto non trovato");

            // Aggiorna i campi del progetto
            progettoToUpdate.Titolo = progetto.Titolo;
            progettoToUpdate.Abstract = progetto.Abstract;
            progettoToUpdate.EntiCoinvolti = progetto.EntiCoinvolti;
            progettoToUpdate.Tab_Name = progetto.Tab_Name;
            progettoToUpdate.Visible = progetto.Visible;
            progettoToUpdate.ImagePath = progetto.ImagePath;
            progettoToUpdate.LastUpdate = DateTime.Now;
            progettoToUpdate.Author = author;

            // Salva le modifiche
            _context.ProgettiUE.Update(progettoToUpdate);
            await _context.SaveChangesAsync();

            return progettoToUpdate;
        }

        /// <summary>
        /// Elimina un progetto UE
        /// </summary>
        /// <param name="id">ID del progetto</param>
        /// <returns>True se eliminato con successo</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var progetto = await _context.ProgettiUE.FindAsync(id);
            if (progetto == null)
                return false;

            _context.ProgettiUE.Remove(progetto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
