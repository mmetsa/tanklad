using Contracts.DAL.App;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ContactTypeRepository : BaseRepository<ContactType>, IContactTypeRepository
    {
        public ContactTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}