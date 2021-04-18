using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Retailer : DomainEntityId
    {
        
        [MaxLength(64)]
        public string Name { get; set; } = default!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        [MaxLength(512)]
        public string? Address { get; set; }

        public ICollection<CustomerCard>? CustomerCards { get; set; }
        public ICollection<Contact>? Contacts { get; set; }
        public ICollection<FavoriteRetailer>? FavoriteRetailers { get; set; }
        public ICollection<GasStation>? GasStations { get; set; }
    }
}