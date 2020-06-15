using System;
using System.Collections.Generic;
using System.Text;
using AireSpring.Data.Core;

namespace AireSpring.Domain.Services
{
 public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }





    }
}
