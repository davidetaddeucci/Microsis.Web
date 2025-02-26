using Microsoft.EntityFrameworkCore;
using Microsis.Names.Models;
using Microsis.Web.Services.Data;

namespace Microsis.Web.Services.Services
{
    /// <summary>
    /// Implementazione del servizio per la gestione dei banner
    /// </summary>
    public class BannerService : IBannerService
    {
        private readonly AppDbContext _context;

        public BannerService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Ottiene tutti i banner
        /// </summary>
        /// <param name="includeHidden">Se true, include anche i banner nascosti</param>
        /// <returns>Lista di banner</returns>
        public async Task<IEnumerable<Banner>> GetAllAsync(bool includeHidden = false)
        {
            IQueryable<Banner> query = _context.Banners;
            
            if (!includeHidden)
                query = query.Where(b => b.Visible);
                
            return await query.OrderBy(b => b.Order).ToListAsync();
        }
        
        /// <summary>
        /// Ottiene i banner visibili ordinati per campo Order
        /// </summary>
        /// <returns>Lista di banner visibili ordinati</returns>
        public async Task<IEnumerable<Banner>> GetVisibleOrderedAsync()
        {
            return await _context.Banners
                .Where(b => b.Visible)
                .OrderBy(b => b.Order)
                .ToListAsync();
        }

        /// <summary>
        /// Ottiene un banner tramite ID
        /// </summary>
        /// <param name="id">ID del banner</param>
        /// <returns>Banner o null</returns>
        public async Task<Banner?> GetByIdAsync(Guid id)
        {
            return await _context.Banners.FindAsync(id);
        }

        /// <summary>
        /// Crea un nuovo banner
        /// </summary>
        /// <param name="banner">Dati del banner</param>
        /// <param name="author">Autore</param>
        /// <returns>Banner creato</returns>
        public async Task<Banner> CreateAsync(Banner banner, string author)
        {
            // Crea un nuovo ID per il banner se non è già stato impostato
            if (banner.ID == Guid.Empty)
                banner.ID = Guid.NewGuid();

            // Imposta i campi obbligatori
            banner.UpdateDate = DateTime.Now;
            banner.Author = author;
            
            // Se l'ordine non è specificato, mettilo in fondo alla lista
            if (banner.Order == 0)
            {
                var maxOrder = await _context.Banners.MaxAsync(b => (int?)b.Order) ?? 0;
                banner.Order = maxOrder + 1;
            }

            // Salva il banner
            await _context.Banners.AddAsync(banner);
            await _context.SaveChangesAsync();

            return banner;
        }

        /// <summary>
        /// Aggiorna un banner esistente
        /// </summary>
        /// <param name="banner">Banner da aggiornare</param>
        /// <param name="author">Autore</param>
        /// <returns>Banner aggiornato</returns>
        public async Task<Banner> UpdateAsync(Banner banner, string author)
        {
            var bannerToUpdate = await _context.Banners.FindAsync(banner.ID)
                ?? throw new KeyNotFoundException("Banner non trovato");

            // Aggiorna i campi del banner
            bannerToUpdate.Path = banner.Path;
            bannerToUpdate.Filename = banner.Filename;
            bannerToUpdate.Title = banner.Title;
            bannerToUpdate.Subtitle = banner.Subtitle;
            bannerToUpdate.Visible = banner.Visible;
            bannerToUpdate.Order = banner.Order;
            bannerToUpdate.UpdateDate = DateTime.Now;
            bannerToUpdate.Author = author;

            // Salva le modifiche
            _context.Banners.Update(bannerToUpdate);
            await _context.SaveChangesAsync();

            return bannerToUpdate;
        }

        /// <summary>
        /// Elimina un banner
        /// </summary>
        /// <param name="id">ID del banner</param>
        /// <returns>True se eliminato con successo</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner == null)
                return false;

            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
