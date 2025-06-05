using Microsoft.EntityFrameworkCore;
using backend.Data;

namespace backend.Extensions;

public static class MigrationExtensions
{
    public static void EnsureDatabaseCreated(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            
            // Create database if it doesn't exist
            if (context.Database.EnsureCreated())
            {
                var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();
                logger.LogInformation("Database created successfully");
            }
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the database");
        }
    }
}
