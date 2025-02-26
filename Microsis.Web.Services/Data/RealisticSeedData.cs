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

            // Applica le migrazioni per assicurarti che il database sia creato e aggiornato
            context.Database.Migrate();

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
            // Progetti UE basati sulle informazioni fornite dal cliente
            var progetto1 = new ProgettoUE
            {
                ID = Guid.NewGuid(),
                Titolo = "MHCD - Sistema di monitoraggio per scompenso cardiaco",
                Abstract = "Sistema di monitoraggio dei pazienti affetti da scompenso cardiaco mirato alla stratificazione prognostica attraverso markers clinici bioumorali ed elettrocardiografici con l'ausilio di algoritmi di AI. Il progetto è cofinanziato dall'Unione Europea e dalla Regione Lazio nell'ambito del programma di Coesione Italia 2014-2020.",
                EntiCoinvolti = new List<string> { "Microsis srl", "Regione Lazio", "Unione Europea" },
                Tab_Name = "MHCD",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var progetto2 = new ProgettoUE
            {
                ID = Guid.NewGuid(),
                Titolo = "FTE0000516 - Marker Cardiaci Prognostici",
                Abstract = "Sistema per l'individuazione di Marker Cardiaci Prognostici basato su tecnologie di Intelligenza Artificiale. Progetto sviluppato in collaborazione con il Ministero delle Imprese e del Made in Italy, Dipartimento per le politiche e per le imprese.",
                EntiCoinvolti = new List<string> { "Microsis srl", "Università La Sapienza - Dip. SCIAC", "Ministero delle Imprese e del Made in Italy" },
                Tab_Name = "FTE0000516",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var progetto3 = new ProgettoUE
            {
                ID = Guid.NewGuid(),
                Titolo = "B87H24K02800004 - Conservazione ecosistemi dulciacquicoli",
                Abstract = "Sistema di Monitoraggio per la conservazione degli ecosistemi dulciacquicoli del Parco di Veio e per la preservazione della Salamandrina dagli Occhiali. Progetto finanziato dall'Unione Europea tramite l'Università di Roma nell'ambito della Missione 4 Componente 2 'Dalla ricerca all'impresa'.",
                EntiCoinvolti = new List<string> { "Microsis srl", "Università di Roma", "Parco di Veio", "Unione Europea" },
                Tab_Name = "Ecosistemi",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var progetto4 = new ProgettoUE
            {
                ID = Guid.NewGuid(),
                Titolo = "2TRAIN - The Future of Health Monitoring",
                Abstract = "Enabling digital technologies for holistic health-lifestyle monitoring and assisted supervision supported by Artificial Intelligence networks. Progetto finanziato dall'Unione Europea nell'ambito del programma Horizon 2020 Research & Innovation, mirato allo sviluppo di tecnologie digitali per il monitoraggio olistico della salute e dello stile di vita.",
                EntiCoinvolti = new List<string> { "Microsis srl", "Unione Europea", "Horizon 2020" },
                Tab_Name = "2TRAIN",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            context.ProgettiUE.AddRange(progetto1, progetto2, progetto3, progetto4);
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
                Descrizione = "Implementazione di soluzioni di Intelligenza Artificiale per la manutenzione predittiva e il monitoraggio sanitario. I nostri sistemi utilizzano algoritmi avanzati di machine learning per analizzare dati provenienti da sensori biomedici e industriali, consentendo diagnosi precoci e interventi preventivi.",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            context.Servizi.AddRange(servizio1, servizio2, servizio3, servizio4, servizio5);
        }

        private static void SeedNews(AppDbContext context)
        {
            // News basate su informazioni aziendali e progetti
            var news1 = new News
            {
                ID = Guid.NewGuid(),
                Titolo = "Microsis partner nel progetto 2TRAIN di Horizon 2020",
                Contenuto = "Siamo orgogliosi di annunciare la nostra partecipazione come partner nel progetto 2TRAIN - The Future of Health Monitoring, finanziato dall'Unione Europea nell'ambito del programma Horizon 2020 Research & Innovation. Il progetto mira a sviluppare tecnologie digitali innovative per il monitoraggio olistico della salute e dello stile di vita, supportate da reti di Intelligenza Artificiale. Questo progetto rappresenta un importante passo avanti nella nostra missione di applicare tecnologie avanzate al settore sanitario.",
                Descrizione = "Microsis partner ufficiale del progetto 2TRAIN di Horizon 2020 per il monitoraggio sanitario avanzato",
                Visible = true,
                DataPubblicazione = DateTime.Now.AddDays(-30),
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var news2 = new News
            {
                ID = Guid.NewGuid(),
                Titolo = "Avviato il sistema di monitoraggio per scompenso cardiaco MHCD",
                Contenuto = "Microsis annuncia l'avvio ufficiale del progetto MHCD, un sistema innovativo per il monitoraggio dei pazienti affetti da scompenso cardiaco, sviluppato con il cofinanziamento dell'Unione Europea e della Regione Lazio. Il sistema utilizza algoritmi di Intelligenza Artificiale per analizzare markers clinici bioumorali ed elettrocardiografici, permettendo una stratificazione prognostica avanzata. Questo progetto rappresenta un significativo avanzamento nel campo della telemedicina e del monitoraggio remoto dei pazienti cardiopatici.",
                Descrizione = "Avviato il sistema MHCD per il monitoraggio dei pazienti con scompenso cardiaco",
                Visible = true,
                DataPubblicazione = DateTime.Now.AddDays(-15),
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var news3 = new News
            {
                ID = Guid.NewGuid(),
                Titolo = "Microsis e Università La Sapienza insieme per il progetto FTE0000516",
                Contenuto = "Microsis e il Dipartimento SCIAC dell'Università La Sapienza di Roma hanno ufficialmente avviato la collaborazione per il progetto FTE0000516, mirato allo sviluppo di un sistema per l'individuazione di Marker Cardiaci Prognostici basato su tecnologie di Intelligenza Artificiale. Il progetto, sostenuto dal Ministero delle Imprese e del Made in Italy, rappresenta un esempio virtuoso di sinergia tra ricerca accademica e innovazione industriale nel settore della salute digitale.",
                Descrizione = "Avviata collaborazione con Università La Sapienza per lo sviluppo di Marker Cardiaci Prognostici",
                Visible = true,
                DataPubblicazione = DateTime.Now.AddDays(-7),
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var news4 = new News
            {
                ID = Guid.NewGuid(),
                Titolo = "Nuovo sistema di monitoraggio per la conservazione degli ecosistemi acquatici",
                Contenuto = "Microsis è lieta di annunciare l'avvio del progetto di monitoraggio per la conservazione degli ecosistemi dulciacquicoli del Parco di Veio e per la preservazione della Salamandrina dagli Occhiali. Questo sistema, finanziato dall'Unione Europea tramite l'Università di Roma, combina tecnologie avanzate di sensoristica e analisi dati per monitorare in tempo reale parametri ambientali critici. Il progetto, identificato dal CUP B87H24K02800004, rientra nella Missione 4 Componente 2 'Dalla ricerca all'impresa' e rappresenta un importante contributo alla tutela della biodiversità locale.",
                Descrizione = "Avviato progetto di monitoraggio per ecosistemi acquatici nel Parco di Veio",
                Visible = true,
                DataPubblicazione = DateTime.Now.AddDays(-3),
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            context.News.AddRange(news1, news2, news3, news4);
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
