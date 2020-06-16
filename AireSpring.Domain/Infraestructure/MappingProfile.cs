using AireSpring.Data.Models;
using AireSpring.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AireSpring.Domain.Infraestructure
{
  public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeModel>();
            CreateMap<EmployeeModel, Employee>();
                
        }

    }
}
