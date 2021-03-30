using System;

namespace NETCore.IntegrationTesting.WebAPI.IntegrationTests.Model
{
    public class TestProductInputModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal? Price { get; set; }
        public int Stock { get; set; }

        public TestProductInputModel CloneWith(Action<TestProductInputModel> changes)
        {
            var clone = (TestProductInputModel)MemberwiseClone();

            changes(clone);

            return clone;
        }
    }
}