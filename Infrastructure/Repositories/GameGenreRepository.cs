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
    public class GameGenreRepository(DatabaseContext context) : RepositoryBase<GameGenre>(context), IGameGenreRepository
    {
        public async Task<List<Game>> GetGamesByGenreId(Guid genreId)
        {
            return await DbSet.Where(x => x.GenreId == genreId).Select( g => g.Game).ToListAsync();
        }

        public async Task<List<Genre>> GetGenreByGameKey(string gameKey)
        {
            return await DbSet.Where(x => x.Game.Key == gameKey).Select(g => g.Genre).ToListAsync();
        }
    }
}
