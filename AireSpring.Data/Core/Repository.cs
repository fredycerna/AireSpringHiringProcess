using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
        public async Task<IEnumerable<T>> GetAllAsync(CollectionParameters parameters)
        {            
            string sql = $"SELECT * FROM {_tableName} ";            
            if (parameters.OrderBy != null && parameters.OrderBy.Length > 0 && typeof(T).GetFields().Count(f => f.Name.Equals(parameters.OrderBy))==1)
                sql += " OrderBy " + parameters.OrderBy + ((parameters.Ordering == AscDec.Asc)? " ASC" : " DESC");            

            if (parameters.Limit != null && parameters.Limit > 0)
                sql += $" LIMIT {parameters.Limit} ";

            if (parameters.Offset != null && parameters.Offset > 0)
                sql += $" OFFSET {parameters.Offset}";

            return await _connection.QueryAsync<T>(sql, _transaction);
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
