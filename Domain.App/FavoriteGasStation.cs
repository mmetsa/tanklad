using System;
using Domain.Base;

namespace Domain.App
{
    public class FavoriteGasStation : DomainEntityId
    {

        public Guid GasStationId { get; set; }
        public GasStation? GasStation { get; set; }
        
        //Todo - add user connection
    }
}