using System.Collections.Generic;
using System.Threading.Tasks;
using NETCore.IntegrationTesting.WebAPI.Data.Dto;

namespace NETCore.IntegrationTesting.WebAPI.External.Database
{
    public interface ICloudDatabase
    {
        Task<ProductDto> GetAsync(string id);

        Task InsertAsync(string id, ProductDto product);

        Task<IReadOnlyCollection<ProductDto>> GetAllAsync();
    }
}