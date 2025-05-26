using JsonApiBugReport.Data;
using JsonApiBugReport.Data.DummySeed;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace JsonApiBugReport.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServerDb");
        return services
            .AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);

#if DEBUG
                options.EnableSensitiveDataLogging();
#endif
            });
    }

    public static async Task RunDbMigrations(this WebApplication app)
    {

        using var scope = app.Services.CreateScope();
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                // Apply migrations
                await context.Database.MigrateAsync();

                // Seed only if needed
                SeedData.Initialize(services);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Migration or seeding failed: {ex.Message}");
            }
        }

    }

}
