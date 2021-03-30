using System;
using NETCore.IntegrationTesting.WebAPI.Models.DomainModels;

namespace NETCore.IntegrationTesting.WebAPI.Data.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }

        public int StockCount { get; set; }
        public DateTime CreatedUtc { get; set; }

        public static ProductDto FromProduct(Product product) => new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            ShortDescription = product.ShortDescription,
            Category = product.Category,
            Price = product.Price,
            StockCount = product.StockCount,
            CreatedUtc = product.CreatedUtc,
        };

        public Product ToProduct()
        {
            var product = new Product(Id)
            {
                Name = Name,
                ShortDescription = ShortDescription,
                Category = Category,
                Price = Price,
                StockCount = StockCount,
                CreatedUtc = CreatedUtc,
            };
     
            return product;
        }
    }
}