# Microsis.Web Solution

Soluzione completa per il sito web Microsis, contenente sia il sito pubblico che l'area amministrativa riservata, con un'architettura basata su API e modelli condivisi.

## Progetti inclusi

La soluzione è composta da quattro progetti principali:

### Microsis.Names

Libreria .NET 9.0 che contiene tutti i modelli di dati condivisi tra i diversi progetti della soluzione, garantendo consistenza e riutilizzo del codice.

- Modelli dati per tutte le entità del sistema (ProgettoUE, Servizio, News, User, Foto, Banner)
- Classi condivise tra tutti i progetti
- Definizione delle proprietà e relazioni dei modelli

### Microsis.Web.Services

API RESTful che funge da backend per tutti i progetti, gestendo l'accesso ai dati e l'autenticazione.

- API per l'autenticazione JWT
- Endpoint per la gestione di progetti, servizi, news, foto e banner
- Interfaccia programmatica per il sito pubblico e l'area riservata
- Servizi con pattern Repository per la gestione dei dati
- Seeding automatico del database con dati iniziali

#### Endpoint principali

- `/api/Auth` - Autenticazione e gestione utenti
- `/api/ProgettiUE` - Gestione progetti europei
- `/api/Servizi` - Gestione servizi offerti
- `/api/News` - Gestione news e articoli
- `/api/Banners` - Gestione banner dell'homepage

### Microsis.Web.Public

Sito web pubblico implementato con Blazor Web .NET 9.0 e controlli Syncfusion v28.1.33.

- Homepage con hero slider dinamico
- Pagine di contenuto pubblico
- Showcase di progetti e servizi
- News e aggiornamenti
- Servizi client per comunicazione con API
- Componenti riutilizzabili per la visualizzazione dei dati

#### Struttura servizi client

- `/Services/ApiConfigService` - Configurazione comunicazione API
- `/Services/BannerService` - Servizio per gestione banner
- Altri servizi client per modelli specifici

### Microsis.Web.Reserved

Area amministrativa riservata per la gestione dei contenuti del sito pubblico.

- Gestione banner dell'homepage
- Gestione gallerie fotografiche
- Pubblicazione e modifica di nuovi progetti
- Gestione offerte di lavoro e ricerca collaboratori
- Dashboard amministrativa
- Servizi client autenticati per comunicazione con API

#### Struttura servizi client

- `/Services/ApiConfigService` - Configurazione comunicazione API con gestione token JWT
- `/Services/BannerService` - Servizio per gestione banner con autenticazione
- Altri servizi client con autenticazione per modelli specifici

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
- Gli amministratori possono gestire i contenuti dinamici come banner, progetti, news e foto

## Gestione dei contenuti dinamici

### Banner dell'homepage

- La homepage include uno slider di banner completamente dinamico
- I banner possono essere gestiti dall'area riservata (aggiunta, modifica, eliminazione)
- Ogni banner include: immagine di sfondo, titolo, sottotitolo, ordine di visualizzazione
- I banner possono essere attivati/disattivati tramite la proprietà `Visible`
- I dati vengono recuperati tramite API e sono disponibili anche in modalità offline (fallback)

## Architettura tecnica

- **Pattern Repository**: Separazione della logica di accesso ai dati dalla logica di business
- **Dependency Injection**: Tutti i servizi vengono registrati tramite container DI
- **Clean Architecture**: Separazione chiara tra UI, logica di business e accesso ai dati
- **CQRS pattern semplificato**: Separazione tra operazioni di lettura e scrittura
- **Service Layer**: Servizi specializzati per ogni tipo di entità
- **Token JWT**: Autenticazione basata su token per l'area riservata

## Struttura della soluzione

```
Microsis.Web/
├── Microsis.Names/            # Modelli dati condivisi
│   └── Models/                # Modelli delle entità
│       ├── Banner.cs          # Modello per i banner dell'homepage
│       ├── Foto.cs            # Modello per le immagini
│       ├── News.cs            # Modello per le news
│       ├── ProgettoUE.cs      # Modello per i progetti europei
│       ├── Servizio.cs        # Modello per i servizi offerti
│       └── User.cs            # Modello per gli utenti
│
├── Microsis.Web.Services/     # Backend API e servizi
│   ├── Controllers/           # Controller API
│   │   ├── AuthController.cs  # Autenticazione
│   │   ├── BannersController.cs # Gestione banner
│   │   ├── NewsController.cs  # Gestione news
│   │   └── ...                # Altri controller
│   ├── Data/                  # Accesso ai dati
│   │   ├── AppDbContext.cs    # Contesto EF Core
│   │   └── SeedData.cs        # Inizializzazione dati
│   └── Services/              # Implementazione servizi
│       ├── BannerService.cs   # Servizio per banner
│       ├── NewsService.cs     # Servizio per news
│       └── ...                # Altri servizi
│
├── Microsis.Web.Public/       # Sito web pubblico
│   ├── Components/            # Componenti Blazor
│   │   ├── Sections/          # Sezioni della pagina
│   │   │   ├── Hero.razor     # Slider banner dinamico
│   │   │   └── ...            # Altre sezioni
│   └── Services/              # Servizi client
│       ├── ApiConfigService.cs # Configurazione API
│       ├── BannerService.cs   # Client per API banner
│       └── ...                # Altri servizi client
│
├── Microsis.Web.Reserved/     # Area amministrativa
│   ├── Components/            # Componenti Blazor admin
│   └── Services/              # Servizi client autenticati
│       ├── ApiConfigService.cs # Config con JWT
│       ├── BannerService.cs   # Client per banner protetto
│       └── ...                # Altri servizi client
│
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
- Utilizzare i servizi specifici per ogni progetto per isolare le responsabilità
- Per i contenuti pubblici, implementare caching lato client per migliorare le performance
