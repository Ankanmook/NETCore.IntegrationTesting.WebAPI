using System.Text.Json;

namespace NETCore.IntegrationTesting.WebAPI.IntegrationTests.TestHelper
{
    public static class JsonSerializerHelper
    {
        public static JsonSerializerOptions DefaultSerialisationOptions => new JsonSerializerOptions
        {
            IgnoreNullValues = true
        };
    }
}