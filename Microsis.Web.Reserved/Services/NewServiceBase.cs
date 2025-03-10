namespace Microsis.Web.Reserved.Services
{
    /// <summary>
    /// Interfaccia base per i servizi di NewService
    /// </summary>
    public interface INewServiceBase
    {
        Task<IEnumerable<object>> GetAllAsync();
        Task<object?> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(object entity);
        Task<bool> UpdateAsync(object entity);
        Task<bool> DeleteAsync(Guid id);
    }
    
    /// <summary>
    /// Classe base per i servizi di NewService
    /// </summary>
    public abstract class NewServiceBase : INewServiceBase
    {
        protected readonly HttpClient _httpClient;
        protected readonly IApiConfigService _apiConfigService;

        public NewServiceBase(HttpClient httpClient, IApiConfigService apiConfigService)
        {
            _httpClient = httpClient;
            _apiConfigService = apiConfigService;
        }

        public async Task<IEnumerable<object>> GetAllAsync()
        {
            return new List<object>();
        }

        public async Task<object?> GetByIdAsync(Guid id)
        {
            return null;
        }

        public async Task<bool> CreateAsync(object entity)
        {
            return false;
        }

        public async Task<bool> UpdateAsync(object entity)
        {
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return false;
        }
    }
}
