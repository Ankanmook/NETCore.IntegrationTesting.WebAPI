using System.Collections.Generic;
using System.Threading.Tasks;
using NETCore.IntegrationTesting.MockCloudSdk.Database;
using NETCore.IntegrationTesting.WebAPI.Data.Dto;

namespace NETCore.IntegrationTesting.WebAPI.External.Database
{
    public class CloudDatabase : ICloudDatabase
    {
        private readonly IDatabaseClient<ProductDto> _client;

        public CloudDatabase(IDatabaseClient<ProductDto> client)
        {
            _client = client;
        }

        public Task<ProductDto> GetAsync(string id) => _client.GetAsync(id);

        public Task InsertAsync(string id, ProductDto product) => _client.InsertAsync(id, product);

        public Task<IReadOnlyCollection<ProductDto>> GetAllAsync() => _client.ScanAsync();
    }
}