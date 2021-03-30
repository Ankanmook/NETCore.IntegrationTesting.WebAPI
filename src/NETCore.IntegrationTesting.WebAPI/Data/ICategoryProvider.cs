using System.Collections.Generic;

namespace NETCore.IntegrationTesting.WebAPI.Data
{
    public interface ICategoryProvider
    {
        IReadOnlyCollection<string> AllowedCategories();
    }
}