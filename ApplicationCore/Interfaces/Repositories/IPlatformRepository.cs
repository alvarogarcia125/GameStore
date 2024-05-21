using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IPlatformRepository : IRepositoryBase<Entities.Platform>
    {
        Task AddPlatform(Platform platform);

        Task<Platform?> GetPlatformByIdAsNoTracking(Guid id);

        Task UpdatePlatform(Platform platform);
    }
}
