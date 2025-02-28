using System;
using System.Collections.Generic;

namespace Microsis.Names.Models
{
    /// <summary>
    /// Modello per i servizi offerti
    /// </summary>
    public class Servizio
    {
        /// <summary>
        /// Identificativo univoco del servizio
        /// </summary>
        public Guid ID { get; set; }
        
        /// <summary>
        /// Titolo del servizio
        /// </summary>
        public string Titolo { get; set; } = string.Empty;
        
        /// <summary>
        /// Titolo del servizio (Inglese)
        /// </summary>
        public string Titolo_EN { get; set; } = string.Empty;
        
        /// <summary>
        /// Descrizione dettagliata del servizio
        /// </summary>
        public string Descrizione { get; set; } = string.Empty;
        
        /// <summary>
        /// Descrizione dettagliata del servizio (Inglese)
        /// </summary>
        public string Descrizione_EN { get; set; } = string.Empty;
        
        /// <summary>
        /// Flag che indica se il servizio è visibile sul sito
        /// </summary>
        public bool Visible { get; set; } = true;
        
        /// <summary>
        /// Data e ora dell'ultimo aggiornamento
        /// </summary>
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Lista di immagini associate al servizio per la galleria fotografica
        /// </summary>
        public List<Guid>? GalleriaFoto { get; set; } = new List<Guid>();
        
        /// <summary>
        /// Utente che ha effettuato l'ultimo aggiornamento
        /// </summary>
        public string Author { get; set; } = string.Empty;
    }
}
