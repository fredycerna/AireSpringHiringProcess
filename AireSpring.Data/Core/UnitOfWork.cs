using AireSpring.Data.Repositories;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AireSpring.Data.Core
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IEmployeeRepository _employees;

        /// <summary>
        /// Contruct of unit of work
        /// </summary>
        /// <param name="connectionString">Injected connection string from IConfiguration</param>
        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();                   
        }

        #region Repositories
           public IEmployeeRepository Employees { get { return _employees ?? (_employees = new EmployeeRepository(_transaction)); } }

        #endregion

        /// <summary>
        /// Completion of the unit of work.
        /// </summary>
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw;
            }
            finally {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ClearRepositories();
            }
        }

        /// <summary>
        /// Method to clear repositories 
        /// </summary>
        public void ClearRepositories() {
            _employees = null;
        }

        /// <summary>
        /// Method to dispose resources.
        /// </summary>
        public void Dispose()
        {
            if (_transaction != null) {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null) {
                _connection.Dispose();
                _connection = null;
            }


        }
    }
}
