using System;
using System.Collections.Generic;

namespace Microsis.Names.Models
{
    /// <summary>
    /// Modello per i progetti UE
    /// </summary>
    public class ProgettoUE
    {
        /// <summary>
        /// Identificativo univoco del progetto
        /// </summary>
        public Guid ID { get; set; }
        
        /// <summary>
        /// Titolo del progetto
        /// </summary>
        public string Titolo { get; set; } = string.Empty;
        
        /// <summary>
        /// Abstract/descrizione del progetto
        /// </summary>
        public string Abstract { get; set; } = string.Empty;
        
        /// <summary>
        /// Lista degli enti coinvolti nel progetto
        /// </summary>
        public List<string> EntiCoinvolti { get; set; } = new List<string>();
        
        /// <summary>
        /// Nome da visualizzare nella tab in home
        /// </summary>
        public string Tab_Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Flag che indica se il progetto è visibile sul sito
        /// </summary>
        public bool Visible { get; set; } = true;
        
        /// <summary>
        /// Percorso dell'immagine rappresentativa del progetto
        /// </summary>
        public string? ImagePath { get; set; }
        
        /// <summary>
        /// Data e ora dell'ultimo aggiornamento
        /// </summary>
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Utente che ha effettuato l'ultimo aggiornamento
        /// </summary>
        public string Author { get; set; } = string.Empty;
    }
}
