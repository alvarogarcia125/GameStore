using ApplicationCore.Interfaces.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IList<TEntity>> GetAllAsync();
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
