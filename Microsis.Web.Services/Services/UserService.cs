using Microsoft.EntityFrameworkCore;
using Microsis.Names.Models;
using Microsis.Web.Services.Data;
using System.Security.Cryptography;
using System.Text;

namespace Microsis.Web.Services.Services
{
    /// <summary>
    /// Implementazione del servizio di gestione degli utenti
    /// </summary>
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Autentica un utente con email e password
        /// </summary>
        /// <param name="email">Email dell'utente</param>
        /// <param name="password">Password dell'utente</param>
        /// <returns>Utente autenticato o null</returns>
        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.IsActive);

            // Verifica se l'utente esiste
            if (user == null)
                return null;

            // Verifica se la password è corretta
            if (!VerifyPasswordHash(password, user.Password))
                return null;

            // Aggiorna la data dell'ultimo login
            user.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Ottiene un utente tramite ID
        /// </summary>
        /// <param name="id">ID dell'utente</param>
        /// <returns>Utente o null</returns>
        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        /// <summary>
        /// Ottiene un utente tramite email
        /// </summary>
        /// <param name="email">Email dell'utente</param>
        /// <returns>Utente o null</returns>
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// Ottiene tutti gli utenti
        /// </summary>
        /// <returns>Lista di utenti</returns>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Crea un nuovo utente
        /// </summary>
        /// <param name="user">Dati dell'utente</param>
        /// <param name="password">Password in chiaro</param>
        /// <returns>Utente creato</returns>
        public async Task<User> CreateAsync(User user, string password)
        {
            // Validazione
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required");

            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new ArgumentException($"Email '{user.Email}' is already registered");

            // Hash della password
            user.Password = HashPassword(password);

            // Crea un nuovo ID per l'utente se non è già stato impostato
            if (user.ID == Guid.Empty)
                user.ID = Guid.NewGuid();

            // Imposta la data di creazione e aggiornamento
            user.LastUpdate = DateTime.Now;

            // Salva l'utente
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Aggiorna un utente esistente
        /// </summary>
        /// <param name="user">Utente da aggiornare</param>
        /// <param name="password">Nuova password (opzionale)</param>
        /// <returns>Utente aggiornato</returns>
        public async Task<User> UpdateAsync(User user, string? password = null)
        {
            var userToUpdate = await _context.Users.FindAsync(user.ID)
                ?? throw new KeyNotFoundException("User not found");

            // Controlla se l'email è già registrata per un altro utente
            if (user.Email != userToUpdate.Email && 
                await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new ArgumentException($"Email '{user.Email}' is already registered to another user");

            // Aggiorna i campi dell'utente
            userToUpdate.Email = user.Email;
            userToUpdate.NomeEsteso = user.NomeEsteso;
            userToUpdate.Role = user.Role;
            userToUpdate.IsActive = user.IsActive;
            userToUpdate.LastUpdate = DateTime.Now;

            // Aggiorna la password se specificata
            if (!string.IsNullOrWhiteSpace(password))
                userToUpdate.Password = HashPassword(password);

            // Salva le modifiche
            _context.Users.Update(userToUpdate);
            await _context.SaveChangesAsync();

            return userToUpdate;
        }

        /// <summary>
        /// Elimina un utente
        /// </summary>
        /// <param name="id">ID dell'utente</param>
        /// <returns>True se eliminato con successo</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Genera un hash della password fornita
        /// </summary>
        /// <param name="password">Password in chiaro</param>
        /// <returns>Hash della password</returns>
        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        /// <summary>
        /// Verifica se una password corrisponde all'hash memorizzato
        /// </summary>
        /// <param name="password">Password in chiaro</param>
        /// <param name="storedHash">Hash memorizzato</param>
        /// <returns>True se la password è corretta</returns>
        private static bool VerifyPasswordHash(string password, string storedHash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == storedHash;
        }
    }
}
