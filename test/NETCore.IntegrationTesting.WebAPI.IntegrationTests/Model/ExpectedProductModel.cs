using System;

namespace NETCore.IntegrationTesting.WebAPI.IntegrationTests.Model
{
    public class ExpectedProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}