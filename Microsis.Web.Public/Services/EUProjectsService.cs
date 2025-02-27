using Microsis.Names.Models;

namespace Microsis.Web.Public.Services
{
    public interface IEUProjectsService
    {
        Task<IEnumerable<ProgettoUE>> GetLastest(int num_records_to_retrieve = 3);
        Task<IEnumerable<ProgettoUE>> GetAllASync(bool includeHidden = false);
        Task<ProgettoUE> GetByIDAsync(Guid ID);
    }
    public class EUProjectsService : IEUProjectsService
    {
        private readonly HttpClient _httpClient;
        private readonly IApiConfigService _apiConfigService;
        private readonly ILogger<NewsPublicService> _logger;

        public EUProjectsService(
            HttpClient httpClient,
            IApiConfigService apiConfigService,
            ILogger<NewsPublicService> logger)
        {
            _httpClient = httpClient;
            _apiConfigService = apiConfigService;
            _logger = logger;
        }

        public async Task<IEnumerable<ProgettoUE>> GetLastest(int num_records_to_retrieve = 3)
        {
            try
            {
                var url = _apiConfigService.GetUrl("api/progettieu/latest");

                url += "?num_records_to_retrieve=" + num_records_to_retrieve;


                var response = await _httpClient.GetFromJsonAsync<IEnumerable<ProgettoUE>>(url);
                return response ?? Enumerable.Empty<ProgettoUE>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle news");
                return Enumerable.Empty<ProgettoUE>();
            }
        }
        public async Task<IEnumerable<ProgettoUE>> GetAllASync(bool includeHidden = false)
        {
            try
            {
                var url = _apiConfigService.GetUrl("api/progettieu/latest");

                url += "?includeHidden=" + includeHidden;


                var response = await _httpClient.GetFromJsonAsync<IEnumerable<ProgettoUE>>(url);
                return response ?? Enumerable.Empty<ProgettoUE>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle news");
                return Enumerable.Empty<ProgettoUE>();
            }
        }
        public async Task<ProgettoUE> GetByIDAsync(Guid ID)
        {
            try
            {
                var url = _apiConfigService.GetUrl("api/progettieu/latest");
                return await _httpClient.GetFromJsonAsync< ProgettoUE>(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle news");
                return null;
            }
        }
    }
}
