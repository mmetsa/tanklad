namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork: IBaseUnitOfWork
    {
        IContactRepository Contacts { get; }
        IContactTypeRepository ContactTypes { get; }
        IGasStationRepository GasStations { get; }
        IRetailerRepository Retailers { get; }
        ICustomerCardRepository CustomerCards { get; }
        IFavoriteGasStationRepository FavoriteGasStations { get; }
        IFavoriteRetailerRepository FavoriteRetailers { get; }
        IFuelTypeInGasStationRepository FuelTypesInGasStation { get; }
        IFuelTypeRepository FuelTypes { get; }
        IServiceInGasStationRepository ServicesInGasStation { get; }
        IServiceRepository Services { get; }
    }
}