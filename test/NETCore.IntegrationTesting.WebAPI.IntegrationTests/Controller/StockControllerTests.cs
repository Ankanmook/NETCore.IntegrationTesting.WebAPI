using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NETCore.IntegrationTesting.WebAPI.Data.Dto;
using NETCore.IntegrationTesting.WebAPI.External.Database;
using NETCore.IntegrationTesting.WebAPI.IntegrationTests.Fakes;
using NETCore.IntegrationTesting.WebAPI.IntegrationTests.Model;
using Xunit;

namespace NETCore.IntegrationTesting.WebAPI.IntegrationTests.Controller
{
    public class StockControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Startup> _factory;

        public StockControllerTests(WebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/stock/");
            _client = factory.CreateClient();
            _factory = factory;
        }

        
        [Fact]
        public async Task GetStockTotal_ReturnsExpectedStockQuantity()
        {
            var cloudDatabase = new FakeCloudDatabase(new[]
            {
                new ProductDto{ StockCount = 200},
                new ProductDto{ StockCount = 500},
                new ProductDto{ StockCount = 300}
            });

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<ICloudDatabase>(cloudDatabase);
                });
            }).CreateClient();

            var model = await client.GetFromJsonAsync<ExpectedStockTotalOutputModel>("total");

            Assert.Equal(1000, model.StockItemTotal);
        }

    }
}