using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class GasStation : DomainEntityId
    {
        
        [MaxLength(256)]
        public string Name { get; set; } = default!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        [MaxLength(512)]
        public string? Address { get; set; }
        
        [MaxLength(512)]
        public string? OpenHours { get; set; }

        public Guid RetailerId { get; set; }
        public Retailer? Retailer { get; set; }
        
        public ICollection<ServiceInGasStation>? ServicesInGasStations { get; set; }
        
        public ICollection<FuelTypeInGasStation>? FuelTypesInGasStation { get; set; }
        
        public ICollection<Contact>? Contacts { get; set; }
        
        public ICollection<FavoriteGasStation>? FavoriteGasStations { get; set; }
        
    }
}