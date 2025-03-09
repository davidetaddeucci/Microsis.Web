using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsis.Names.Models;

namespace Microsis.Web.Reserved.Services
{
    public class ProgettoUEService : IProgettoUEService
    {
        private readonly HttpClient _httpClient;
        private readonly IApiConfigService _apiConfigService;

        public ProgettoUEService(HttpClient httpClient, IApiConfigService apiConfigService)
        {
            _httpClient = httpClient;
            _apiConfigService = apiConfigService;
        }

        public async Task<List<ProgettoUE>> GetAllProgettiAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiConfigService.GetApiEndpoint("progettiue"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ProgettoUE>>() ?? new List<ProgettoUE>();
                }
                return new List<ProgettoUE>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel recupero dei progetti UE: {ex.Message}");
                return new List<ProgettoUE>();
            }
        }

        public async Task<ProgettoUE?> GetProgettoByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiConfigService.GetApiEndpoint($"progettiue/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ProgettoUE>();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel recupero del progetto UE {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateProgettoAsync(ProgettoUE progetto)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(progetto),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync(_apiConfigService.GetApiEndpoint("progettiue"), jsonContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nella creazione del progetto UE: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateProgettoAsync(ProgettoUE progetto)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(progetto),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync(_apiConfigService.GetApiEndpoint($"progettiue/{progetto.ID}"), jsonContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nell'aggiornamento del progetto UE {progetto.ID}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteProgettoAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(_apiConfigService.GetApiEndpoint($"progettiue/{id}"));
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nell'eliminazione del progetto UE {id}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ToggleProgettoStatusAsync(Guid id, bool isActive)
        {
            try
            {
                var toggleModel = new
                {
                    IsActive = isActive
                };

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(toggleModel),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PatchAsync(_apiConfigService.GetApiEndpoint($"progettiue/{id}/status"), jsonContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel cambio di stato del progetto UE {id}: {ex.Message}");
                return false;
            }
        }
    }
}
