using ApplicationCore.DTO;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Services
{
    public interface IGameService
    {
        Task<Guid> CreateGame(DTO.GameRequestDto game);

        Task<GameResponseDto> GetGameById(Guid id);

        Task<GameResponseDto> GetGameByKeyAsync(string key);

        GameResponseDto GetGameByKey(string key);

        Task UpdateGame(GameUpdateDto gameUpdate);

        void DeleteGame(GameResponseDto game);

        string GeneratePdf(string key);

        Task<List<GameResponseDto>> GetAllGamesAsync();

        Task<List<GenreListDto>> GetGenreByGameKey(string gameKey);
    }
}
