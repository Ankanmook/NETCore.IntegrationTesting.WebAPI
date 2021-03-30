using NETCore.IntegrationTesting.WebAPI.Models.DomainModels;

namespace NETCore.IntegrationTesting.WebAPI.Data
{
    public interface IProductValidator
    {
        ValidationResult ValidateNewProduct(Product product);
    }
}
