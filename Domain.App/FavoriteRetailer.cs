using System;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class FavoriteRetailer : DomainEntityId
    {

        public Guid RetailerId { get; set; }
        public Retailer? Retailer { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}