using Microsis.Names.Models;

namespace Microsis.Web.Services.Services
{
    /// <summary>
    /// Interfaccia per il servizio di gestione degli utenti
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Autentica un utente con email e password
        /// </summary>
        /// <param name="email">Email dell'utente</param>
        /// <param name="password">Password dell'utente</param>
        /// <returns>Utente autenticato o null</returns>
        Task<User?> AuthenticateAsync(string email, string password);
        
        /// <summary>
        /// Ottiene un utente tramite ID
        /// </summary>
        /// <param name="id">ID dell'utente</param>
        /// <returns>Utente o null</returns>
        Task<User?> GetByIdAsync(Guid id);
        
        /// <summary>
        /// Ottiene un utente tramite email
        /// </summary>
        /// <param name="email">Email dell'utente</param>
        /// <returns>Utente o null</returns>
        Task<User?> GetByEmailAsync(string email);
        
        /// <summary>
        /// Ottiene tutti gli utenti
        /// </summary>
        /// <returns>Lista di utenti</returns>
        Task<IEnumerable<User>> GetAllAsync();
        
        /// <summary>
        /// Crea un nuovo utente
        /// </summary>
        /// <param name="user">Dati dell'utente</param>
        /// <param name="password">Password in chiaro</param>
        /// <returns>Utente creato</returns>
        Task<User> CreateAsync(User user, string password);
        
        /// <summary>
        /// Aggiorna un utente esistente
        /// </summary>
        /// <param name="user">Utente da aggiornare</param>
        /// <param name="password">Nuova password (opzionale)</param>
        /// <returns>Utente aggiornato</returns>
        Task<User> UpdateAsync(User user, string? password = null);
        
        /// <summary>
        /// Elimina un utente
        /// </summary>
        /// <param name="id">ID dell'utente</param>
        /// <returns>True se eliminato con successo</returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
