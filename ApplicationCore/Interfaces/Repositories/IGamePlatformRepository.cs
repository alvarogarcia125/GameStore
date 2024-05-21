using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IGamePlatformRepository : IRepositoryBase<Entities.GamePlatform>
    {
        Task<List<Game>> GetGamesByPlatformIdAsync(Guid genreId);

        Task<List<Platform>> GetPlatformsByGameKey(string gameKey);
    }
}
