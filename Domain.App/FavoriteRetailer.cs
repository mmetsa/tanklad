using System;
using Domain.Base;

namespace Domain.App
{
    public class FavoriteRetailer : DomainEntityId
    {

        public Guid RetailerId { get; set; }
        public Retailer? Retailer { get; set; }
        
        //TODO - add user connection
    }
}