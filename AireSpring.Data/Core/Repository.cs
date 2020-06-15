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
        protected string _tableName;
        protected readonly string _connectionString;

        public Repository(string tableName, string connectionString)
        {
            _tableName = tableName;
            _connectionString = connectionString;
        }
               
        /// <summary>
        /// Open a new connection
        /// </summary>
        /// <returns>IDbConnection</returns>
        protected IDbConnection GetConnection() {
            var con = new SqlConnection(_connectionString);
            con.Open();
            return con;
        }

        /// <summary>
        /// Get all rows in the table
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var con = GetConnection();
            return await con.QueryAsync<T>($"SELECT * FROM {_tableName}");
        }

        /// <summary>
        /// Get an entity by Id
        /// </summary>
        /// <param name="id">Row Id</param>
        /// <returns>Object T</returns>
        public Task<T> GetByIdAsync(int id)
        {
            using (var con = GetConnection())
            {
                return con.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE Id=@ID", new { ID = id });
            }
        }


        /// <summary>
        /// Remove a record by Id
        /// </summary>
        /// <param name="id">Id of the record</param>
        /// <returns>Void</returns>
        public async Task RemoveAsync(int id)
        {
            using (var con = GetConnection())
            {
                await con.ExecuteAsync($"DELETE FROM {_tableName} WHERE id=@ID  ", new { ID = id });
            }
        }


        public abstract Task<int> AddAsync(T entity);

        public abstract Task<int> UpdateAsync(T entity);

        public abstract Task<IEnumerable<T>> FindAsync(T entity, bool contains = false);
       

        
    }
}
