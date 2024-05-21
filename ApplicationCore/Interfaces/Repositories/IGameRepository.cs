using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IGameRepository : IRepositoryBase<Entities.Game>
    {
        Task<Guid> AddNewGame(Game game, List<Guid> genres, List<Guid> platforms);

        Task<Game?> GetByKeyAsync(string key);

        Game? GetByKey(string key);

        Task UpdateAsync(Game game, List<Guid> platforms, List<Guid> genres);
    }
}
