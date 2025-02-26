using Microsoft.IdentityModel.Tokens;
using Microsis.Names.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Microsis.Web.Services.Services
{
    /// <summary>
    /// Implementazione del servizio per la generazione dei token JWT
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Genera un token JWT per l'utente specificato
        /// </summary>
        /// <param name="user">Utente per cui generare il token</param>
        /// <returns>Token JWT</returns>
        public string GenerateJwtToken(User user)
        {
            // Ottieni la chiave segreta dal file di configurazione
            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
                throw new InvalidOperationException("JWT Key not configured in appsettings.json");

            // Crea una chiave di sicurezza simmetrica
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crea i claim dell'utente
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.ID.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.NomeEsteso),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // Configura il token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"] ?? "60")),
                signingCredentials: credentials
            );

            // Genera il token in formato stringa
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
