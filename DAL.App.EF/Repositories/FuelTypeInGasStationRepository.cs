using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class FuelTypeInGasStationRepository : BaseRepository<FuelTypeInGasStation>, IFuelTypeInGasStationRepository
    {
        public FuelTypeInGasStationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<FuelTypeInGasStation>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            var res = query
                .Include(e => e.FuelType)
                .Include(e => e.GasStation);
            if (noTracking)
            {
                res.AsNoTracking();
            }

            return await res.ToListAsync();
        }

        public override async Task<FuelTypeInGasStation> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            var res = query
                .Include(e => e.FuelType)
                .Include(e => e.GasStation);
            if (noTracking)
            {
                res.AsNoTracking();
            }

            return await res.FirstOrDefaultAsync();
        }
    }
}