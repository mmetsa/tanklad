using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Contact>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            return await RepoDbSet
                .Include(c => c.ContactType)
                .Include(c => c.GasStation)
                .Include(c => c.Retailer)
                .ToListAsync();
        }

        public override Task<Contact> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var res = RepoDbSet
                .Include(c => c.ContactType)
                .Include(c => c.GasStation)
                .Include(c => c.Retailer)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            return res;
        }
    }
}