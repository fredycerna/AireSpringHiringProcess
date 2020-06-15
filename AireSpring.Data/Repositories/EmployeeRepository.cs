using AireSpring.Data.Core;
using AireSpring.Data.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AireSpring.Data.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IRepository<Employee>
    {
        
        public EmployeeRepository(string tableName):base(tableName, "")
        {
            _tableName = tableName;
        }

        public override async Task<int> AddAsync(Employee entity)
        {
            string sql = @"INSERT INTO [dbo].[Employee]([EmployeeId],[LastName],[FirstName],[Phone],[ZipCode],[HireDate]) 
                           VALUES (@Id, @LastName ,@First,@Phone,@Zip,@HireDate)";
            int result = 0;

            using (var con= GetConnection())
            {
                result = await con.ExecuteAsync(sql, new
                {
                    Id= Guid.NewGuid(),
                    LastName = entity.LastName,
                    FirstName = entity.FirstName,
                    Phone = entity.Phone, 
                    Zip = entity.ZipCode, 
                    HireDate= entity.HireDate
                });
               
            }
            return result;
        }

        public override Task<IEnumerable<Employee>> FindAsync(Employee entity, bool contains=false)
        {
            return null;

        }

        public override async Task<int> UpdateAsync(Employee entity)
        {
            string sql = "UPDATE Employee SET LastName=@LastName, FirstName=@FirstName, Phone=@Phone, ZipCode=@Zip, HireDate=@HireDate WHERE EmployeeId=@Id ";

            int result = 0;

            using (var con = GetConnection())
            {
                result = await con.ExecuteAsync(sql, new { 
                 LastName= entity.LastName,
                 FirstName= entity.FirstName,
                 Phone = entity.Phone, 
                 Zip= entity.ZipCode, 
                 HireDate = entity.HireDate, 
                 Id = entity.Id                
                });
            }
            return result;
        }
    }
}
