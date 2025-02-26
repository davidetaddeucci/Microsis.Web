using System;
using System.Collections.Generic;

namespace Microsis.Names.Models
{
    /// <summary>
    /// Modello per le news e gli articoli del sito
    /// </summary>
    public class News
    {
        /// <summary>
        /// Identificativo univoco della news
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Titolo della news
        /// </summary>
        public string Titolo { get; set; } = string.Empty;

        /// <summary>
        /// Descrizione breve della news (per anteprima)
        /// </summary>
        public string Descrizione { get; set; } = string.Empty;

        /// <summary>
        /// Contenuto completo della news in HTML
        /// </summary>
        public string Contenuto { get; set; } = string.Empty;

        /// <summary>
        /// Data di pubblicazione della news
        /// </summary>
        public DateTime DataPubblicazione { get; set; } = DateTime.Now;

        /// <summary>
        /// Autore della news
        /// </summary>
        public string Author { get; set; } = string.Empty;

        /// <summary>
        /// Indica se la news è visibile pubblicamente
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Categorie associate alla news
        /// </summary>
        public string Categoria { get; set; } = string.Empty;

        /// <summary>
        /// Tags associati alla news
        /// </summary>
        public string Tags { get; set; } = string.Empty;

        /// <summary>
        /// Immagine principale della news (percorso)
        /// </summary>
        public string ImmagineUrl { get; set; } = string.Empty;

        /// <summary>
        /// Collezione di immagini aggiuntive per la news
        /// </summary>
        public List<Foto> GalleriaFoto { get; set; } = new List<Foto>();

        /// <summary>
        /// URL amichevole per SEO
        /// </summary>
        public string SlugUrl { get; set; } = string.Empty;

        /// <summary>
        /// Numero di visualizzazioni della news
        /// </summary>
        public int NumeroVisite { get; set; } = 0;

        /// <summary>
        /// Data dell'ultimo aggiornamento
        /// </summary>
        public DateTime LastUpdate { get; set; } = DateTime.Now;

        /// <summary>
        /// Contenuto in formato testo semplice
        /// </summary>
        public string ContentoId { get; set; } = string.Empty;

        /// <summary>
        /// Titolo alternativo per SEO
        /// </summary>
        public string TitoloId { get; set; } = string.Empty;

        /// <summary>
        /// Flag per visualizzazione in homepage
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Descrizione per SEO
        /// </summary>
        public string DescrizioneId { get; set; } = string.Empty;

        /// <summary>
        /// Flag per indicare contenuto pubblico/riservato
        /// </summary>
        public bool IsPublic { get; set; } = true;

        /// <summary>
        /// Flag per gestione della pubblicazione programmata
        /// </summary>
        public bool IsScheduled { get; set; } = false;

        /// <summary>
        /// Data di pubblicazione pubblicazione programmata
        /// </summary>
        public DateTime? ScheduledDate { get; set; }

        /// <summary>
        /// Riferimento all'immagine principale
        /// </summary>
        public Guid? MainImageId { get; set; }

        /// <summary>
        /// Riferimento all'utente autore
        /// </summary>
        public Guid? AuthorId { get; set; }
    }
}
