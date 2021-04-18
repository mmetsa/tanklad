using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Service : DomainEntityId
    {
        
        [MaxLength(128)]
        public string Name { get; set; } = default!;

        public ICollection<ServiceInGasStation>? ServicesInGasStation { get; set; }
    }
}