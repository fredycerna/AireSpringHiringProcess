using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AireSpring.Data.Core;
using AireSpring.Data.Models;
using AireSpring.Domain.Models;
using AutoMapper;

namespace AireSpring.Domain.Services
{
 public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<EmployeeModel>> GetEmployeeList(int pageSize, int page) {            
           var result = _mapper.Map<List<EmployeeModel>>(await _unitOfWork.Employees.GetAllAsync(new CollectionParameters { Limit= pageSize, 
                                                                                                                            Offset=(page-1)*pageSize, 
                                                                                                                            OrderBy="HireDate",  
                                                                                                                            Ordering=AscDec.Asc }));           
           return result;
        }

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

        public async Task<EmployeeModel> CreateEmployee(EmployeeModel employee) {
            var result= await _unitOfWork.Employees.AddAsync(_mapper.Map<Employee>(employee));
            _unitOfWork.Commit();
             return _mapper.Map<EmployeeModel>(result);              
        }

        public async Task<EmployeeModel> GetEmployee(int id) {
            var employee = _mapper.Map<EmployeeModel>(await _unitOfWork.Employees.GetByIdAsync(id));
            return employee;
        }

        public async Task<EmployeeModel> UpdateEmployee(EmployeeModel employee) {
            var entity = _mapper.Map<Employee>(employee);
            await _unitOfWork.Employees.UpdateAsync(entity);
            _unitOfWork.Commit();
            return employee;
        }

        public async Task<bool> DeleteEmployee(int id) {
            await _unitOfWork.Employees.RemoveAsync(id);
           _unitOfWork.Commit();
            return true;
        }
               
    }
}
