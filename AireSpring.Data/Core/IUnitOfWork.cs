using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AireSpring.Data.Core
{
    interface IUnitOfWork
    {

        Task Commit();
    }
}
