using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeoVortex.Application.Interfaces.Auth;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Infrastructure.Common.Auth;
using NeoVortex.Infrastructure.Common.Logging;
using NeoVortex.Infrastructure.Data;
using NeoVortex.Infrastructure.Data.Seeders;
using NeoVortex.Infrastructure.Repositories;
using NeoVortex.Infrastructure.Services.Auth;

namespace NeoVortex.Infrastructure.Common.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        LoggingProfile.Configure(config);
        services.AddJwtScheme(config);

        services.AddDbContext<NeoVortexDbContext>(opt =>
        {
            opt.UseNpgsql(config.GetConnectionString("DefaultConnection"))
                .UseSeeding((context, _) => { Seeder.Administration.SeedAsync(context).Wait(); })
                .UseAsyncSeeding(async (context, _, ct) => { await Seeder.Administration.SeedAsync(context); });
        });

        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();

        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<ITokenService, JwtService>();

        return services;
    }
}