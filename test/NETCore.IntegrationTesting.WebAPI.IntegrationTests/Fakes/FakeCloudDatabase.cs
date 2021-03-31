using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETCore.IntegrationTesting.WebAPI.Data.Dto;
using NETCore.IntegrationTesting.WebAPI.External.Database;

namespace NETCore.IntegrationTesting.WebAPI.IntegrationTests.Fakes
{
    public class FakeCloudDatabase : ICloudDatabase
    {
        private IReadOnlyCollection<ProductDto> _customDefaultProducts;

        public List<ProductDto> Products { get; set; }

        public bool ShouldThrow { get; set; }

        public FakeCloudDatabase(IReadOnlyCollection<ProductDto> products = null)
        {
            ReplaceCustomProducts(products);
            ResetDefaultProducts();
        }

        public Task<ProductDto> GetAsync(string id)
        {
            return Task.FromResult(Products.SingleOrDefault(p => p.Id.ToString() == id));
        }

        public Task InsertAsync(string id, ProductDto product)
        {
            Products.Add(product);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<ProductDto>> GetAllAsync()
        {
            if (ShouldThrow)
                throw new Exception("Test exception!");
            return Task.FromResult(Products as IReadOnlyCollection<ProductDto>);
        }

        public void ReplaceCustomProducts(IReadOnlyCollection<ProductDto> products)
        {
            _customDefaultProducts = products;
        }

        public void ResetDefaultProducts(bool useCustomIfAvailable = true)
        {
            Products = _customDefaultProducts is { } && useCustomIfAvailable
                ? _customDefaultProducts.ToList()
                : GetDefaultProducts();
        }

        public static FakeCloudDatabase WithDefaultProducts()
        {
            var database = new FakeCloudDatabase();
            database.ResetDefaultProducts();
            return database;
        }

        private List<ProductDto> GetDefaultProducts() => new List<ProductDto>
            {
                new ProductDto
                {
                    Id = new Guid("6de90054-242a-434d-86cc-0d3cbc3cb01a"),
                    Name = "Test Product 1",
                    ShortDescription = "Test Product 1 Description",
                    Category = "Clothing",
                    Price = 30.00m,
                    StockCount = 1200
                },

                new ProductDto
                {
                    Id=new Guid("9b7a1993-481d-4877-a48e-a1565cd134ab"),
                    Name = "Test Product 2",
                    ShortDescription = "Test Product 2 Description",
                    Category = "Clothing",
                    Price = 40.00m,
                    StockCount = 500
                },

                new ProductDto
                {
                    Id = new Guid("a5cfffa4-47ed-4b6b-8eab-bf89de5b8fa0"),
                    Name = "Test Product 3",
                    ShortDescription = "Test Product 3 Description",
                    Category = "Rackets",
                    Price = 210.00m,
                    StockCount = 40
                },

                new ProductDto
                {
                    Id = new Guid("1ef1ee8a-89f4-460f-8149-657149f07a79"),
                    Name = "Test Product 4 (Disabled)",
                    ShortDescription = "Test Product 4 Description - This product is disabled.",
                    Category = "Rackets",
                    Price = 400.00m,
                    StockCount = 0
                },

                new ProductDto
                {
                    Id =new Guid("0e577d09-3659-4153-b8e3-24a5447a2c0e"),
                    Name = "Test Product 5",
                    ShortDescription = "Test Product 5 Description",
                    Category = "Accessories",
                    Price = 25.00m,
                    StockCount = 60
                }
            };
    }
}