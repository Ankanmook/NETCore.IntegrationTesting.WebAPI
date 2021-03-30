using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETCore.IntegrationTesting.WebAPI.Data.Dto;
using NETCore.IntegrationTesting.WebAPI.External.Database;
using NETCore.IntegrationTesting.WebAPI.Models.DomainModels;

namespace NETCore.IntegrationTesting.WebAPI.Data
{
    public class CloudBasedProductDataRepository : IProductDataRepository
    {
        private readonly ICloudDatabase _cloudDatabase;
        private readonly IProductValidator _productValidator;

        public CloudBasedProductDataRepository(ICloudDatabase cloudDatabase,IProductValidator productValidator)
        {
            _cloudDatabase = cloudDatabase;
            _productValidator = productValidator;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var product = await _cloudDatabase.GetAsync(id.ToString());
            return product is object ? product.ToProduct() : null;
        }

        public async Task<IReadOnlyCollection<Product>> GetProductsAsync()
        {
            var allProducts = await _cloudDatabase.GetAllAsync();
            return allProducts.Select(p => p.ToProduct()).ToArray();
        }

        public async Task<IReadOnlyCollection<Product>> GetProductsForCategoryAsync(string category)
        {
            var allProducts = await _cloudDatabase.GetAllAsync();
            return allProducts.Select(p => p.ToProduct()).ToArray();
        }

        public async Task<AddProductResult> AddProductAsync(Product product)
        {
            _ = product ?? throw new ArgumentNullException(nameof(product), "A product is required");

            var validationResult = _productValidator.ValidateNewProduct(product);

            var exsiting = await _cloudDatabase.GetAsync(product.Id.ToString());

            if (validationResult.IsValid && exsiting is null)
            {
                await _cloudDatabase.InsertAsync(product.Id.ToString(), ProductDto.FromProduct(product));
               
                return new AddProductResult(validationResult, false);
            }

            return new AddProductResult(validationResult, validationResult.IsValid); // if it's valid here, then it's a duplicate
        }
    }
}