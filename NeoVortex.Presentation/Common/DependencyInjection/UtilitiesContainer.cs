using NeoVortex.Application.Interfaces.Services;
using NeoVortex.Presentation.Utilities;

namespace NeoVortex.Presentation.Common.DependencyInjection;

public static class UtilitiesContainer
{
    public static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        services.AddScoped<IAuthUtilities, AuthUtilities>();
        
        return services;
    }
}