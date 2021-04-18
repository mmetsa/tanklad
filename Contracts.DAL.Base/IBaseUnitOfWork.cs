using System;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBaseUnitOfWork
    {
        Task<int> SaveChangesAsync();

        public TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod)
            where TRepository : class;
    }
}