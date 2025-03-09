using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsis.Names.Models;

namespace Microsis.Web.Reserved.Services
{
    public class FotoService : IFotoService
    {
        private readonly HttpClient _httpClient;
        private readonly IApiConfigService _apiConfigService;

        public FotoService(HttpClient httpClient, IApiConfigService apiConfigService)
        {
            _httpClient = httpClient;
            _apiConfigService = apiConfigService;
        }

        public async Task<List<Foto>> GetAllFotoAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiConfigService.GetApiEndpoint("foto"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Foto>>() ?? new List<Foto>();
                }
                return new List<Foto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel recupero delle foto: {ex.Message}");
                return new List<Foto>();
            }
        }

        public async Task<List<Foto>> GetFotoByEntityAsync(Guid entityId)
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiConfigService.GetApiEndpoint($"foto/entity/{entityId}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Foto>>() ?? new List<Foto>();
                }
                return new List<Foto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel recupero delle foto per l'entità {entityId}: {ex.Message}");
                return new List<Foto>();
            }
        }

        public async Task<Foto?> GetFotoByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiConfigService.GetApiEndpoint($"foto/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Foto>();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel recupero della foto {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateFotoAsync(Foto foto, Stream fileStream, string fileName)
        {
            try
            {
                using var content = new MultipartFormDataContent();
                
                // Aggiungi file
                var fileContent = new StreamContent(fileStream);
                content.Add(fileContent, "file", fileName);
                
                // Aggiungi metadati
                content.Add(new StringContent(foto.Title), "title");
                content.Add(new StringContent(foto.Description), "description");
                content.Add(new StringContent(foto.EntityID.ToString()), "entityId");
                content.Add(new StringContent(foto.EntityType), "entityType");
                content.Add(new StringContent(foto.IsAttiva.ToString()), "isAttiva");

                var response = await _httpClient.PostAsync(_apiConfigService.GetApiEndpoint("foto"), content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel caricamento della foto: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateFotoAsync(Foto foto)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(foto),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync(_apiConfigService.GetApiEndpoint($"foto/{foto.ID}"), jsonContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nell'aggiornamento della foto {foto.ID}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteFotoAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(_apiConfigService.GetApiEndpoint($"foto/{id}"));
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nell'eliminazione della foto {id}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ToggleFotoStatusAsync(Guid id, bool isActive)
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

                var response = await _httpClient.PatchAsync(_apiConfigService.GetApiEndpoint($"foto/{id}/status"), jsonContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel cambio di stato della foto {id}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ReorderFotoAsync(List<Guid> fotoIds)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(new { FotoIds = fotoIds }),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync(_apiConfigService.GetApiEndpoint("foto/reorder"), jsonContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel riordinamento delle foto: {ex.Message}");
                return false;
            }
        }
    }
}
