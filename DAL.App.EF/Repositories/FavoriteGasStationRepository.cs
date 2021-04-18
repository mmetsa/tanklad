using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.Domain.Base;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class FavoriteGasStationRepository : BaseRepository<FavoriteGasStation>, IFavoriteGasStationRepository
    {
        public FavoriteGasStationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<FavoriteGasStation>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            var res = query.Include(e => e.GasStation);
            if (noTracking)
            {
                res.AsNoTracking();
            }

            return await res.ToListAsync();
        }

        public override async Task<FavoriteGasStation> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            var res = query.Include(e => e.GasStation);
            if (noTracking)
            {
                res.AsNoTracking();
            }

            return await res.FirstOrDefaultAsync();
        }

    }
}