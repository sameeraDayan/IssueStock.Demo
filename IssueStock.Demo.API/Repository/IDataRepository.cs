using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueStock.Demo.API.Repository
{
    public interface IDataRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity dbEntity, TEntity entity);
    }
}
