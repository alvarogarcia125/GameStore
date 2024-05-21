using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IGameGenreRepository : IRepositoryBase<Entities.GameGenre>
    {
        Task<List<Game>> GetGamesByGenreId(Guid genreId);

        Task<List<Genre>> GetGenreByGameKey(string gameKey);
    }
}
