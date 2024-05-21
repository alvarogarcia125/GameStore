using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace GameStorageApi.Controllers
{
    [ApiController]
    [Route("genres")]
    public class GenreController : ControllerBase
    {

        private readonly ILogger<GameController> _logger;
        private IGenreService _genreService { get; }

        private IGameGenreService _gameGenreService { get; }

        public GenreController(ILogger<GameController> logger, IGenreService genreService, IGameGenreService gameGenreService)
        {
            _logger = logger;
            _genreService = genreService;
            _gameGenreService = gameGenreService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateGenre([FromBody] GenreRequestDto genre)
        {
            try
            {
                await _genreService.CreateGenre(genre);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting genre: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<GameResponseDto>> GetGenreById(Guid id)
        {
            try
            {
                var genre = await _genreService.GetGenreById(id);
                if (genre != null)
                {
                    return Ok(genre);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving genre: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }



        [HttpGet("")]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<List<GenreListDto>>> GetAllGenres()
        {
            try
            {
                var genres = await _genreService.GetAllGenres();
                return Ok(genres);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving game: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpGet("{id}/genres")]
        public async Task<ActionResult<List<GameResponseDto>>> GetGamesByParentId(Guid id)
        {
            try
            {
                var game = await _genreService.GetGenresByParentId(id);
                if (game != null)
                {
                    return Ok(game);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving games: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPut]
        public async Task<ActionResult> UpdateGenre([FromBody] GenreDto genre)
        {
            if (genre != null)
            {
                try
                {

                    await _genreService.UpdateGenre(genre);

                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }

            return BadRequest("Invalid input data.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGame(Guid id)
        {
            try
            {
                GenreDto genreToDelete = await _genreService.GetGenreByIdAsNoTracking(id);

                if (genreToDelete == null)
                {
                    return NotFound(); 
                }

                _genreService.DeleteGame(genreToDelete);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
            
        }



    }
}
