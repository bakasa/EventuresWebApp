using Microsoft.AspNetCore.Builder;

namespace Eventures.App.Middlewares
{
    public static class SeedDbMidlewareExtensions
    {
        public static IApplicationBuilder UseSeeder(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedDbMiddleware>();
        }
    }
}