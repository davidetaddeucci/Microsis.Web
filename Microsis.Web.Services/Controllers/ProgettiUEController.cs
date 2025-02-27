using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsis.Names.Models;
using Microsis.Web.Services.Services;
using System.Security.Claims;

namespace Microsis.Web.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgettiUEController : ControllerBase
    {
        private readonly IProgettoUEService _progettoUEService;
        private readonly ILogger<ProgettiUEController> _logger;

        public ProgettiUEController(
            IProgettoUEService progettoUEService,
            ILogger<ProgettiUEController> logger)
        {
            _progettoUEService = progettoUEService;
            _logger = logger;
        }

        /// <summary>
        /// Ottiene tutti i progetti UE visibili
        /// </summary>
        /// <returns>Lista di progetti UE</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var progetti = await _progettoUEService.GetAllAsync();
                return Ok(progetti);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei progetti UE");
                return StatusCode(500, new { message = "Si è verificato un errore durante il recupero dei progetti UE" });
            }
        }

        /// <summary>
        /// Ottiene tutti i progetti UE (visibili e nascosti)
        /// </summary>
        /// <returns>Lista di progetti UE</returns>
        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllForAdmin()
        {
            try
            {
                var progetti = await _progettoUEService.GetAllAsync(includeHidden: true);
                return Ok(progetti);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero di tutti i progetti UE");
                return StatusCode(500, new { message = "Si è verificato un errore durante il recupero di tutti i progetti UE" });
            }
        }

        /// <summary>
        /// Ottiene un progetto UE tramite ID
        /// </summary>
        /// <param name="id">ID del progetto</param>
        /// <returns>Progetto UE</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var progetto = await _progettoUEService.GetByIdAsync(id);
                if (progetto == null)
                    return NotFound(new { message = "Progetto non trovato" });

                return Ok(progetto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero del progetto UE con ID {Id}", id);
                return StatusCode(500, new { message = "Si è verificato un errore durante il recupero del progetto UE" });
            }
        }

        /// <summary>
        /// Crea un nuovo progetto UE
        /// </summary>
        /// <param name="progetto">Dati del progetto</param>
        /// <returns>Progetto creato</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ProgettoUE progetto)
        {
            try
            {
                // Ottieni l'utente autenticato
                var authorEmail = User.FindFirstValue(ClaimTypes.Email) ?? "system";

                // Crea il progetto
                var createdProgetto = await _progettoUEService.CreateAsync(progetto, authorEmail);
                return CreatedAtAction(nameof(GetById), new { id = createdProgetto.ID }, createdProgetto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione del progetto UE");
                return StatusCode(500, new { message = "Si è verificato un errore durante la creazione del progetto UE" });
            }
        }
        /// <summary>
        /// Ottiene le ultime N news.
        /// </summary>
        /// <param name="count">Numero di news da recuperare.</param>
        /// <returns>Lista delle ultime N news.</returns>
        [HttpGet("latest")] // Route esplicita: /api/news/latest
        public async Task<IActionResult> GetLatestAsync([FromQuery] int num_records_to_retrieve)
        {
            try
            {
                var news = await _progettoUEService.GetLatestAsync(num_records_to_retrieve);
                return Ok(news);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle ultime news");
                return StatusCode(500, new { message = "Si è verificato un errore." });
            }
        }
        /// <summary>
        /// Aggiorna un progetto UE esistente
        /// </summary>
        /// <param name="id">ID del progetto</param>
        /// <param name="progetto">Dati aggiornati del progetto</param>
        /// <returns>Progetto aggiornato</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProgettoUE progetto)
        {
            try
            {
                // Verifica che l'ID sia corretto
                if (id != progetto.ID)
                    return BadRequest(new { message = "L'ID del progetto non corrisponde" });

                // Ottieni l'utente autenticato
                var authorEmail = User.FindFirstValue(ClaimTypes.Email) ?? "system";

                // Aggiorna il progetto
                var updatedProgetto = await _progettoUEService.UpdateAsync(progetto, authorEmail);
                return Ok(updatedProgetto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Progetto non trovato" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento del progetto UE con ID {Id}", id);
                return StatusCode(500, new { message = "Si è verificato un errore durante l'aggiornamento del progetto UE" });
            }
        }

        /// <summary>
        /// Elimina un progetto UE
        /// </summary>
        /// <param name="id">ID del progetto</param>
        /// <returns>Esito dell'operazione</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _progettoUEService.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = "Progetto non trovato" });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'eliminazione del progetto UE con ID {Id}", id);
                return StatusCode(500, new { message = "Si è verificato un errore durante l'eliminazione del progetto UE" });
            }
        }
    }
}
