using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsis.Names.Models
{
    public class Settore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        public string Title { get; set; } = string.Empty;
        
        public string Title_EN { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public string Description_EN { get; set; } = string.Empty;
        
        public string ImageUrl { get; set; } = string.Empty;
        
        /// <summary>
        /// Data e ora dell'ultimo aggiornamento
        /// </summary>
        public DateTime LastUpdate { get; set; } = DateTime.Now;

        /// <summary>
        /// Utente che ha effettuato l'ultimo aggiornamento
        /// </summary>
        public string? Author { get; set; } = string.Empty;
    }
}
