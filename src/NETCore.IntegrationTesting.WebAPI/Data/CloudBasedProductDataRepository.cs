using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETCore.IntegrationTesting.WebAPI.External.Database;
using NETCore.IntegrationTesting.WebAPI.Models.DomainModels;

namespace NETCore.IntegrationTesting.WebAPI.Data
{
    public class CloudBasedProductDataRepository : IProductDataRepository
    {
        private readonly ICloudDatabase _cloudDatabase;

        public CloudBasedProductDataRepository(ICloudDatabase cloudDatabase)
        {
            _cloudDatabase = cloudDatabase;
        }

        public Task<AddProductResult> AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Product>> GetProductsAsync()
        {
            var allProducts = await _cloudDatabase.GetAllAsync();
            return allProducts.Select(p => p.ToProduct()).ToArray();
        }

        public Task<IReadOnlyCollection<Product>> GetProductsForCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }
    }
}