using AireSpring.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AireSpring.Data.Core
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IEmployeeRepository _employees;

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();           
        }


        public IEmployeeRepository Employees { get { return _employees ?? (_employees = new EmployeeRepository(_transaction)); } }        

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

        public void ClearRepositories() {
            _employees = null;
        }

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
