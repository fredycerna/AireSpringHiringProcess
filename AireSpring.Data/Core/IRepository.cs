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

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> FindAsync(T entity, bool contains=false);

        Task<int> AddAsync(T entity);    

        Task RemoveAsync(int id);

        Task<int> UpdateAsync(T entity);

    }
}
