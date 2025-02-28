# Multilanguage Implementation for Microsis.Web

This document outlines the multilanguage implementation approach for the Microsis.Web application. The implementation supports both Italian (default) and English languages.

## Implementation Overview

### 1. Model Updates

All model classes have been updated to include English (_EN) versions of text fields. For example:

```csharp
// Italian field (original)
public string Titolo { get; set; } = string.Empty;

// English field (added)
public string Titolo_EN { get; set; } = string.Empty;
```

Classes that were updated:
- Banner
- News
- ProgettoUE
- Servizio
- Settore

### 2. Language Service

A new `LanguageService` class has been created to manage language selection:

```csharp
public class LanguageService
{
    private const string LocalStorageKey = "preferred_language";
    private readonly IJSRuntime _jsRuntime;
    private bool _isEnglish = false;
    
    // Event to notify components when language changes
    public event Action? OnLanguageChanged;

    public LanguageService(IJSRuntime jsRuntime) { ... }

    // Initialize language from local storage
    public async Task InitializeAsync() { ... }

    // Check if English is the current language
    public bool IsEnglish => _isEnglish;

    // Set language preference
    public async Task SetLanguageAsync(bool isEnglish) { ... }

    // Toggle between languages
    public async Task ToggleLanguageAsync() { ... }
}
```

Key features:
- Stores user language preference in localStorage
- Provides an event for notifying components of language changes
- Offers a simple API for toggling between languages

### 3. Language Selector UI Component

A new `LanguageSelector.razor` component has been created for the UI:

```html
<div class="language-selector">
    <button class="btn btn-sm @(LanguageService.IsEnglish ? "btn-outline-secondary" : "btn-primary")" @onclick="() => SwitchLanguage(false)" title="Italiano">
        <span class="flag-icon">🇮🇹</span> IT
    </button>
    <button class="btn btn-sm @(LanguageService.IsEnglish ? "btn-primary" : "btn-outline-secondary")" @onclick="() => SwitchLanguage(true)" title="English">
        <span class="flag-icon">🇬🇧</span> EN
    </button>
</div>
```

This component has been added to the navigation bar for easy access.

### 4. API Service Updates

All service interfaces and implementations have been updated to support language selection:

```csharp
// Interface methods now include a language parameter
Task<IEnumerable<Banner>> GetAllAsync(bool includeHidden = false, bool englishTranslation = false);
```

Service implementations handle the language parameter by:
1. Passing it to the API when making requests
2. If the API doesn't yet support the parameter, applying the translation client-side

### 5. UI Component Updates

Components have been updated to use the selected language:

```csharp
// Dynamic text based on language
@(LanguageService.IsEnglish ? "Back to news list" : "Torna all'elenco delle news")

// Dynamic content selection based on language
var content = LanguageService.IsEnglish && !string.IsNullOrEmpty(newsItem.Contenuto_EN) 
    ? newsItem.Contenuto_EN 
    : newsItem.Contenuto;
```

### 6. HTML Language Attribute

The `html` tag's `lang` attribute is dynamically set based on the selected language:

```html
<html lang="@(LanguageService.IsEnglish ? "en" : "it")">
```

## Usage Guidelines

### For Developers

1. When creating new models/classes with text fields, add corresponding `_EN` fields
2. When fetching data from services, pass the `LanguageService.IsEnglish` value to the `englishTranslation` parameter
3. In UI components, use the `LanguageService.IsEnglish` property to conditionally display text
4. Subscribe to `LanguageService.OnLanguageChanged` to update UI when language changes
5. Remember to unsubscribe from events in component disposal

### For Content Editors

1. Always fill in both Italian and English fields
2. If an English translation is not available, leave the field empty (the application will fall back to the Italian version)

## Future Improvements

1. Support for additional languages (by adding more fields, e.g., `_FR`, `_DE`)
2. Resource-based localization for static UI text
3. Localization middleware for server-side messages

## Database Considerations

The database schema has been updated to include the new English fields. When updating content, ensure both language versions are populated.
