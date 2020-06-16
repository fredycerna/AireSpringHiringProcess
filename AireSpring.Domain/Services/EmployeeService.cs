using AireSpring.Data.Core;
using AireSpring.Data.Models;
using AireSpring.Domain.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AireSpring.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Employee Service constructur
        /// </summary>
        /// <param name="unitOfWork">Injected Unit Of Work</param>
        /// <param name="mapper">Injected automapper</param>
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for get a list of employees
        /// </summary>
        /// <param name="pageSize">Number of records by page</param>
        /// <param name="page">Page number</param>
        /// <returns></returns>
        public async Task<List<EmployeeModel>> GetEmployeeList(int pageSize, int page)
        {
            var result = _mapper.Map<List<EmployeeModel>>(await _unitOfWork.Employees.GetAllAsync(new CollectionParameters
            {
                Limit = pageSize,
                Offset = (page - 1) * pageSize,
                OrderBy = "HireDate",
                Ordering = AscDec.Asc
            }));
            return result;
        }

        /// <summary>
        /// Method to find a employee with a last name or phonenumber that contains the search predicate.
        /// </summary>
        /// <param name="search">predicate</param>
        /// <param name="pageSize">Number of record by page.</param>
        /// <param name="page">Page number</param>
        /// <returns></returns>
        public async Task<List<EmployeeModel>> FindEmployees(string search, int pageSize, int page)
        {

            var result = _mapper.Map<List<EmployeeModel>>(await _unitOfWork.Employees.FindAsync(search, new CollectionParameters
            {
                Limit = pageSize,
                Offset = (page - 1) * pageSize,
                OrderBy = "HireDate",
                Ordering = AscDec.Asc
            }));
            return result;
        }

        /// <summary>
        /// Method to create a new Employee using repository and unit of work
        /// </summary>
        /// <param name="employee">Employee to add</param>
        /// <returns>Employee</returns>
        public async Task<EmployeeModel> CreateEmployee(EmployeeModel employee)
        {
            var result = await _unitOfWork.Employees.AddAsync(_mapper.Map<Employee>(employee));
            _unitOfWork.Commit();
            return _mapper.Map<EmployeeModel>(result);
        }

        /// <summary>
        /// Method to get a employee with that id
        /// </summary>
        /// <param name="id">Id of employee</param>
        /// <returns>Employee</returns>
        public async Task<EmployeeModel> GetEmployee(int id)
        {
            var employee = _mapper.Map<EmployeeModel>(await _unitOfWork.Employees.GetByIdAsync(id));
            return employee;
        }

        /// <summary>
        /// Method to update a employee.
        /// </summary>
        /// <param name="employee">Employee to update.</param>
        /// <returns>Employee updated</returns>
        public async Task<EmployeeModel> UpdateEmployee(EmployeeModel employee)
        {
            var entity = _mapper.Map<Employee>(employee);
            await _unitOfWork.Employees.UpdateAsync(entity);
            _unitOfWork.Commit();
            return employee;
        }

        /// <summary>
        /// Method to remove a employee.
        /// </summary>
        /// <param name="id">Id of employee</param>
        /// <returns>true or false depends of the result of transaction.</returns>
        public async Task<bool> DeleteEmployee(int id)
        {
            await _unitOfWork.Employees.RemoveAsync(id);
            _unitOfWork.Commit();
            return true;
        }

    }
}
