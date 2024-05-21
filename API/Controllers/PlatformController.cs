using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStorageApi.Controllers
{
    [ApiController]
    [Route("platforms")]
    public class PlatformController : ControllerBase
    {

        private readonly ILogger<PlatformController> _logger;
        private IGamePlatformService _gamePlatformService { get; }

        private IPlatformService _platformService { get; }

        public PlatformController(ILogger<PlatformController> logger, IGamePlatformService gamePlatformService, IPlatformService platformService)
        {
            _logger = logger;
            _gamePlatformService = gamePlatformService;
            _platformService = platformService;
        }

        [HttpGet("{id}/games")]
        public async Task<ActionResult<List<GameResponseDto>>> GetGamesByPlatformId(Guid id)
        {
            try
            {
                var game = await _gamePlatformService.GetGamesByPlatformId(id);
                if (game != null)
                {
                    return Ok(game);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving games: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreatePlatform([FromBody] PlatformRequestDto platform)
        {
            try
            {
                await _platformService.CreatePlatform(platform);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting platform: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<PlatformDto>> GetPlatformById(Guid id)
        {
            try
            {
                var genre = await _platformService.GetPlatformById(id);
                if (genre != null)
                {
                    return Ok(genre);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving platform: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("")]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<List<PlatformDto>>> GetAllPlatforms()
        {
            try
            {
                var genres = await _platformService.GetAllPlatforms();
                return Ok(genres);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving all platforms: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePlatform(PlatformDto platformDto)
        {
            try
            {
                PlatformDto platform = await _platformService.GetPlatformByIdAsNoTracking(platformDto.Id);

                if (platform == null)
                {
                    return NotFound();
                }

                await _platformService.UpdatePlatform(platformDto);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating platform: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlatform(Guid id)
        {
            try
            {
                PlatformDto platform = await _platformService.GetPlatformByIdAsNoTracking(id);

                if (platform == null)
                {
                    return NotFound();
                }

                _platformService.DeletePlatform(platform);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting platform: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }

    }
}
