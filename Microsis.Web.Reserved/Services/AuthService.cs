using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsis.Names.Models;

namespace Microsis.Web.Reserved.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        
        public AuthService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
        }

        public async Task<bool> Login(string email, string password)
        {
            try
            {
                var loginModel = new
                {
                    Email = email,
                    Password = password
                };

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(loginModel),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("api/auth/login", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();
                    if (loginResult != null && !string.IsNullOrEmpty(loginResult.Token))
                    {
                        // Salva il token nel local storage
                        await ((CustomAuthStateProvider)_authStateProvider).MarkUserAsAuthenticated(loginResult.Token);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception)
            {
                // Log dell'errore
                return false;
            }
        }

        public async Task Logout()
        {
            await ((CustomAuthStateProvider)_authStateProvider).MarkUserAsLoggedOut();
        }

        public async Task<User?> GetCurrentUser()
        {
            try
            {
                var authState = await _authStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                if (user.Identity?.IsAuthenticated == true)
                {
                    var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
                    if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                    {
                        var response = await _httpClient.GetAsync($"api/auth/user/{userId}");
                        if (response.IsSuccessStatusCode)
                        {
                            return await response.Content.ReadFromJsonAsync<User>();
                        }
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }

    // Modello per il risultato del login
    public class LoginResult
    {
        public string Token { get; set; } = "";
        public User? User { get; set; }
    }
}
