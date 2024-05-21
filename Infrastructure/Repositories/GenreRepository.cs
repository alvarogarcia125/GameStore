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
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(DatabaseContext context) : base(context) {
            }


        public async Task AddGenre(Genre genre)
        {
            await DbSet.AddAsync(genre);
            await DbContext.SaveChangesAsync();
        }

        public async Task<List<Genre>> GetByParentIdAsync(Guid parentId)
        {
            return await DbSet.Where(x => x.ParentGenreId == parentId).ToListAsync();
        }

        public async Task<Genre?> GetGenreByIdAsNoTracking(Guid id)
        {
            return await DbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateGenre(Genre genre)
        {

            DbSet.Update(genre);

            await DbContext.SaveChangesAsync();

        }

    }

}
