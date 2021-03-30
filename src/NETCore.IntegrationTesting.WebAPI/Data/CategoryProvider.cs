using System.Collections.Generic;

namespace NETCore.IntegrationTesting.WebAPI.Data
{
    public class CategoryProvider : ICategoryProvider
    {
        public IReadOnlyCollection<string> AllowedCategories()
        {
            var allowedCategories = new string[]
            {
                "Alpha", "Beta", "Gamma", "Sigma"
            };

            return allowedCategories;
        }
    }
}