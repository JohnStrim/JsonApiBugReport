using JsonApiBugReport.Data;
using JsonApiBugReport.Data.DummySeed;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace JsonApiBugReport.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<ApplicationDbContext>(options =>
            {
#if USE_SQL_SERVER
                var connectionString = configuration.GetConnectionString("SqlServerDb");
                options.UseSqlServer(connectionString);
#else
                var connectionString = configuration.GetConnectionString("PostgresDb");
                options.UseNpgsql(connectionString);
#endif

#if DEBUG
                options.LogTo(Console.WriteLine, [RelationalEventId.CommandExecuting]);
                options.EnableSensitiveDataLogging();
#endif
            });
    }

    public static async Task RunDbMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
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
