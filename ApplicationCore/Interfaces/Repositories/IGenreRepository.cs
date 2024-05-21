using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IGenreRepository : IRepositoryBase<Entities.Genre>
    {
        Task AddGenre(Genre genre);

        Task UpdateGenre(Genre genre);
        Task<List<Genre>> GetByParentIdAsync(Guid parentId);

        Task<Genre?> GetGenreByIdAsNoTracking(Guid id);
    }
}
