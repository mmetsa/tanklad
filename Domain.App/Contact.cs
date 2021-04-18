using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Contact : DomainEntityId
    {
        
        [MaxLength(128)]
        public string Name { get; set; } = default!;

        public Guid? ContactTypeId { get; set; }
        public ContactType? ContactType { get; set; }

        public Guid? RetailerId { get; set; }
        public Retailer? Retailer { get; set; }

        public Guid? GasStationId { get; set; }
        public GasStation? GasStation { get; set; }
    }
}