using System;
using Domain.Base;

namespace Domain.App
{
    public class ServiceInGasStation : DomainEntityId
    {
        public Guid GasStationId { get; set; }
        public GasStation? GasStation { get; set; }

        public Guid ServiceId { get; set; }
        public Service? Service { get; set; }
    }
}