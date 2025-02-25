# Microsis - Progetto Blazor Web Public

Questo progetto implementa un sito web in stile DBA Group utilizzando Blazor Web .NET 9.0 con supporto per controlli Syncfusion (v28.1.33) e Telerik Blazor.

## Requisiti

- Visual Studio 2022 (versione 17.10 o superiore)
- .NET 9.0 SDK
- Syncfusion Blazor v28.1.33
- Licenze valide per Syncfusion Blazor e Telerik UI for Blazor

## Configurazione Iniziale

1. Clona il repository o crea un nuovo progetto in Visual Studio
2. Assicurati di avere installato Syncfusion Blazor versione 28.1.33 e Telerik UI for Blazor
3. Aggiorna le chiavi di licenza nel file `Program.cs`
4. Crea la cartella `wwwroot/images` e aggiungi le immagini necessarie

## Struttura del Progetto

Il progetto è strutturato seguendo le best practices per applicazioni Blazor Web:

- **Components/Layout**: Contiene i componenti di layout principali
- **Components/Pages**: Contiene le pagine dell'applicazione
- **Components/Sections**: Contiene le sezioni riutilizzabili della home page
- **wwwroot**: Contiene file statici (CSS, JavaScript, immagini)

## Stili e Temi

Il progetto utilizza:

- Bootstrap 5 per il layout e la griglia
- Font Montserrat per la tipografia
- Font Awesome per le icone
- Temi personalizzati per Syncfusion e Telerik

## Controlli Syncfusion

Il progetto utilizza i controlli Syncfusion Blazor v28.1.33, che includono:

- SfButton per i pulsanti
- SfCard per le card di contenuto
- SfDialog per le finestre di dialogo
- Altri controlli configurabili

## Integrazione con .NET Framework

Il progetto è integrato con:

- .NET 9.0 per il backend
- Blazor WebAssembly o Server per il frontend
- Syncfusion Blazor per i controlli UI
- Telerik Blazor per componenti aggiuntivi

## Estensione del Progetto

Per estendere il progetto:

1. Aggiungi nuove pagine nella cartella `Components/Pages`
2. Crea nuovi componenti riutilizzabili nella cartella `Components/Shared`
3. Aggiungi nuove rotte nel componente `Routes.razor`

## Integrazione con Database

Per integrare un database:

1. Aggiungi Entity Framework Core al progetto
2. Crea modelli nella cartella `Models`
3. Configura il DbContext e le migrazioni
4. Crea servizi per accedere ai dati nella cartella `Services`

## Utilizzo dei Controlli Syncfusion

Il progetto è configurato per utilizzare i controlli Syncfusion Blazor v28.1.33:

- SfButton: Per bottoni con stili personalizzati
- SfCard: Per le card nella sezione settori
- SfDialog: Per finestre modali e popup
- SfGrid: Per tabelle e griglie dati
- SfChart: Per grafici e visualizzazioni

## Deployment

Per il deployment in produzione:

1. Esegui `dotnet publish -c Release`
2. Configura il web server (IIS, Nginx, ecc.)
3. Imposta le variabili d'ambiente per la produzione

## Problemi Comuni

- **Errori di licenza**: Verifica che le chiavi di licenza in `Program.cs` siano valide
- **Problemi CSS**: Controlla l'ordine di caricamento dei fogli di stile
- **Errori JavaScript**: Verifica che i file JS siano caricati correttamente
- **Incompatibilità versioni**: Assicurati di utilizzare Syncfusion Blazor v28.1.33

## Risorse Utili

- [Documentazione .NET 9.0](https://learn.microsoft.com/aspnet/core)
- [Documentazione Blazor](https://learn.microsoft.com/aspnet/core/blazor)
- [Documentazione Syncfusion Blazor v28.1.33](https://blazor.syncfusion.com/documentation)
- [Documentazione Telerik UI for Blazor](https://docs.telerik.com/blazor-ui/introduction)
