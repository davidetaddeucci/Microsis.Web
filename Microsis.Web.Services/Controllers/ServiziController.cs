using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsis.Names.Models;
using Microsis.Web.Services.Services;
using System.Security.Claims;

namespace Microsis.Web.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiziController : ControllerBase
    {
        private readonly IServizioService _servizioService;
        private readonly ILogger<ServiziController> _logger;

        public ServiziController(
            IServizioService servizioService,
            ILogger<ServiziController> logger)
        {
            _servizioService = servizioService;
            _logger = logger;
        }

        /// <summary>
        /// Ottiene tutti i servizi visibili
        /// </summary>
        /// <returns>Lista di servizi</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var servizi = await _servizioService.GetAllAsync();
                return Ok(servizi);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei servizi");
                return StatusCode(500, new { message = "Si è verificato un errore durante il recupero dei servizi" });
            }
        }

        /// <summary>
        /// Ottiene tutti i servizi (visibili e nascosti)
        /// </summary>
        /// <returns>Lista di servizi</returns>
        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllForAdmin()
        {
            try
            {
                var servizi = await _servizioService.GetAllAsync(includeHidden: true);
                return Ok(servizi);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero di tutti i servizi");
                return StatusCode(500, new { message = "Si è verificato un errore durante il recupero di tutti i servizi" });
            }
        }

        /// <summary>
        /// Ottiene un servizio tramite ID
        /// </summary>
        /// <param name="id">ID del servizio</param>
        /// <returns>Servizio</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var servizio = await _servizioService.GetByIdAsync(id);
                if (servizio == null)
                    return NotFound(new { message = "Servizio non trovato" });

                return Ok(servizio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero del servizio con ID {Id}", id);
                return StatusCode(500, new { message = "Si è verificato un errore durante il recupero del servizio" });
            }
        }

        /// <summary>
        /// Crea un nuovo servizio
        /// </summary>
        /// <param name="servizio">Dati del servizio</param>
        /// <returns>Servizio creato</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Servizio servizio)
        {
            try
            {
                // Ottieni l'utente autenticato
                var authorEmail = User.FindFirstValue(ClaimTypes.Email) ?? "system";

                // Crea il servizio
                var createdServizio = await _servizioService.CreateAsync(servizio, authorEmail);
                return CreatedAtAction(nameof(GetById), new { id = createdServizio.ID }, createdServizio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione del servizio");
                return StatusCode(500, new { message = "Si è verificato un errore durante la creazione del servizio" });
            }
        }

        /// <summary>
        /// Aggiorna un servizio esistente
        /// </summary>
        /// <param name="id">ID del servizio</param>
        /// <param name="servizio">Dati aggiornati del servizio</param>
        /// <returns>Servizio aggiornato</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] Servizio servizio)
        {
            try
            {
                // Verifica che l'ID sia corretto
                if (id != servizio.ID)
                    return BadRequest(new { message = "L'ID del servizio non corrisponde" });

                // Ottieni l'utente autenticato
                var authorEmail = User.FindFirstValue(ClaimTypes.Email) ?? "system";

                // Aggiorna il servizio
                var updatedServizio = await _servizioService.UpdateAsync(servizio, authorEmail);
                return Ok(updatedServizio);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Servizio non trovato" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento del servizio con ID {Id}", id);
                return StatusCode(500, new { message = "Si è verificato un errore durante l'aggiornamento del servizio" });
            }
        }

        /// <summary>
        /// Elimina un servizio
        /// </summary>
        /// <param name="id">ID del servizio</param>
        /// <returns>Esito dell'operazione</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _servizioService.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = "Servizio non trovato" });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'eliminazione del servizio con ID {Id}", id);
                return StatusCode(500, new { message = "Si è verificato un errore durante l'eliminazione del servizio" });
            }
        }
    }
}
