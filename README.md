# Microsis.Web Solution

Soluzione completa per il sito web Microsis, contenente sia il sito pubblico che l'area amministrativa riservata.

## Progetti inclusi

La soluzione è composta da due progetti principali:

### Microsis.Web.Public

Sito web pubblico implementato con Blazor Web .NET 9.0 e controlli Syncfusion v28.1.33.
Contiene:
- Homepage con sezioni informative
- Pagine di contenuto pubblico
- Showcase di progetti e servizi
- News e aggiornamenti

### Microsis.Web.Reserved

Area amministrativa riservata per la gestione dei contenuti del sito pubblico.
Funzionalità previste:
- Gestione gallerie fotografiche
- Pubblicazione e modifica di nuovi progetti
- Gestione offerte di lavoro e ricerca collaboratori
- Dashboard amministrativa

## Requisiti di sistema

- Visual Studio 2022 (v17.8 o superiore)
- .NET 9.0 SDK
- Syncfusion Blazor v28.1.33
- SQL Server (locale o remoto)
- Licenze valide per Syncfusion e Telerik UI

## Configurazione dell'ambiente di sviluppo

1. Clonare il repository
2. Aprire la soluzione in Visual Studio 2022
3. Configurare le connessioni al database negli appsettings.json
4. Eseguire le migrazioni del database (se necessario)
5. Compilare e avviare il progetto desiderato

## Flusso di navigazione

- Gli utenti normali accedono al sito pubblico (Microsis.Web.Public)
- Gli amministratori possono accedere all'area riservata tramite il pulsante LOGIN nel menu principale
- L'autenticazione avviene tramite form di login o Single Sign-On

## Struttura della soluzione

```
Microsis.Web/
├── Microsis.Web.Public/        # Sito web pubblico
├── Microsis.Web.Reserved/      # Area amministrativa
└── Solution Items/             # File comuni alla soluzione
    ├── .gitignore
    ├── README.md
    └── docker-compose.yml
```

## Note di sviluppo

- Utilizzare EF Core per l'accesso ai dati
- Implementare l'autenticazione tramite ASP.NET Core Identity
- Seguire i principi di Clean Architecture
- Implementare test unitari per la logica di business
