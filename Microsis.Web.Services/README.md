# Microsis.Web.Services

API RESTful per la gestione centralizzata dei dati dell'applicazione Microsis, con autenticazione JWT e autorizzazione basata su ruoli.

## Caratteristiche

- **Autenticazione JWT**: Sistema di autenticazione sicuro basato su token JWT
- **API RESTful**: Interfaccia programmatica conforme alle best practices REST
- **Swagger/OpenAPI**: Documentazione interattiva delle API
- **Entity Framework Core**: ORM per l'accesso ai dati
- **SQL Server**: Database relazionale per la persistenza dei dati
- **CORS configurabile**: Supporto per richieste cross-origin dai client Blazor

## Endpoint principali

- `/api/auth`: Endpoints per autenticazione (login/register)
- `/api/progetti`: Gestione progetti UE
- `/api/servizi`: Gestione servizi offerti
- `/api/news`: Gestione news e aggiornamenti
- `/api/foto`: Gestione immagini e gallerie
- `/api/users`: Gestione utenti (solo per amministratori)

## Struttura del progetto

- **/Controllers**: Controller API per gestire le richieste HTTP
- **/Data**: Contesto del database e configurazioni EF Core
- **/Services**: Servizi di business logic per operazioni sui dati
- **/Helpers**: Classi di supporto e utilità

## Sicurezza

Il progetto implementa diversi livelli di sicurezza:

1. **Autenticazione JWT**: Tutti gli endpoint di scrittura richiedono un token JWT valido
2. **Autorizzazione basata su ruoli**: Alcuni endpoint sono accessibili solo a utenti con ruoli specifici
3. **HTTPS obbligatorio**: Tutte le comunicazioni sono crittografate
4. **Validazione dei dati**: Tutti i dati in input sono validati per prevenire attacchi

## Integrazione con altri progetti

- **Microsis.Names**: Utilizza i modelli dati definiti nella libreria condivisa
- **Microsis.Web.Public**: Fornisce dati in sola lettura al sito pubblico
- **Microsis.Web.Reserved**: Fornisce funzionalità complete di CRUD per l'area amministrativa

## Configurazione

Le impostazioni principali sono definite in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=MicrosisDB;..."
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY",
    "Issuer": "MicrosisAPI",
    "Audience": "MicrosisWebApps",
    "ExpireMinutes": 60
  },
  "AllowedOrigins": {
    "PublicSite": "https://localhost:5100",
    "ReservedSite": "https://localhost:5200"
  }
}
```
