# Microsis.Names

Questo progetto contiene tutti i modelli di dati condivisi tra i diversi progetti della soluzione Microsis.Web.

## Descrizione

Microsis.Names è una libreria di classi .NET 9.0 che definisce le entità e i modelli dati utilizzati dai diversi progetti della soluzione, facilitando il riutilizzo del codice e garantendo la consistenza delle strutture dati in tutta l'applicazione.

## Modelli inclusi

- **ProgettoUE**: Rappresenta i progetti Europei gestiti da Microsis
- **Servizio**: Rappresenta i servizi offerti
- **News**: Rappresenta le notizie e gli aggiornamenti
- **User**: Rappresenta gli utenti del sistema amministrativo
- **Foto**: Rappresenta le immagini e le gallerie fotografiche

## Utilizzo

Questo progetto viene referenziato dagli altri progetti della soluzione:
- `Microsis.Web.Public`: Sito web pubblico
- `Microsis.Web.Reserved`: Area amministrativa
- `Microsis.Web.Services`: API di servizio

## Estensione

Per aggiungere nuovi modelli:
1. Creare una nuova classe in `/Models`
2. Definire le proprietà e le relazioni
3. Aggiungere la documentazione XML
4. Referenziare la nuova classe nei progetti che la utilizzano
