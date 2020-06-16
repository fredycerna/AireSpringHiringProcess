using AireSpring.Data.Repositories;

namespace AireSpring.Data.Core
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get; }
        void Commit();
    }
}
