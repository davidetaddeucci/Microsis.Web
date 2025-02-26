using Microsoft.EntityFrameworkCore;
using Microsis.Names.Models;
using Microsis.Web.Services.Data;

namespace Microsis.Web.Services.Services
{
    /// <summary>
    /// Implementazione del servizio per la gestione delle foto
    /// </summary>
    public class FotoService : IFotoService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public FotoService(
            AppDbContext context, 
            IWebHostEnvironment environment,
            IConfiguration configuration)
        {
            _context = context;
            _environment = environment;
            _configuration = configuration;
        }

        /// <summary>
        /// Ottiene tutte le foto
        /// </summary>
        /// <param name="includeHidden">Se true, include anche le foto nascoste</param>
        /// <returns>Lista di foto</returns>
        public async Task<IEnumerable<Foto>> GetAllAsync(bool includeHidden = false)
        {
            IQueryable<Foto> query = _context.Foto;
            
            if (!includeHidden)
                query = query.Where(f => f.Visible);
                
            return await query.OrderByDescending(f => f.LastUpdate).ToListAsync();
        }

        /// <summary>
        /// Ottiene una foto tramite ID
        /// </summary>
        /// <param name="id">ID della foto</param>
        /// <returns>Foto o null</returns>
        public async Task<Foto?> GetByIdAsync(Guid id)
        {
            return await _context.Foto.FindAsync(id);
        }

        /// <summary>
        /// Crea una nuova foto
        /// </summary>
        /// <param name="foto">Dati della foto</param>
        /// <param name="file">File della foto</param>
        /// <param name="author">Autore</param>
        /// <returns>Foto creata</returns>
        public async Task<Foto> CreateAsync(Foto foto, IFormFile file, string author)
        {
            // Crea un nuovo ID per la foto se non è già stato impostato
            if (foto.ID == Guid.Empty)
                foto.ID = Guid.NewGuid();

            // Ottiene il percorso della cartella uploads
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            
            // Crea la cartella se non esiste
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Genera un nome univoco per il file
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Salva il file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Imposta i campi della foto
            foto.Filename = file.FileName;
            foto.LocalPath = $"/uploads/{uniqueFileName}";
            foto.FileSize = file.Length;
            foto.LastUpdate = DateTime.Now;
            foto.Author = author;

            // Salva la foto nel database
            await _context.Foto.AddAsync(foto);
            await _context.SaveChangesAsync();

            return foto;
        }

        /// <summary>
        /// Aggiorna una foto esistente
        /// </summary>
        /// <param name="foto">Foto da aggiornare</param>
        /// <param name="author">Autore</param>
        /// <returns>Foto aggiornata</returns>
        public async Task<Foto> UpdateAsync(Foto foto, string author)
        {
            var fotoToUpdate = await _context.Foto.FindAsync(foto.ID)
                ?? throw new KeyNotFoundException("Foto non trovata");

            // Aggiorna i campi della foto (solo metadati, non il file)
            fotoToUpdate.Title = foto.Title;
            fotoToUpdate.Description = foto.Description;
            fotoToUpdate.Visible = foto.Visible;
            fotoToUpdate.LastUpdate = DateTime.Now;
            fotoToUpdate.Author = author;

            // Salva le modifiche
            _context.Foto.Update(fotoToUpdate);
            await _context.SaveChangesAsync();

            return fotoToUpdate;
        }

        /// <summary>
        /// Elimina una foto
        /// </summary>
        /// <param name="id">ID della foto</param>
        /// <returns>True se eliminata con successo</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var foto = await _context.Foto.FindAsync(id);
            if (foto == null)
                return false;

            // Rimuovi il file fisico
            var filePath = Path.Combine(_environment.WebRootPath, foto.LocalPath.TrimStart('/'));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Rimuovi dal database
            _context.Foto.Remove(foto);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Ottiene l'URL pubblico di una foto
        /// </summary>
        /// <param name="id">ID della foto</param>
        /// <returns>URL della foto</returns>
        public async Task<string> GetPhotoUrlAsync(Guid id)
        {
            var foto = await _context.Foto.FindAsync(id)
                ?? throw new KeyNotFoundException("Foto non trovata");

            var baseUrl = _configuration["AppSettings:BaseUrl"] ?? "https://localhost:7000";
            return $"{baseUrl}{foto.LocalPath}";
        }
    }
}
