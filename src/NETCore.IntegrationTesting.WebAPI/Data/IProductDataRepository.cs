using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NETCore.IntegrationTesting.WebAPI.Models.DomainModels;

namespace NETCore.IntegrationTesting.WebAPI.Data
{
    public interface IProductDataRepository
    {
        Task<Product> GetByIdAsync(Guid id);

        Task<IReadOnlyCollection<Product>> GetProductsAsync();

        Task<IReadOnlyCollection<Product>> GetProductsForCategoryAsync(string category);

    }
}