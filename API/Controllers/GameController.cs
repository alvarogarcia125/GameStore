using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace GameStorageApi.Controllers
{
    [ApiController]
    [Route("games")]
    public class GameController : ControllerBase
    {

        private readonly ILogger<GameController> _logger;
        private IGameService _gameService { get; }

        private IGameGenreService _gameGenreService { get; }

        private IGamePlatformService _gamePlatformService { get; }

        private readonly IFileProvider _fileProvider;

        public GameController(
            ILogger<GameController> logger, 
            IGameService gameService,
            IFileProvider fileProvider,
            IGameGenreService gameGenreService,
            IGamePlatformService gamePlatformService)
        {
            _logger = logger;
            _gameService = gameService;
            _fileProvider = fileProvider;
            _gameGenreService = gameGenreService;
            _gamePlatformService = gamePlatformService;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateGame([FromBody] GameRequestDto game)
        {
            try
            {
                Guid idCreatedGame = await _gameService.CreateGame(game);

                return Ok(idCreatedGame);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting game: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{key}")]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<GameResponseDto>> GetGameByKey(string key)
        {
            try
            {
                var game = await _gameService.GetGameByKeyAsync(key);
                if (game != null)
                {
                    return Ok(game);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving game: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("find/{id}")]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<GameResponseDto>> GetGameById(Guid id)
        {
            try
            {
                var game = await _gameService.GetGameById(id);
                if (game != null)
                {
                    return Ok(game);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving game: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGame([FromBody] GameUpdateDto requestDto)
        {
            if (requestDto != null)
            {
                try
                {
                    await _gameService.UpdateGame(requestDto);

                    return Ok("Game updated successfully.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }

            return BadRequest("Invalid input data.");
        }

        [HttpDelete("{key}")]
        public ActionResult DeleteGame(string key)
        {
            try
            {
                GameResponseDto gametoDelete = _gameService.GetGameByKey(key);

                if (gametoDelete == null)
                {
                    return NotFound(); 
                }

                _gameService.DeleteGame(gametoDelete);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
            
        }

        [HttpGet("{key}/file")]
        [ResponseCache(Duration = 60)]
        public IActionResult DownloadGameFile(string key)
        {
            try
            {
                var gameFilePath = _gameService.GeneratePdf(key);

                if (string.IsNullOrEmpty(gameFilePath) || !System.IO.File.Exists(gameFilePath))
                {
                    return NotFound(); 
                }

                var fileName = Path.GetFileName(gameFilePath);

                if (_fileProvider != null)
                {
                    var fileInfo = _fileProvider.GetFileInfo(gameFilePath);

                    if (fileInfo.Exists)
                    {
                        var fileStream = fileInfo.CreateReadStream();
                        return File(fileStream, "application/octet-stream", fileName);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor: _fileProvider no está inicializado");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar la solicitud: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }


        [HttpGet()]
        [ResponseCache(Duration = 60)] 
        public async Task<ActionResult<List<GameResponseDto>>> GetAllGames()
        {
            try
            {
                var games = await _gameService.GetAllGamesAsync();

                return Ok(games);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving games: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{key}/genres")]
        public async Task<ActionResult<List<GenreListDto>>> GetGenresByGameKey(string key)
        {
            try
            {
                var genres = await _gameGenreService.GetGenresByGameKey(key);
                if (genres != null)
                {
                    return Ok(genres);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving genres by game key: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{key}/platforms")]
        public async Task<ActionResult<List<GenreListDto>>> GetPlatformsByGameKey(string key)
        {
            try
            {
                var genres = await _gamePlatformService.GetPlatformsByGameKey(key);
                if (genres != null)
                {
                    return Ok(genres);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving genres by game key: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }



    }
}
