using AireSpring.Data.Models;
using AireSpring.Domain.Models;
using AutoMapper;

namespace AireSpring.Domain.Infraestructure
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Method to map bettew Data Access Layer and Domain Layer
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeModel>();
            CreateMap<EmployeeModel, Employee>();
                
        }

    }
}
