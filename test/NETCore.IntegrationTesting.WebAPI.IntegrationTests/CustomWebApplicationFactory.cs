using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NETCore.IntegrationTesting.WebAPI.External.Database;
using NETCore.IntegrationTesting.WebAPI.IntegrationTests.Fakes;

namespace NETCore.IntegrationTesting.WebAPI.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where
        TStartup : class
    {
        public FakeCloudDatabase FakeCloudDatabase { get; }

        public CustomWebApplicationFactory()
        {
            FakeCloudDatabase = FakeCloudDatabase.WithDefaultProducts();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<ICloudDatabase>(FakeCloudDatabase);
            });
        }
    }
}
