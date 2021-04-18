using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class FavoriteRetailerRepository : BaseRepository<FavoriteRetailer>, IFavoriteRetailerRepository
    {
        public FavoriteRetailerRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<FavoriteRetailer>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            var res = query.Include(e => e.Retailer);
            if (noTracking)
            {
                res.AsNoTracking();
            }

            return await res.ToListAsync();
        }

        public override async Task<FavoriteRetailer> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            var res = query.Include(e => e.Retailer);
            if (noTracking)
            {
                res.AsNoTracking();
            }

            return await res.FirstOrDefaultAsync();
        }
    }
}