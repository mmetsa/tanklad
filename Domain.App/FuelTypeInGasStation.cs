using System;
using Domain.Base;

namespace Domain.App
{
    public class FuelTypeInGasStation : DomainEntityId
    {
        public double Price { get; set; }
        
        public Guid GasStationId { get; set; }
        public GasStation? GasStation { get; set; }

        public Guid FuelTypeId { get; set; }
        public FuelType? FuelType { get; set; }
        
    }
}