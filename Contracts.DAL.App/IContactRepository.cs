using System.Threading.Tasks;
using Contracts.Repositories;
using Domain.App;

namespace Contracts.DAL.App
{
    public interface IContactRepository : IBaseRepository<Contact>
    {
        // Custom declarations here
    }
}