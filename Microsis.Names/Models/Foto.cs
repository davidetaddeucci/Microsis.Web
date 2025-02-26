using System;

namespace Microsis.Names.Models
{
    /// <summary>
    /// Modello per le immagini e le foto del sito
    /// </summary>
    public class Foto
    {
        /// <summary>
        /// Identificativo univoco della foto
        /// </summary>
        public Guid ID { get; set; }
        
        /// <summary>
        /// Titolo della foto
        /// </summary>
        public string Titolo { get; set; } = string.Empty;
        
        /// <summary>
        /// Descrizione della foto
        /// </summary>
        public string Descrizione { get; set; } = string.Empty;
        
        /// <summary>
        /// Percorso dell'immagine
        /// </summary>
        public string Path { get; set; } = string.Empty;
        
        /// <summary>
        /// Nome del file dell'immagine
        /// </summary>
        public string Filename { get; set; } = string.Empty;
        
        /// <summary>
        /// Data e ora dell'ultimo aggiornamento
        /// </summary>
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Utente che ha caricato o aggiornato la foto
        /// </summary>
        public string Author { get; set; } = string.Empty;
        
        /// <summary>
        /// Flag che indica se la foto è visibile
        /// </summary>
        public bool Visible { get; set; } = true;
        
        /// <summary>
        /// Ordine di visualizzazione della foto
        /// </summary>
        public int Order { get; set; } = 0;
        
        /// <summary>
        /// Identificativo dell'entità collegata (News, Progetto, ecc.)
        /// </summary>
        public Guid? EntityID { get; set; }
        
        /// <summary>
        /// Tipo dell'entità collegata (News, Progetto, ecc.)
        /// </summary>
        public string EntityType { get; set; } = string.Empty;
        
        /// <summary>
        /// URL completo dell'immagine, composto da Path e Filename
        /// </summary>
        public string FullUrl => string.IsNullOrEmpty(Path) || string.IsNullOrEmpty(Filename) 
            ? string.Empty 
            : $"{Path.TrimEnd('/')}/{Filename}";
    }
}
