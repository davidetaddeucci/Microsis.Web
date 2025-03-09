using Microsis.Names.Models;

namespace Microsis.Web.Reserved.Services
{
    public interface IServizioService
    {
        Task<List<Servizio>> GetAllServiziAsync();
        Task<Servizio?> GetServizioByIdAsync(Guid id);
        Task<bool> CreateServizioAsync(Servizio servizio);
        Task<bool> UpdateServizioAsync(Servizio servizio);
        Task<bool> DeleteServizioAsync(Guid id);
        Task<bool> ToggleServizioStatusAsync(Guid id, bool isActive);
        Task<List<Settore>> GetAllSettoriAsync();
    }
}
