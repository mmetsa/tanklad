using Contracts.DAL.App;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }

        public IContactRepository Contacts => GetRepository(() => new ContactRepository(UowDbContext));
        public IContactTypeRepository ContactTypes => GetRepository(() => new ContactTypeRepository(UowDbContext));
        public IGasStationRepository GasStations => GetRepository(() => new GasStationRepository(UowDbContext));
        public IRetailerRepository Retailers => GetRepository(() => new RetailerRepository(UowDbContext));
        public ICustomerCardRepository CustomerCards => GetRepository(() => new CustomerCardRepository(UowDbContext));
        public IFavoriteGasStationRepository FavoriteGasStations => GetRepository(() => new FavoriteGasStationRepository(UowDbContext));
        public IFavoriteRetailerRepository FavoriteRetailers => GetRepository(() => new FavoriteRetailerRepository(UowDbContext));
        public IFuelTypeInGasStationRepository FuelTypesInGasStation => GetRepository(() => new FuelTypeInGasStationRepository(UowDbContext));
        public IFuelTypeRepository FuelTypes => GetRepository(() => new FuelTypeRepository(UowDbContext));
        public IServiceInGasStationRepository ServicesInGasStation => GetRepository(() => new ServiceInGasStationRepository(UowDbContext));
        public IServiceRepository Services => GetRepository(() => new ServiceRepository(UowDbContext));


    }
}