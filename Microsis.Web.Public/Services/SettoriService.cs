using Microsis.Names.Models;

namespace Microsis.Web.Public.Services
{
    public interface ISettoriService
    {
        Task<IEnumerable<Settore>> GetLastestAsync(int num_records_to_retrieve = 3);
        Task<IEnumerable<Settore>> GetAllASync(bool includeHidden = false);
        Task<Settore> GetByIDAsync(Guid ID);
    }
    public class SettoriService : ISettoriService
    {
        private readonly HttpClient _httpClient;
        private readonly IApiConfigService _apiConfigService;
        private readonly ILogger<NewsPublicService> _logger;

        public SettoriService(
            HttpClient httpClient,
            IApiConfigService apiConfigService,
            ILogger<NewsPublicService> logger)
        {
            _httpClient = httpClient;
            _apiConfigService = apiConfigService;
            _logger = logger;
        }

        public async Task<IEnumerable<Settore>> GetLastestAsync(int num_records_to_retrieve = 3)
        {
            try
            {
                var url = _apiConfigService.GetUrl("api/settori/latest");

                url += "?num_records_to_retrieve=" + num_records_to_retrieve;


                var response = await _httpClient.GetFromJsonAsync<IEnumerable<Settore>>(url);
                return response ?? Enumerable.Empty<Settore>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle news");
                return Enumerable.Empty<Settore>();
            }
        }
        public async Task<IEnumerable<Settore>> GetAllASync(bool includeHidden = false)
        {
            try
            {
                var url = _apiConfigService.GetUrl("api/settori/latest");

                url += "?includeHidden=" + includeHidden;


                var response = await _httpClient.GetFromJsonAsync<IEnumerable<Settore>>(url);
                return response ?? Enumerable.Empty<Settore>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle news");
                return Enumerable.Empty<Settore>();
            }
        }
        public async Task<Settore> GetByIDAsync(Guid ID)
        {
            try
            {
                var url = _apiConfigService.GetUrl("api/settori/latest");
                return await _httpClient.GetFromJsonAsync<Settore>(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle news");
                return null;
            }
        }
    }
}