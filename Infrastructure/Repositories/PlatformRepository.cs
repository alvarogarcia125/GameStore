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
    public class PlatformRepository : RepositoryBase<Platform>, IPlatformRepository
    {
        public PlatformRepository(DatabaseContext context) : base(context) {
            }


        public async Task AddPlatform(Platform platform)
        {
            await DbSet.AddAsync(platform);
            await DbContext.SaveChangesAsync();
        }

        public async Task<Platform?> GetPlatformByIdAsNoTracking(Guid id)
        {
            return await DbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdatePlatform(Platform platform)
        {

            DbSet.Update(platform);

            await DbContext.SaveChangesAsync();

        }

    }

}
