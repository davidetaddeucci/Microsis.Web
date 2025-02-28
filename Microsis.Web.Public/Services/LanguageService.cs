using Microsoft.JSInterop;

namespace Microsis.Web.Public.Services
{
    /// <summary>
    /// Service for managing language preferences
    /// </summary>
    public class LanguageService
    {
        private const string LocalStorageKey = "preferred_language";
        private readonly IJSRuntime _jsRuntime;
        private bool _isEnglish = false;
        
        // Event to notify components when language changes
        public event Action? OnLanguageChanged;

        public LanguageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Initialize language from local storage
        /// </summary>
        public async Task InitializeAsync()
        {
            try
            {
                var storedValue = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", LocalStorageKey);
                if (!string.IsNullOrEmpty(storedValue))
                {
                    _isEnglish = storedValue.Equals("en", StringComparison.OrdinalIgnoreCase);
                }
            }
            catch
            {
                // If local storage is not available, default to Italian
                _isEnglish = false;
            }
        }

        /// <summary>
        /// Check if English is the current language
        /// </summary>
        public bool IsEnglish => _isEnglish;

        /// <summary>
        /// Set language preference
        /// </summary>
        public async Task SetLanguageAsync(bool isEnglish)
        {
            if (_isEnglish != isEnglish)
            {
                _isEnglish = isEnglish;
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", LocalStorageKey, isEnglish ? "en" : "it");
                OnLanguageChanged?.Invoke();
            }
        }

        /// <summary>
        /// Toggle between languages
        /// </summary>
        public async Task ToggleLanguageAsync()
        {
            await SetLanguageAsync(!_isEnglish);
        }
    }
}
