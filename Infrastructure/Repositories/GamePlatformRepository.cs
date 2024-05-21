using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GamePlatformRepository(DatabaseContext context) : RepositoryBase<GamePlatform>(context), IGamePlatformRepository
    {
        public async Task<List<Game>> GetGamesByPlatformIdAsync(Guid genreId)
        {
            return await DbSet.Where(x => x.PlatformId == genreId).Select(g => g.Game).ToListAsync();
        }

        public async Task<List<Platform>> GetPlatformsByGameKey(string gameKey)
        {
            return await DbSet.Where(x => x.Game.Key == gameKey).Select(g => g.Platform).ToListAsync();
        }
    }
}
