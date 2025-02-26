# Microsis.Web Solution

Soluzione completa per il sito web Microsis, contenente sia il sito pubblico che l'area amministrativa riservata, con un'architettura basata su API e modelli condivisi.

## Progetti inclusi

La soluzione è composta da quattro progetti principali:

### Microsis.Names

Libreria .NET 9.0 che contiene tutti i modelli di dati condivisi tra i diversi progetti della soluzione, garantendo consistenza e riutilizzo del codice.

- Modelli dati per tutte le entità del sistema
- Classi condivise tra tutti i progetti

### Microsis.Web.Services

API RESTful che funge da backend per tutti i progetti, gestendo l'accesso ai dati e l'autenticazione.

- API per l'autenticazione JWT
- Endpoint per la gestione di progetti, servizi, news e foto
- Interfaccia programmatica per il sito pubblico e l'area riservata

### Microsis.Web.Public

Sito web pubblico implementato con Blazor Web .NET 9.0 e controlli Syncfusion v28.1.33.

- Homepage con sezioni informative
- Pagine di contenuto pubblico
- Showcase di progetti e servizi
- News e aggiornamenti

### Microsis.Web.Reserved

Area amministrativa riservata per la gestione dei contenuti del sito pubblico.

- Gestione gallerie fotografiche
- Pubblicazione e modifica di nuovi progetti
- Gestione offerte di lavoro e ricerca collaboratori
- Dashboard amministrativa

## Requisiti di sistema

- Visual Studio 2022 (v17.8 o superiore)
- .NET 9.0 SDK
- Syncfusion Blazor v28.1.33
- SQL Server 2022
- Licenze valide per Syncfusion e Telerik UI

## Configurazione dell'ambiente di sviluppo

1. Clonare il repository
2. Aprire la soluzione in Visual Studio 2022
3. Configurare le connessioni al database negli appsettings.json di Microsis.Web.Services
4. Eseguire le migrazioni del database (se necessario)
5. Avviare tutti i progetti in modalità multipla (Right-click sulla soluzione > Properties > Multiple Startup Projects)

## Flusso di navigazione

- Gli utenti normali accedono al sito pubblico (Microsis.Web.Public)
- Gli amministratori possono accedere all'area riservata tramite il pulsante LOGIN nel menu principale
- L'autenticazione avviene tramite Microsis.Web.Services che rilascia token JWT

## Struttura della soluzione

```
Microsis.Web/
├── Microsis.Names/            # Modelli dati condivisi
├── Microsis.Web.Services/     # Backend API e servizi
├── Microsis.Web.Public/       # Sito web pubblico
├── Microsis.Web.Reserved/     # Area amministrativa
└── Solution Items/            # File comuni alla soluzione
    ├── .gitignore
    ├── README.md
    └── docker-compose.yml
```

## Note di sviluppo

- Utilizzare EF Core per l'accesso ai dati tramite Microsis.Web.Services
- Implementare l'autenticazione JWT tramite Microsis.Web.Services
- Seguire i principi di Clean Architecture
- Implementare test unitari per la logica di business
