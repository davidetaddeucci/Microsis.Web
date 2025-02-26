using Microsis.Names.Models;

namespace Microsis.Web.Services.Services
{
    /// <summary>
    /// Interfaccia per il servizio di gestione dei progetti UE
    /// </summary>
    public interface IProgettoUEService
    {
        Task<IEnumerable<ProgettoUE>> GetAllAsync(bool includeHidden = false);
        Task<ProgettoUE?> GetByIdAsync(Guid id);
        Task<ProgettoUE> CreateAsync(ProgettoUE progetto, string author);
        Task<ProgettoUE> UpdateAsync(ProgettoUE progetto, string author);
        Task<bool> DeleteAsync(Guid id);
    }

    /// <summary>
    /// Interfaccia per il servizio di gestione dei servizi
    /// </summary>
    public interface IServizioService
    {
        Task<IEnumerable<Servizio>> GetAllAsync(bool includeHidden = false);
        Task<Servizio?> GetByIdAsync(Guid id);
        Task<Servizio> CreateAsync(Servizio servizio, string author);
        Task<Servizio> UpdateAsync(Servizio servizio, string author);
        Task<bool> DeleteAsync(Guid id);
        Task<Servizio> AddPhotoToGalleryAsync(Guid servizioId, Guid fotoId, string author);
        Task<Servizio> RemovePhotoFromGalleryAsync(Guid servizioId, Guid fotoId, string author);
    }

    /// <summary>
    /// Interfaccia per il servizio di gestione delle news
    /// </summary>
    public interface INewsService
    {
        Task<IEnumerable<News>> GetAllAsync(bool includeHidden = false);
        Task<News?> GetByIdAsync(Guid id);
        Task<News> CreateAsync(News news, string author);
        Task<News> UpdateAsync(News news, string author);
        Task<bool> DeleteAsync(Guid id);
        Task<News> AddPhotoToGalleryAsync(Guid newsId, Guid fotoId, string author);
        Task<News> RemovePhotoFromGalleryAsync(Guid newsId, Guid fotoId, string author);
    }

    /// <summary>
    /// Interfaccia per il servizio di gestione delle foto
    /// </summary>
    public interface IFotoService
    {
        Task<IEnumerable<Foto>> GetAllAsync(bool includeHidden = false);
        Task<Foto?> GetByIdAsync(Guid id);
        Task<Foto> CreateAsync(Foto foto, IFormFile file, string author);
        Task<Foto> UpdateAsync(Foto foto, string author);
        Task<bool> DeleteAsync(Guid id);
        Task<string> GetPhotoUrlAsync(Guid id);
    }
}
