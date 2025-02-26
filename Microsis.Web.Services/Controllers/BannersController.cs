using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsis.Names.Models;
using Microsis.Web.Services.Services;
using System.Security.Claims;

namespace Microsis.Web.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BannersController : ControllerBase
    {
        private readonly IBannerAdminService _bannerService;
        private readonly ILogger<BannersController> _logger;

        public BannersController(
            IBannerAdminService bannerService,
            ILogger<BannersController> logger)
        {
            _bannerService = bannerService;
            _logger = logger;
        }

        /// <summary>
        /// Ottiene tutti i banner visibili
        /// </summary>
        /// <returns>Lista di banner</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var banners = await _bannerService.GetAllAsync(includeHidden: false);
                return Ok(banners);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei banner");
                return StatusCode(500, new { message = "Si è verificato un errore durante il recupero dei banner" });
            }
        }
        
        /// <summary>
        /// Ottiene tutti i banner visibili ordinati per campo Order
        /// </summary>
        /// <returns>Lista di banner</returns>
        [HttpGet("ordered")]
        public async Task<IActionResult> GetAllOrdered()
        {
            try
            {
                var banners = await _bannerService.GetVisibleOrderedAsync();
                return Ok(banners);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei banner ordinati");
                return StatusCode(500, new { message = "Si è verificato un errore durante il recupero dei banner ordinati" });
            }
        }

        /// <summary>
        /// Ottiene tutti i banner (visibili e nascosti)
        /// </summary>
        /// <returns>Lista di banner</returns>
        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllForAdmin()
        {
            try
            {
                var banners = await _bannerService.GetAllAsync(includeHidden: true);
                return Ok(banners);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero di tutti i banner");
                return StatusCode(500, new { message = "Si è verificato un errore durante il recupero di tutti i banner" });
            }
        }

        /// <summary>
        /// Ottiene un banner tramite ID
        /// </summary>
        /// <param name="id">ID del banner</param>
        /// <returns>Banner</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var banner = await _bannerService.GetByIdAsync(id);
                if (banner == null)
                    return NotFound(new { message = "Banner non trovato" });

                return Ok(banner);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero del banner con ID {Id}", id);
                return StatusCode(500, new { message = "Si è verificato un errore durante il recupero del banner" });
            }
        }

        /// <summary>
        /// Crea un nuovo banner
        /// </summary>
        /// <param name="banner">Dati del banner</param>
        /// <returns>Banner creato</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Banner banner)
        {
            try
            {
                // Ottieni l'utente autenticato
                var authorEmail = User.FindFirstValue(ClaimTypes.Email) ?? "system";

                // Crea il banner
                var createdBanner = await _bannerService.CreateAsync(banner, authorEmail);
                return CreatedAtAction(nameof(GetById), new { id = createdBanner.ID }, createdBanner);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione del banner");
                return StatusCode(500, new { message = "Si è verificato un errore durante la creazione del banner" });
            }
        }

        /// <summary>
        /// Aggiorna un banner esistente
        /// </summary>
        /// <param name="id">ID del banner</param>
        /// <param name="banner">Dati aggiornati del banner</param>
        /// <returns>Banner aggiornato</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] Banner banner)
        {
            try
            {
                // Verifica che l'ID sia corretto
                if (id != banner.ID)
                    return BadRequest(new { message = "L'ID del banner non corrisponde" });

                // Ottieni l'utente autenticato
                var authorEmail = User.FindFirstValue(ClaimTypes.Email) ?? "system";

                // Aggiorna il banner
                var updatedBanner = await _bannerService.UpdateAsync(banner, authorEmail);
                return Ok(updatedBanner);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Banner non trovato" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento del banner con ID {Id}", id);
                return StatusCode(500, new { message = "Si è verificato un errore durante l'aggiornamento del banner" });
            }
        }

        /// <summary>
        /// Elimina un banner
        /// </summary>
        /// <param name="id">ID del banner</param>
        /// <returns>Esito dell'operazione</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _bannerService.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = "Banner non trovato" });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'eliminazione del banner con ID {Id}", id);
                return StatusCode(500, new { message = "Si è verificato un errore durante l'eliminazione del banner" });
            }
        }
    }
}
