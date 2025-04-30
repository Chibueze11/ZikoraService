using Microsoft.EntityFrameworkCore;
using ZikoraService.Api.Middlewares;
using ZikoraService.Infrastructure.Persistence.DbContext;

namespace ZikoraService.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            return app;
        }

        public  static async Task ApplyMigrationsAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            try
            {
                logger.LogInformation("Applying database migrations...");
                await dbContext.Database.MigrateAsync(); 
                logger.LogInformation("Migrations applied successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to apply database migrations");
                throw;
            }
        }
    }

}
