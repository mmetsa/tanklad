using System;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class FavoriteGasStation : DomainEntityId
    {

        public Guid GasStationId { get; set; }
        public GasStation? GasStation { get; set; }
        
        public Guid AppUserId { get; set; }
        
        public AppUser? AppUser { get; set; }
    }
}