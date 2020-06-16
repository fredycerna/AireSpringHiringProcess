using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AireSpring.Data.Core
{
  public interface IRepository<TEntity>
    {        
        Task<TEntity> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync(CollectionParameters parameters);

        Task<IEnumerable<TEntity>> FindAsync(string search, CollectionParameters parameters);

        Task<TEntity> AddAsync(TEntity entity);    

        Task RemoveAsync(int id);

        Task<int> UpdateAsync(TEntity entity);

        Task<bool> Exist();

        Task<int> Count();

    }
}
