using Microsoft.AspNetCore.Mvc;
using Microsis.Web.Services.Services;
using System.Text.Json.Serialization;

namespace Microsis.Web.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IUserService userService,
            IJwtService jwtService,
            ILogger<AuthController> logger)
        {
            _userService = userService;
            _jwtService = jwtService;
            _logger = logger;
        }

        /// <summary>
        /// Effettua il login e restituisce un token JWT
        /// </summary>
        /// <param name="model">Credenziali di login</param>
        /// <returns>Token JWT</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            try
            {
                // Controlla che i dati del modello siano validi
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Autentica l'utente
                var user = await _userService.AuthenticateAsync(model.Email, model.Password);
                if (user == null)
                    return Unauthorized(new { message = "Email o password non validi" });

                // Genera il token JWT
                var token = _jwtService.GenerateJwtToken(user);

                // Restituisci i dati dell'utente e il token
                return Ok(new LoginResponse
                {
                    Id = user.ID,
                    Email = user.Email,
                    NomeEsteso = user.NomeEsteso,
                    Token = token
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il login: {ErrorMessage}", ex.Message);
                return StatusCode(500, new { message = "Si è verificato un errore durante il login. Riprova più tardi." });
            }
        }

        /// <summary>
        /// Registra un nuovo utente (solo per sviluppo)
        /// </summary>
        /// <param name="model">Dati di registrazione</param>
        /// <returns>Utente registrato</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            try
            {
                // Controlla che i dati del modello siano validi
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Verifica se l'email è già registrata
                var existingUser = await _userService.GetByEmailAsync(model.Email);
                if (existingUser != null)
                    return BadRequest(new { message = "Email già registrata" });

                // Crea il nuovo utente
                var user = new Names.Models.User
                {
                    Email = model.Email,
                    NomeEsteso = model.NomeEsteso,
                    Role = "Editor", // Ruolo predefinito per i nuovi utenti
                    IsActive = true
                };

                // Salva l'utente nel database
                await _userService.CreateAsync(user, model.Password);

                return Ok(new { message = "Registrazione completata con successo" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la registrazione: {ErrorMessage}", ex.Message);
                return StatusCode(500, new { message = "Si è verificato un errore durante la registrazione. Riprova più tardi." });
            }
        }
    }

    /// <summary>
    /// Modello per la richiesta di login
    /// </summary>
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>
    /// Modello per la risposta di login
    /// </summary>
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string NomeEsteso { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }

    /// <summary>
    /// Modello per la richiesta di registrazione
    /// </summary>
    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string NomeEsteso { get; set; } = string.Empty;
    }
}
