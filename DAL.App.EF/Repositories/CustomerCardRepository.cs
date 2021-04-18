using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CustomerCardRepository : BaseRepository<CustomerCard>, ICustomerCardRepository
    {
        public CustomerCardRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<CustomerCard>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            return await RepoDbSet
                .Include(c => c.Retailer)
                .ToListAsync();
        }

        public override async Task<CustomerCard> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            return await RepoDbSet
                .Include(c => c.Retailer)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}