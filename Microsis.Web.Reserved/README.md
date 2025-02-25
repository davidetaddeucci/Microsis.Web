# Microsis.Web.Reserved

Area amministrativa riservata per la gestione dei contenuti del sito Microsis.Web.Public. Questo progetto fornisce un'interfaccia di amministrazione per gestire gallerie fotografiche, progetti, news e opportunità di lavoro.

## Funzionalità

- **Autenticazione e Autorizzazione**: Sistema di login sicuro con ruoli amministrativi
- **Gestione Gallerie**: Caricamento e organizzazione di immagini in gallerie tematiche
- **Gestione Progetti**: Creazione e modifica di schede progetti da mostrare nel sito pubblico
- **Gestione News**: Pubblicazione di notizie, eventi e aggiornamenti
- **Gestione Carriere**: Pubblicazione di opportunità di lavoro e posizioni aperte

## Tecnologie

- .NET 9.0
- Blazor Server
- Entity Framework Core
- Syncfusion Blazor v28.1.33
- SQL Server
- ASP.NET Core Identity

## Struttura del progetto

Il progetto è organizzato seguendo i principi della Clean Architecture:

- **Components**: Componenti Blazor per l'interfaccia utente
- **Models**: Classi che rappresentano le entità di dominio
- **Data**: DbContext, repository e migrazioni per l'accesso ai dati
- **Services**: Logica di business e servizi applicativi

## Requisiti di sistema

- Visual Studio 2022 (v17.8 o superiore)
- .NET 9.0 SDK
- SQL Server (locale o remoto)
- Licenze valide per Syncfusion Blazor e Telerik UI

## Configurazione iniziale

1. Aggiorna la stringa di connessione in `appsettings.json`
2. Esegui le migrazioni del database: `dotnet ef database update`
3. Configura le impostazioni di autenticazione
4. Imposta le directory per il caricamento dei file

## Integrazione con il sito pubblico

- L'area riservata gestisce i contenuti visualizzati nel sito pubblico
- Le modifiche effettuate qui si riflettono automaticamente nel sito pubblico
- È possibile configurare la pubblicazione programmata dei contenuti

## Sicurezza

- Implementa l'autenticazione a due fattori
- Utilizza roles e policy per l'autorizzazione
- Limita l'accesso in base alle responsabilità dell'utente
- Registra tutte le attività amministrative in log di audit

## Note per lo sviluppo

- Utilizza i componenti Syncfusion Grid per la visualizzazione e modifica dei dati
- Implementa il rich text editor per la modifica di contenuti formatati
- Utilizza il file manager per la gestione delle risorse multimediali
- Assicurati di validare tutti gli input lato client e server
