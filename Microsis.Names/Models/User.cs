using System;

namespace Microsis.Names.Models
{
    /// <summary>
    /// Modello per gli utenti del sistema
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificativo univoco dell'utente
        /// </summary>
        public Guid ID { get; set; }
        
        /// <summary>
        /// Email dell'utente, utilizzata come username per il login
        /// </summary>
        public string Email { get; set; } = string.Empty;
        
        /// <summary>
        /// Password dell'utente (memorizzata criptata)
        /// </summary>
        public string Password { get; set; } = string.Empty;
        
        /// <summary>
        /// Nome completo/esteso dell'utente
        /// </summary>
        public string NomeEsteso { get; set; } = string.Empty;
        
        /// <summary>
        /// Data e ora dell'ultimo aggiornamento
        /// </summary>
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Indica se l'account è attivo
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Ruolo dell'utente nel sistema (Admin, Editor, Viewer, ecc.)
        /// </summary>
        public string Role { get; set; } = "Editor";
        
        /// <summary>
        /// Data dell'ultimo login
        /// </summary>
        public DateTime? LastLogin { get; set; }
    }
}
