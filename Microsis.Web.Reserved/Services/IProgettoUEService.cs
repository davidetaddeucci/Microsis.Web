using Microsis.Names.Models;

namespace Microsis.Web.Reserved.Services
{
    public interface IProgettoUEService
    {
        Task<List<ProgettoUE>> GetAllProgettiAsync();
        Task<ProgettoUE?> GetProgettoByIdAsync(Guid id);
        Task<bool> CreateProgettoAsync(ProgettoUE progetto);
        Task<bool> UpdateProgettoAsync(ProgettoUE progetto);
        Task<bool> DeleteProgettoAsync(Guid id);
        Task<bool> ToggleProgettoStatusAsync(Guid id, bool isActive);
    }
}
