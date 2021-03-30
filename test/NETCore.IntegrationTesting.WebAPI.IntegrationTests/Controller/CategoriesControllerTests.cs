using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NETCore.IntegrationTesting.WebAPI.IntegrationTests.Model;
using Xunit;

namespace NETCore.IntegrationTesting.WebAPI.IntegrationTests.Controller
{
    public class CategoriesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CategoriesControllerTests(WebApplicationFactory<Startup> factory)
        {
            //_client = factory.CreateDefaultClient();
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/categories");
            _client = factory.CreateClient();
        }

        [Fact] // verbose but example
        public async Task GetAll_ReturnsStatusCode()
        {
            var response = await _client.GetAsync(("/api/categories"));
            response.EnsureSuccessStatusCode();
        }

        [Fact] //can check specific media types too
        public async Task GetAll_ReturnsExpectedMediaType()
        {
            var response = await _client.GetAsync(("/api/categories"));

            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }

        [Fact] 
        public async Task GetAll_ReturnsContent()
        {
            var response = await _client.GetAsync(("/api/categories"));

            Assert.NotNull(response.Content);
            Assert.True(response.Content.Headers.ContentLength > 0);
        }

        [Fact] //Testing content for Json -> kind of make all tests above redundant :)
        public async Task GetAll_ReturnsExpectedJson()
        {
            var expected = new List<string> { "Alpha", "Beta", "Gamma", "Sigma" };
            var model = await _client.GetFromJsonAsync<ExpectedCategoriesModel>(string.Empty);

            Assert.NotNull(model?.AllowedCategories);
            Assert.Equal(expected.OrderBy(s => s), model.AllowedCategories.OrderBy(s => s));
        }

        [Fact]
        public async Task GetAll_SetsExpectedCacheControlHeader()
        {
            var response = await _client.GetAsync("");
            var header = response.Headers.CacheControl;

            Assert.True(header.MaxAge.HasValue);
            Assert.Equal(TimeSpan.FromMinutes(5), header.MaxAge);
            Assert.True(header.Public);
        }
    }
}