using System;

namespace NETCore.IntegrationTesting.WebAPI.Models.DomainModels
{
    public class Product
    {
        public Product(Guid id)
        {
            Id = id;
            CreatedUtc = DateTime.UtcNow; // use to demo IDateTime
        }

        public Guid Id { get; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Category { get; set; }

        public decimal Price { get; set; }

        public int StockCount { get; set; }

        public DateTime CreatedUtc { get; set; }
    }
}