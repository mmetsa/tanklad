using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class CustomerCard : DomainEntityId
    {
        
        [MaxLength(128)]
        public string Name { get; set; } = default!;
        public double Discount { get; set; }
        
        [MaxLength(1000)]
        public string? Description { get; set; }
        public Guid RetailerId { get; set; }
        public Retailer? Retailer { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

    }
}