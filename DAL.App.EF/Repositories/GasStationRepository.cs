using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class GasStationRepository : BaseRepository<GasStation>, IGasStationRepository
    {
        public GasStationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<GasStation>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            var res = query.Include(g => g.Retailer);
            if (noTracking)
            {
                res.AsNoTracking();
            }

            return await res.ToListAsync();
        }
    }
}