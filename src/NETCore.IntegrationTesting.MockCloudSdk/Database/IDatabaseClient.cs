using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETCore.IntegrationTesting.MockCloudSdk.Database
{
    public interface IDatabaseClient<T>
    {
        Task<T> GetAsync(string id);

        Task<IReadOnlyCollection<T>> ScanAsync();

        Task InsertAsync(string id, T value);
    }
}