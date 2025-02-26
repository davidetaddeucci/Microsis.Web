using Microsis.Names.Models;

namespace Microsis.Web.Services.Services
{
    /// <summary>
    /// Interfaccia per il servizio di generazione dei token JWT
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Genera un token JWT per l'utente specificato
        /// </summary>
        /// <param name="user">Utente per cui generare il token</param>
        /// <returns>Token JWT</returns>
        string GenerateJwtToken(User user);
    }
}
