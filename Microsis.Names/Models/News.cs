using System;
using System.Collections.Generic;

namespace Microsis.Names.Models
{
    /// <summary>
    /// Modello per le news
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
        /// Contenuto completo della news
        /// </summary>
        public string Contenuto { get; set; } = string.Empty;
        
        /// <summary>
        /// Breve descrizione/anteprima della news
        /// </summary>
        public string? Descrizione { get; set; }
        
        /// <summary>
        /// Flag che indica se la news è visibile sul sito
        /// </summary>
        public bool Visible { get; set; } = true;
        
        /// <summary>
        /// Data e ora di pubblicazione della news
        /// </summary>
        public DateTime DataPubblicazione { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Data e ora dell'ultimo aggiornamento
        /// </summary>
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Lista di immagini associate alla news per la galleria fotografica
        /// </summary>
        public List<Guid>? GalleriaFoto { get; set; } = new List<Guid>();
        
        /// <summary>
        /// Utente che ha effettuato l'ultimo aggiornamento
        /// </summary>
        public string Author { get; set; } = string.Empty;
    }
}
