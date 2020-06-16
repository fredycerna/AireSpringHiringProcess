using AireSpring.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AireSpring.Domain.Services
{
   public interface IEmployeeService
    {

        Task<List<EmployeeModel>> GetEmployeeList(int pageSize=0, int page=0);

        Task<EmployeeModel> CreateEmployee(EmployeeModel employee);

        Task<EmployeeModel> GetEmployee(int id);

        Task<EmployeeModel> UpdateEmployee(EmployeeModel employee);

        Task<bool> DeleteEmployee(int id);

        Task<List<EmployeeModel>> FindEmployees(String search, int pageSize=0, int page=0);

    }
}
