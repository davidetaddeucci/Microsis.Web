using Microsoft.EntityFrameworkCore;
using Microsis.Names.Models;
using System.Security.Cryptography;
using System.Text;

namespace Microsis.Web.Services.Data
{
    /// <summary>
    /// Classe per il seeding iniziale dei dati nel database
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Inizializza il database con i dati di base
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
            // Esempio di progetto UE
            var progetto1 = new ProgettoUE
            {
                ID = Guid.NewGuid(),
                Titolo = "SMART Digital Transformation",
                Abstract = "Il progetto mira a sviluppare strumenti digitali innovativi per le PMI nell'ambito dell'Industria 4.0.",
                EntiCoinvolti = new List<string> { "Microsis srl", "Università di Pisa", "CNA Toscana" },
                Tab_Name = "SMART",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var progetto2 = new ProgettoUE
            {
                ID = Guid.NewGuid(),
                Titolo = "ECO-Innovation Network",
                Abstract = "Progetto per la creazione di una rete di aziende impegnate nell'innovazione sostenibile e nelle tecnologie verdi.",
                EntiCoinvolti = new List<string> { "Microsis srl", "Agenzia Regionale per l'Ambiente", "Confcommercio" },
                Tab_Name = "ECO-NET",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            context.ProgettiUE.AddRange(progetto1, progetto2);
        }

        private static void SeedServizi(AppDbContext context)
        {
            // Esempio di servizi
            var servizio1 = new Servizio
            {
                ID = Guid.NewGuid(),
                Titolo = "Consulenza IT",
                Descrizione = "Forniamo servizi di consulenza IT per le aziende in crescita, aiutandole a sviluppare strategie digitali efficaci.",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var servizio2 = new Servizio
            {
                ID = Guid.NewGuid(),
                Titolo = "Sviluppo Software",
                Descrizione = "Sviluppiamo soluzioni software personalizzate per rispondere alle esigenze specifiche delle aziende.",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var servizio3 = new Servizio
            {
                ID = Guid.NewGuid(),
                Titolo = "Formazione Digitale",
                Descrizione = "Offriamo corsi di formazione per aiutare il personale aziendale ad acquisire competenze digitali avanzate.",
                Visible = true,
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            context.Servizi.AddRange(servizio1, servizio2, servizio3);
        }

        private static void SeedNews(AppDbContext context)
        {
            // Esempio di news
            var news1 = new News
            {
                ID = Guid.NewGuid(),
                Titolo = "Microsis diventa partner Microsoft",
                Contenuto = "Siamo lieti di annunciare che Microsis è diventata partner ufficiale Microsoft. Questa partnership ci permetterà di offrire soluzioni ancora più avanzate ai nostri clienti.",
                Descrizione = "Microsis diventa partner ufficiale Microsoft",
                Visible = true,
                DataPubblicazione = DateTime.Now.AddDays(-15),
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            var news2 = new News
            {
                ID = Guid.NewGuid(),
                Titolo = "Nuovi corsi di formazione disponibili",
                Contenuto = "Abbiamo lanciato una nuova serie di corsi di formazione per le aziende che vogliono migliorare le competenze digitali del proprio personale.",
                Descrizione = "Nuovi corsi di formazione per le competenze digitali",
                Visible = true,
                DataPubblicazione = DateTime.Now.AddDays(-7),
                LastUpdate = DateTime.Now,
                Author = "info@hybrid.it"
            };

            context.News.AddRange(news1, news2);
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
