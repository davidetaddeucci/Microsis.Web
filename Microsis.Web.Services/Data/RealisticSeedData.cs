using Microsoft.EntityFrameworkCore;
using Microsis.Names.Models;
using System.Security.Cryptography;
using System.Text;

namespace Microsis.Web.Services.Data
{
    /// <summary>
    /// Classe per il seeding realistico dei dati nel database
    /// basato sui contenuti del sito www.microsis.it
    /// </summary>
    public static class RealisticSeedData
    {
        /// <summary>
        /// Inizializza il database con i dati realistici
        /// </summary>
        /// <param name="serviceProvider">Provider di servizi</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            // Assicurati che il database esista ed è stato creato
            context.Database.EnsureCreated();

            // Seeding degli utenti se non ci sono
            if (!context.Users.Any())
            {
                SeedUsers(context);
            }

            // Seeding dei progetti UE se non ci sono
            if (!context.ProgettiUE.Any())
            {
                SeedProgettiUE(context);
            }

            // Seeding dei servizi se non ci sono
            if (!context.Servizi.Any())
            {
                SeedServizi(context);
            }

            // Seeding delle news se non ci sono
            if (!context.News.Any())
            {
                SeedNews(context);
            }

            // Salva i cambiamenti nel database
            context.SaveChanges();
        }

        private static void SeedUsers(AppDbContext context)
        {
            context.Users.Add(new User
            {
                ID = Guid.NewGuid(),
                Email = "info@hybrid.it",
                Password = HashPassword("Florealia2025"),
                NomeEsteso = "Hybrid sas",
                Role = "Admin",
                IsActive = true,
                LastUpdate = DateTime.Now
            });
        }

        private static void SeedProgettiUE(AppDbContext context)
        {
            // Progetti UE basati su attività viste sul sito
            var progetto1 = new ProgettoUE
            {
                ID = Guid.NewGuid(),
                Titolo = "ESPRIT - Enhanced Space Power for Research and Industrial Technologies",
                Abstract = "Progetto di ricerca per lo sviluppo di amplificatori di alta potenza per comunicazioni con lo spazio profondo, in collaborazione con ESA e l'Agenzia Spaziale Italiana. Il progetto mira a migliorare l'efficienza e la potenza delle comunicazioni spaziali per missioni di lunga distanza.",
                EntiCoinvolti = new List<string> { "Microsis srl", "Agenzia Spaziale Italiana", "European Space Agency", "CNR-IEIIT" },
                Tab_Name = "ESPRIT",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var progetto2 = new ProgettoUE
            {
                ID = Guid.NewGuid(),
                Titolo = "AI-PREDICT - Intelligenza Artificiale per la Manutenzione Predittiva Industriale",
                Abstract = "Progetto europeo per l'implementazione di tecnologie di Intelligenza Artificiale nell'ambito della manutenzione predittiva per impianti industriali. Il sistema analizza i dati raccolti da sensori IoT per prevedere guasti e ottimizzare i cicli di manutenzione.",
                EntiCoinvolti = new List<string> { "Microsis srl", "Politecnico di Milano", "Università di Roma Tor Vergata", "Confindustria" },
                Tab_Name = "AI-PREDICT",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var progetto3 = new ProgettoUE
            {
                ID = Guid.NewGuid(),
                Titolo = "MonCISPACE - Monitoraggio e Controllo Integrato per Sistemi Spaziali e Civili",
                Abstract = "Sviluppo di una piattaforma avanzata per il monitoraggio e controllo in ambito civile, militare e spaziale. Il progetto integra tecnologie hardware e software per creare un sistema di monitoraggio unificato ad alta affidabilità.",
                EntiCoinvolti = new List<string> { "Microsis srl", "Ministero della Difesa", "ENEA", "Università La Sapienza" },
                Tab_Name = "MonCISPACE",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            context.ProgettiUE.AddRange(progetto1, progetto2, progetto3);
        }

        private static void SeedServizi(AppDbContext context)
        {
            // Servizi basati sul sito ufficiale
            var servizio1 = new Servizio
            {
                ID = Guid.NewGuid(),
                Titolo = "Progettazione Hardware",
                Descrizione = "Servizi di progettazione hardware specializzata per il monitoraggio e controllo in ambito civile, militare e spaziale. Forniamo soluzioni personalizzate che soddisfano gli elevati standard richiesti nei settori più esigenti, con particolare attenzione all'affidabilità e alle prestazioni in condizioni critiche.",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var servizio2 = new Servizio
            {
                ID = Guid.NewGuid(),
                Titolo = "Progettazione Software",
                Descrizione = "Sviluppo di soluzioni software in ambito gestionale, industriale e scientifico. I nostri team di esperti realizzano applicazioni su misura che rispondono alle specifiche esigenze dei clienti, garantendo elevata qualità del codice, manutenibilità e scalabilità.",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var servizio3 = new Servizio
            {
                ID = Guid.NewGuid(),
                Titolo = "Sistemi e Banchi ATE",
                Descrizione = "Progettazione e realizzazione banchi prova automatici (ATE) per il collaudo e la verifica di apparecchiature elettroniche. Le nostre soluzioni permettono di automatizzare i processi di test, aumentando l'efficienza produttiva e garantendo un controllo qualità rigoroso.",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var servizio4 = new Servizio
            {
                ID = Guid.NewGuid(),
                Titolo = "Spaces Technologies",
                Descrizione = "Sviluppo di amplificatori di alta potenza per comunicazioni con lo spazio profondo. Le nostre tecnologie spaziali sono progettate per garantire comunicazioni affidabili su distanze estreme, supportando missioni spaziali nazionali e internazionali.",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var servizio5 = new Servizio
            {
                ID = Guid.NewGuid(),
                Titolo = "Intelligenza Artificiale",
                Descrizione = "Implementazione di soluzioni di Intelligenza Artificiale per la manutenzione predittiva. I nostri sistemi utilizzano algoritmi avanzati di machine learning per analizzare dati provenienti da sensori e prevedere possibili guasti, consentendo interventi preventivi e riducendo i costi operativi.",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            context.Servizi.AddRange(servizio1, servizio2, servizio3, servizio4, servizio5);
        }

        private static void SeedNews(AppDbContext context)
        {
            // News basate su informazioni aziendali
            var news1 = new News
            {
                ID = Guid.NewGuid(),
                Titolo = "Microsis ottiene la certificazione ISO 9001:2015 e ISO 14001:2015",
                Contenuto = "Siamo lieti di annunciare che Microsis ha ottenuto il rinnovo delle certificazioni ISO 9001:2015 per i sistemi di gestione della qualità e ISO 14001:2015 per i sistemi di gestione ambientale. Queste certificazioni confermano il nostro impegno verso l'eccellenza e la sostenibilità in tutti i nostri processi aziendali.",
                Descrizione = "Ottenuto il rinnovo delle certificazioni ISO 9001:2015 e ISO 14001:2015",
                Visible = true,
                DataPubblicazione = DateTime.Now.AddDays(-30),
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var news2 = new News
            {
                ID = Guid.NewGuid(),
                Titolo = "Nuovo progetto europeo per la tecnologia spaziale",
                Contenuto = "Microsis è lieta di annunciare l'avvio di un nuovo progetto europeo nell'ambito delle tecnologie spaziali. Il progetto, finanziato dal programma Horizon Europe, si concentrerà sullo sviluppo di amplificatori di alta potenza per le comunicazioni con lo spazio profondo, rafforzando la nostra posizione di leader nel settore delle tecnologie spaziali.",
                Descrizione = "Avviato nuovo progetto europeo per lo sviluppo di tecnologie di comunicazione spaziale",
                Visible = true,
                DataPubblicazione = DateTime.Now.AddDays(-15),
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var news3 = new News
            {
                ID = Guid.NewGuid(),
                Titolo = "Partnership con ITS per la formazione tecnica specializzata",
                Contenuto = "Microsis ha siglato un accordo di collaborazione con ITS (Istituto Tecnico Superiore) per la formazione di tecnici specializzati nel settore dell'elettronica e dell'informatica. Questa partnership strategica permetterà a Microsis di contribuire alla formazione di giovani talenti e di accedere a un bacino di risorse qualificate per supportare la crescita aziendale.",
                Descrizione = "Siglato accordo con ITS per la formazione tecnica specializzata",
                Visible = true,
                DataPubblicazione = DateTime.Now.AddDays(-7),
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            context.News.AddRange(news1, news2, news3);
        }

        /// <summary>
        /// Genera un hash della password fornita
        /// </summary>
        /// <param name="password">Password in chiaro</param>
        /// <returns>Hash della password</returns>
        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
