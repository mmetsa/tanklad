using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class FuelType : DomainEntityId
    {
        
        [MaxLength(64)]
        public string Name { get; set; } = default!;
        
        [MaxLength(1000)]
        public string? Description { get; set; }

        public ICollection<FuelTypeInGasStation>? FuelTypesInGasStation { get; set; }
    }
}