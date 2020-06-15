using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AireSpring.Data.Core
{
    public abstract class Repository<T> : IRepository<T> where T: DbEntity
    {
        protected IDbTransaction _transaction;
        protected IDbConnection _connection { get { return _transaction.Connection; } }

        protected string _tableName;

        public Repository(IDbTransaction transaction, string tableName)
        {
            _tableName = tableName;
            _transaction = transaction;
        }
               
     
        /// <summary>
        /// Get all rows in the table
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {           
            return await _connection.QueryAsync<T>($"SELECT * FROM {_tableName}", _transaction);
        }

        /// <summary>
        /// Get an entity by Id
        /// </summary>
        /// <param name="id">Row Id</param>
        /// <returns>Object T</returns>
        public Task<T> GetByIdAsync(int id)
        {
          return _connection.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE Id=@ID", new { ID = id }, _transaction);
            
        }


        /// <summary>
        /// Remove a record by Id
        /// </summary>
        /// <param name="id">Id of the record</param>
        /// <returns>Void</returns>
        public async Task RemoveAsync(int id)
        {
           await _connection.ExecuteAsync($"DELETE FROM {_tableName} WHERE id=@ID  ", new { ID = id }, _transaction);
           
        }


        public abstract Task<int> AddAsync(T entity);

        public abstract Task<int> UpdateAsync(T entity);

        public abstract Task<IEnumerable<T>> FindAsync(T entity, bool contains = false);
       

        
    }
}
