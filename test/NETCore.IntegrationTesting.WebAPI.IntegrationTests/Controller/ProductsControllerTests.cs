using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NETCore.IntegrationTesting.WebAPI.Data;
using NETCore.IntegrationTesting.WebAPI.IntegrationTests.Model;
using NETCore.IntegrationTesting.WebAPI.IntegrationTests.TestHelper;
using Xunit;

namespace NETCore.IntegrationTesting.WebAPI.IntegrationTests.Controller
{
    public class ProductsControllerTests: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public ProductsControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/products/");

            _client = factory.CreateClient();
            _factory = factory;
        }

          [Fact]
        public async Task GetAll_ReturnsExpectedArrayOfProducts()
        {
            _factory.FakeCloudDatabase.ResetDefaultProducts(useCustomIfAvailable: false);

            var products = await _client.GetFromJsonAsync<ExpectedProductModel[]>("");

            Assert.NotNull(products);
            Assert.Equal(_factory.FakeCloudDatabase.Products.Count, products.Count());
        }

        [Fact]
        public async Task Get_ReturnsExpectedProduct()
        {
            var firstProduct = _factory.FakeCloudDatabase.Products.First();

            var product = await _client.GetFromJsonAsync<ExpectedProductModel>($"{firstProduct.Id}");

            Assert.NotNull(product);
            Assert.Equal(firstProduct.Name, product.Name);
        }

        [Theory]
        [MemberData(nameof(GetInvalidInputs))]
        public async Task Post_WithoutName_ReturnsBadRequest(TestProductInputModel productInputModel)
        {            
            var response = await _client.PostAsJsonAsync("", productInputModel, JsonSerializerHelper.DefaultSerialisationOptions);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(GetInvalidInputsAndProblemDetailsErrorValidator))]
        public async Task Post_WithInvalidName_ReturnsExpectedProblemDetails(TestProductInputModel productInputModel,
            Action<KeyValuePair<string, string[]>> validator)
        {
            var response = await _client.PostAsJsonAsync("", productInputModel, JsonSerializerHelper.DefaultSerialisationOptions);

            var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            Assert.Collection(problemDetails.Errors, validator);
        }

        [Fact]
        public async Task Post_WithExistingProductId_ReturnsConflict_WithExpectedLocation()
        {
            var id = _factory.FakeCloudDatabase.Products.First().Id;
            var content = GetValidProductJsonContent(id);

            var response = await _client.PostAsync("", content);

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
            Assert.Equal($"http://localhost/api/products/{id}", response.Headers.Location.ToString().ToLower());
        }

        [Fact]
        public async Task Post_WithValidProduct_ReturnsCreatedResult()
        {
            var id = Guid.NewGuid();
            var content = GetValidProductJsonContent(id);

            var response = await _client.PostAsync("", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal($"http://localhost/api/products/{id}", response.Headers.Location.ToString().ToLower());
        }

        [Fact]
        public async Task Post_AfterPostingValidProduct_ItCanBeRetrieved()
        {
            var id = Guid.NewGuid();
            var content = GetValidProductJsonContent(id);

            var response = await _client.PostAsync("", content);

            response.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync(response.Headers.Location.ToString());

            getResponse.EnsureSuccessStatusCode();
        }

          private static JsonContent GetValidProductJsonContent(Guid? id = null)
        {
            return JsonContent.Create(GetValidProductInputModel(id));
        }

        private static TestProductInputModel GetValidProductInputModel(Guid? id = null)
        {
            return new TestProductInputModel
            {
                Id = id is { } ? id.Value.ToString() : Guid.NewGuid().ToString(),
                Name = "Some Product",
                Description = "This is a description",
                Category = new CategoryProvider().AllowedCategories().First(),
                Price = 4.00m
            };
        }

        public static IEnumerable<object[]> GetInvalidInputsAndProblemDetailsErrorValidator()
        {
            var testData = new List<object[]>
            {
                new object[]
                {
                    GetValidProductInputModel().CloneWith(x => x.Id = null),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.Equal("Id", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.Equal("A non-default ID is required.", error);
                    })
                },

                new object[]
                {
                    GetValidProductInputModel().CloneWith(x => x.Id = Guid.Empty.ToString()),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.Equal("Id", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.Equal("A non-default ID is required.", error);
                    })
                },

                new object[]
                {
                    GetValidProductInputModel().CloneWith(x => x.Id = "NOT-A-GUID"),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.Equal("$.Id", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.StartsWith("The JSON value could not be converted to System.Guid.", error);
                    })
                },

                new object[]
                {
                    GetValidProductInputModel().CloneWith(x => x.Name = null),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.Equal("Name", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.Equal("The Name field is required.", error);
                    })
                },

                new object[]
                {
                    GetValidProductInputModel().CloneWith(x => x.Name =  new string('a', 257)),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                         Assert.Equal("Name", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.Equal("The field Name must be a string with a maximum length of 256.", error);
                    })
                },

                new object[]
                {
                    GetValidProductInputModel().CloneWith(x => x.Description = null),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.Equal("Description", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.Equal("The Description field is required.", error);
                    })
                },

                new object[]
                {
                    GetValidProductInputModel().CloneWith(x => x.Category = null),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.Equal("Category", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.Equal("The Category field is required.", error);
                    })
                },

                new object[]
                {
                    GetValidProductInputModel().CloneWith(x => x.Category = "NOT ALLOWED"),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.Equal("Category", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.Equal("The category did not match any of the allowed categories.", error);
                    })
                },

                new object[]
                {
                    GetValidProductInputModel().CloneWith(x => x.Price = null),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.Equal("Price", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.Equal("The field Price must be between 0.01 and 10000.", error);
                    })
                },

                new object[]
                {
                    GetValidProductInputModel().CloneWith(x => x.Price = 0m),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.Equal("Price", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.Equal("The field Price must be between 0.01 and 10000.", error);
                    })
                },

                new object[]
                {
                    GetValidProductInputModel().CloneWith(x => x.Price = 10000.01m),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.Equal("Price", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.Equal("The field Price must be between 0.01 and 10000.", error);
                    })
                },
            };

            return testData;
        }

        public static IEnumerable<object[]> GetInvalidInputs()
        {
            return GetInvalidInputsAndProblemDetailsErrorValidator().Select(x => new[] { x[0] });
        }
    }
}
