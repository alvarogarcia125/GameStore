using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(DatabaseContext context) : base(context) { }

        public async Task<Guid> AddNewGame(Game game, List<Guid> genres, List<Guid> platforms)
        {
            using (var dbContextTransaction = await DbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await AddAsync(game);

                    if (genres.Any())
                    {
                        var gameGenres = genres.Select(genreUid => new GameGenre
                        {
                            GameId = game.Id,
                            GenreId = genreUid
                        });

                        await DbContext.AddRangeAsync(gameGenres);
                    }

                    if (platforms.Any())
                    {
                        var gamePlatforms = platforms.Select(platformUid => new GamePlatform
                        {
                            GameId = game.Id,
                            PlatformId = platformUid
                        });

                        await DbContext.AddRangeAsync(gamePlatforms);
                    }

                    await DbContext.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();

                    return game.Id;
                }
                catch (DbUpdateException ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    throw new Exception("Ocurrió un error en la base de datos al crear el juego.", ex);
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    throw new Exception("Ocurrió un error al crear el juego.", ex);
                }
            }
        }

        public async Task<Game?> GetByKeyAsync(string key)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Key == key);
        }

        public Game? GetByKey(string key)
        {
            return DbSet.Where(x => x.Key == key).AsNoTracking().FirstOrDefault();
        }

        public virtual async Task UpdateAsync(Game game, List<Guid> platforms, List<Guid> genres)
        {
            var existingGame = await DbSet.FindAsync(game.Id);

            if (existingGame == null)
            {
                throw new Exception("Game Not found");
            }

            using (var dbContextTransaction = await DbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    DbContext.Entry(existingGame).CurrentValues.SetValues(game);

                    List<GameGenre> currentGenres = DbContext.GameGenre.Where(x => x.GameId == game.Id).ToList();

                    DbContext.GameGenre.RemoveRange(currentGenres);

                    if (genres.Any())
                    {
                        var gameGenres = genres.Select(genreUid => new GameGenre
                        {
                            GameId = game.Id,
                            GenreId = genreUid
                        });

                        await DbContext.AddRangeAsync(gameGenres);
                    }

                    List<GamePlatform> currentPlatforms = DbContext.GamePlatform.Where(x => x.GameId == game.Id).ToList();

                    DbContext.GamePlatform.RemoveRange(currentPlatforms);

                    if (platforms.Any())
                    {
                        var gamePlatforms = platforms.Select(platformUid => new GamePlatform
                        {
                            GameId = game.Id,
                            PlatformId = platformUid
                        });

                        await DbContext.AddRangeAsync(gamePlatforms);
                    }

                    await DbContext.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                }
                catch (DbUpdateException ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    throw new Exception("Ocurrió un error en la base de datos al editar el juego.", ex);
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    throw new Exception("Ocurrió un error al editar el juego.", ex);
                }
            }
        }
    }

}
