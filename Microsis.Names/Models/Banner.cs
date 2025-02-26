using System;

namespace Microsis.Names.Models
{
    /// <summary>
    /// Modello per i banner principali del sito
    /// </summary>
    public class Banner
    {
        /// <summary>
        /// Identificativo univoco del banner
        /// </summary>
        public Guid ID { get; set; }
        
        /// <summary>
        /// Percorso dell'immagine del banner
        /// </summary>
        public string Path { get; set; } = string.Empty;
        
        /// <summary>
        /// Nome del file dell'immagine
        /// </summary>
        public string Filename { get; set; } = string.Empty;
        
        /// <summary>
        /// Titolo principale del banner
        /// </summary>
        public string Title { get; set; } = string.Empty;
        
        /// <summary>
        /// Sottotitolo o descrizione del banner
        /// </summary>
        public string Subtitle { get; set; } = string.Empty;
        
        /// <summary>
        /// Data e ora dell'ultimo aggiornamento
        /// </summary>
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Utente che ha creato o aggiornato il banner
        /// </summary>
        public string Author { get; set; } = string.Empty;
        
        /// <summary>
        /// Flag che indica se il banner è visibile
        /// </summary>
        public bool Visible { get; set; } = true;
        
        /// <summary>
        /// Ordine di visualizzazione del banner (per gestire banner multipli)
        /// </summary>
        public int Order { get; set; } = 0;
    }
}
