using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AireSpring.Data.Core
{
    interface IRepository<T>
    {        
        Task<T> GetByIdAsync(int id);

        Task<T> GetAll();

        Task<IEnumerable<T>> Findsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);



    }
}
