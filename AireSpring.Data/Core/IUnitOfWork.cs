using AireSpring.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AireSpring.Data.Core
{
    interface IUnitOfWork 
    {
        IEmployeeRepository Employees { get; }
        void Commit();
    }
}
