using Microsoft.AspNetCore.Builder;

namespace NETCore.IntegrationTesting.WebAPI.Middleware
{
    public static class MetricsMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestMetrics(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MetricsMiddleware>();
        }
    }
}