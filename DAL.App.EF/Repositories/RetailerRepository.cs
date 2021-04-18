using Contracts.DAL.App;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class RetailerRepository : BaseRepository<Retailer>, IRetailerRepository
    {
        public RetailerRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}