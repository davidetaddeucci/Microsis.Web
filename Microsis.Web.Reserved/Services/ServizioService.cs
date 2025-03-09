using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsis.Names.Models;

namespace Microsis.Web.Reserved.Services
{
    public class ServizioService : IServizioService
    {
        private readonly HttpClient _httpClient;
        private readonly IApiConfigService _apiConfigService;

        public ServizioService(HttpClient httpClient, IApiConfigService apiConfigService)
        {
            _httpClient = httpClient;
            _apiConfigService = apiConfigService;
        }

        public async Task<List<Servizio>> GetAllServiziAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiConfigService.GetApiEndpoint("servizi"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Servizio>>() ?? new List<Servizio>();
                }
                return new List<Servizio>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel recupero dei servizi: {ex.Message}");
                return new List<Servizio>();
            }
        }

        public async Task<Servizio?> GetServizioByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiConfigService.GetApiEndpoint($"servizi/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Servizio>();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel recupero del servizio {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateServizioAsync(Servizio servizio)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(servizio),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync(_apiConfigService.GetApiEndpoint("servizi"), jsonContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nella creazione del servizio: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateServizioAsync(Servizio servizio)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(servizio),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync(_apiConfigService.GetApiEndpoint($"servizi/{servizio.ID}"), jsonContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nell'aggiornamento del servizio {servizio.ID}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteServizioAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(_apiConfigService.GetApiEndpoint($"servizi/{id}"));
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nell'eliminazione del servizio {id}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ToggleServizioStatusAsync(Guid id, bool isActive)
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

                var response = await _httpClient.PatchAsync(_apiConfigService.GetApiEndpoint($"servizi/{id}/status"), jsonContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel cambio di stato del servizio {id}: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Settore>> GetAllSettoriAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiConfigService.GetApiEndpoint("settori"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Settore>>() ?? new List<Settore>();
                }
                return new List<Settore>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel recupero dei settori: {ex.Message}");
                return new List<Settore>();
            }
        }
    }
}
