using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsis.Names.Models;
using Microsis.Web.Services.Services;
using System.Security.Claims;

namespace Microsis.Web.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly ILogger<NewsController> _logger;

        public NewsController(
            INewsService newsService,
            ILogger<NewsController> logger)
        {
            _newsService = newsService;
            _logger = logger;
        }

        /// <summary>
        /// Ottiene tutte le news visibili
        /// </summary>
        /// <returns>Lista di news</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var news = await _newsService.GetAllAsync();
                return Ok(news);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle news");
                return StatusCode(500, new { message = "Si è verificato un errore durante il recupero delle news" });
            }
        }

        /// <summary>
        /// Ottiene tutte le news (visibili e nascoste)
        /// </summary>
        /// <returns>Lista di news</returns>
        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllForAdmin()
        {
            try
            {
                var news = await _newsService.GetAllAsync(includeHidden: true);
                return Ok(news);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero di tutte le news");
                return StatusCode(500, new { message = "Si è verificato un errore durante il recupero di tutte le news" });
            }
        }

        /// <summary>
        /// Ottiene una news tramite ID
        /// </summary>
        /// <param name="id">ID della news</param>
        /// <returns>News</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var news = await _newsService.GetByIdAsync(id);
                if (news == null)
                    return NotFound(new { message = "News non trovata" });

                return Ok(news);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero della news con ID {Id}", id);
                return StatusCode(500, new { message = "Si è verificato un errore durante il recupero della news" });
            }
        }

        /// <summary>
        /// Crea una nuova news
        /// </summary>
        /// <param name="news">Dati della news</param>
        /// <returns>News creata</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] News news)
        {
            try
            {
                // Ottieni l'utente autenticato
                var authorEmail = User.FindFirstValue(ClaimTypes.Email) ?? "system";

                // Crea la news
                var createdNews = await _newsService.CreateAsync(news, authorEmail);
                return CreatedAtAction(nameof(GetById), new { id = createdNews.ID }, createdNews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione della news");
                return StatusCode(500, new { message = "Si è verificato un errore durante la creazione della news" });
            }
        }

        /// <summary>
        /// Aggiorna una news esistente
        /// </summary>
        /// <param name="id">ID della news</param>
        /// <param name="news">Dati aggiornati della news</param>
        /// <returns>News aggiornata</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] News news)
        {
            try
            {
                // Verifica che l'ID sia corretto
                if (id != news.ID)
                    return BadRequest(new { message = "L'ID della news non corrisponde" });

                // Ottieni l'utente autenticato
                var authorEmail = User.FindFirstValue(ClaimTypes.Email) ?? "system";

                // Aggiorna la news
                var updatedNews = await _newsService.UpdateAsync(news, authorEmail);
                return Ok(updatedNews);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "News non trovata" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento della news con ID {Id}", id);
                return StatusCode(500, new { message = "Si è verificato un errore durante l'aggiornamento della news" });
            }
        }

        /// <summary>
        /// Elimina una news
        /// </summary>
        /// <param name="id">ID della news</param>
        /// <returns>Esito dell'operazione</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _newsService.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = "News non trovata" });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'eliminazione della news con ID {Id}", id);
                return StatusCode(500, new { message = "Si è verificato un errore durante l'eliminazione della news" });
            }
        }
    }
}
