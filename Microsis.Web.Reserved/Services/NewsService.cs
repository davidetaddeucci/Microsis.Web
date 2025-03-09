using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsis.Names.Models;

namespace Microsis.Web.Reserved.Services
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;
        private readonly IApiConfigService _apiConfigService;

        public NewsService(HttpClient httpClient, IApiConfigService apiConfigService)
        {
            _httpClient = httpClient;
            _apiConfigService = apiConfigService;
        }

        public async Task<List<News>> GetAllNewsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiConfigService.GetApiEndpoint("news"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<News>>() ?? new List<News>();
                }
                return new List<News>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel recupero delle news: {ex.Message}");
                return new List<News>();
            }
        }

        public async Task<News?> GetNewsByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiConfigService.GetApiEndpoint($"news/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<News>();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel recupero della news {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateNewsAsync(News news)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(news),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync(_apiConfigService.GetApiEndpoint("news"), jsonContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nella creazione della news: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateNewsAsync(News news)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(news),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync(_apiConfigService.GetApiEndpoint($"news/{news.ID}"), jsonContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nell'aggiornamento della news {news.ID}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteNewsAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(_apiConfigService.GetApiEndpoint($"news/{id}"));
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nell'eliminazione della news {id}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ToggleNewsStatusAsync(Guid id, bool isActive)
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

                var response = await _httpClient.PatchAsync(_apiConfigService.GetApiEndpoint($"news/{id}/status"), jsonContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel cambio di stato della news {id}: {ex.Message}");
                return false;
            }
        }

        public async Task<List<News>> GetLatestNewsAsync(int count = 5)
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiConfigService.GetApiEndpoint($"news/latest?count={count}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<News>>() ?? new List<News>();
                }
                return new List<News>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel recupero delle ultime news: {ex.Message}");
                return new List<News>();
            }
        }
    }
}
