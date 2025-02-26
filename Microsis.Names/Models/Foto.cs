using System;

namespace Microsis.Names.Models
{
    /// <summary>
    /// Modello per la gestione delle foto
    /// </summary>
    public class Foto
    {
        /// <summary>
        /// Identificativo univoco della foto
        /// </summary>
        public Guid ID { get; set; }
        
        /// <summary>
        /// Percorso locale dove è salvata la foto
        /// </summary>
        public string LocalPath { get; set; } = string.Empty;
        
        /// <summary>
        /// Nome del file originale
        /// </summary>
        public string Filename { get; set; } = string.Empty;
        
        /// <summary>
        /// Titolo della foto
        /// </summary>
        public string Title { get; set; } = string.Empty;
        
        /// <summary>
        /// Descrizione opzionale della foto
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// Data e ora dell'ultimo aggiornamento
        /// </summary>
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Utente che ha caricato o aggiornato la foto
        /// </summary>
        public string Author { get; set; } = string.Empty;
        
        /// <summary>
        /// Dimensione del file in bytes
        /// </summary>
        public long FileSize { get; set; }
        
        /// <summary>
        /// Flag che indica se la foto è visibile
        /// </summary>
        public bool Visible { get; set; } = true;
    }
}
