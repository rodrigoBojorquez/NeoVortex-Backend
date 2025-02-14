using NeoVortex.Infrastructure.Data;

namespace NeoVortex.Presentation.Common.HttpConfigurations;

public static class TriggerSeeder
{
    public static async Task<WebApplication> UseTriggerSeeder(this WebApplication app)
    {
        await using (var scope = app.Services.CreateAsyncScope())
        await using (var dbContext = scope.ServiceProvider.GetRequiredService<NeoVortexDbContext>())
        {
            await dbContext.Database.EnsureCreatedAsync();
        }
        
        return app;
    }
}