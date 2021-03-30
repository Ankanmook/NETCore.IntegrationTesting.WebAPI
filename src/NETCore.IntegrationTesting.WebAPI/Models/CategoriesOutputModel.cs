using System.Collections.Generic;

namespace NETCore.IntegrationTesting.WebAPI.Models
{
    public class CategoriesOutputModel
    {
        public CategoriesOutputModel(IReadOnlyCollection<string> categories)
        {
            AllowedCategories = categories;
        }

        public IReadOnlyCollection<string> AllowedCategories { get; }
    }
}
