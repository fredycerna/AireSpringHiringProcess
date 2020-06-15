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

        Task<IEnumerable<TEntity>> FindAsync(TEntity entity, bool contains=false);

        Task<int> AddAsync(TEntity entity);    

        Task RemoveAsync(int id);

        Task<int> UpdateAsync(TEntity entity);

    }
}
