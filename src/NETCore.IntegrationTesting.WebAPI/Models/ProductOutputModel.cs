using System;
using NETCore.IntegrationTesting.WebAPI.Models.DomainModels;

namespace NETCore.IntegrationTesting.WebAPI.Models
{
    public class ProductOutputModel
    {
        private ProductOutputModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.ShortDescription;
            Category = product.Category;
            Price = product.Price;
            StockCount = product.StockCount;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Category { get; }
        public decimal Price { get; }
        public int StockCount { get; }

        public static ProductOutputModel FromProduct(Product product)
        {
            _ = product ?? throw new ArgumentNullException(nameof(product));

            return new ProductOutputModel(product);
        }
    }
}