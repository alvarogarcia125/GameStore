using ApplicationCore.DTO;

namespace ApplicationCore.Interfaces.Services
{
    public interface IGenreService
    {
        Task CreateGenre(GenreRequestDto genreRequest);

        Task<GenreDto> GetGenreById(Guid id);

        Task<GenreDto> GetGenreByIdAsNoTracking(Guid id);

        Task<List<GenreListDto>> GetAllGenres();

        Task UpdateGenre(GenreDto genreToUpdate);

        void DeleteGame(GenreDto genre);
        Task<List<GenreListDto>> GetGenresByParentId(Guid parentId);

    }
}
