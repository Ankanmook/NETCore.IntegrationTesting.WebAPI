using Microsoft.Extensions.DependencyInjection;
using NETCore.IntegrationTesting.MockCloudSdk.Database;

namespace NETCore.IntegrationTesting.MockCloudSdk
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddDatabaseClient(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IDatabaseClient<>), typeof(DatabaseClient<>));
            return services;
        }
    }
}