using ApplicationCore.DTO;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Services
{
    public interface IGameGenreService
    {
        Task<List<GameResponseDto>> GetGamesByGenreId(Guid genreId);

        Task<List<GenreListDto>> GetGenresByGameKey(string gameKey);
    }
}
