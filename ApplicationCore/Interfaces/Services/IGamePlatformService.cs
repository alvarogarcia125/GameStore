using ApplicationCore.DTO;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Services
{
    public interface IGamePlatformService
    {
        Task<List<GameResponseDto>> GetGamesByPlatformId(Guid genreId);

        Task<List<PlatformDto>> GetPlatformsByGameKey(string gameKey);
    }
}
