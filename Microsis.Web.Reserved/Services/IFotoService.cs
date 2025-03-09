using Microsis.Names.Models;

namespace Microsis.Web.Reserved.Services
{
    public interface IFotoService
    {
        Task<List<Foto>> GetAllFotoAsync();
        Task<List<Foto>> GetFotoByEntityAsync(Guid entityId);
        Task<Foto?> GetFotoByIdAsync(Guid id);
        Task<bool> CreateFotoAsync(Foto foto, Stream fileStream, string fileName);
        Task<bool> UpdateFotoAsync(Foto foto);
        Task<bool> DeleteFotoAsync(Guid id);
        Task<bool> ToggleFotoStatusAsync(Guid id, bool isActive);
        Task<bool> ReorderFotoAsync(List<Guid> fotoIds);
    }
}
