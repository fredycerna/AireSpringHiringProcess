using AireSpring.Data.Core;
using AireSpring.Data.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AireSpring.Data.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public const string TableName = "Employee";

        public EmployeeRepository(IDbTransaction transaction) : base(transaction, TableName)
        {             
        }

        public override async Task<Employee> AddAsync(Employee entity)
        {
            string sql = @"INSERT INTO [dbo].[Employee]([LastName],[FirstName],[Phone],[ZipCode],[HireDate]) 
                           VALUES (@LastName ,@FirstName,@Phone,@Zip,@HireDate);
                           SELECT CAST(SCOPE_IDENTITY() as int)";
                       
            var id= (await _connection.QueryAsync<int>(sql, new
            {                    
                    LastName = entity.LastName,
                    FirstName = entity.FirstName,
                    Phone = entity.Phone, 
                    Zip = entity.ZipCode, 
                    HireDate= entity.HireDate
            }, _transaction)).Single();
            entity.Id = id;
            return entity;
        }

        public override async Task<IEnumerable<Employee>> FindAsync(string search, CollectionParameters parameters)
        {
            string orderby = string.Empty;
            string top = string.Empty;
            string offset = string.Empty;
            string where = string.Empty;

            if (parameters.OrderBy != null && parameters.OrderBy.Length > 0 && typeof(Employee).GetProperties().Count(f => f.Name.Equals(parameters.OrderBy)) == 1)
                orderby = "ORDER BY " + parameters.OrderBy + ((parameters.Ordering == AscDec.Asc) ? " ASC" : " DESC");

            if (parameters.Limit != null && parameters.Limit > 0)
                top = $"TOP({parameters.Limit})";

            if (parameters.Offset != null && parameters.Offset > 0)
                offset = $"OFFSET {parameters.Offset} ROWS";

            if (search!= null && search.Length > 0)
                offset = $"WHERE LastName like '%{search}%' OR Phone like '%{search}%' ";

            string sql = $"SELECT {top} * FROM {_tableName} {where} {orderby} {offset} ";

            return await _connection.QueryAsync<Employee>(sql, transaction: _transaction);

        }

        public override async Task<int> UpdateAsync(Employee entity)
        {
            string sql = "UPDATE Employee SET LastName=@LastName, FirstName=@FirstName, Phone=@Phone, ZipCode=@Zip, HireDate=@HireDate WHERE Id=@Id ";

            int result = 0;

           
                result = await _connection.ExecuteAsync(sql, new { 
                 LastName= entity.LastName,
                 FirstName= entity.FirstName,
                 Phone = entity.Phone, 
                 Zip= entity.ZipCode, 
                 HireDate = entity.HireDate, 
                 Id = entity.Id                
                }, _transaction);
           
            return result;
        }
    }
}
