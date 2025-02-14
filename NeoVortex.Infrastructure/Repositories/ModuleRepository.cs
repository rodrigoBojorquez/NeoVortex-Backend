using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Domain.Entities;
using NeoVortex.Infrastructure.Data;

namespace NeoVortex.Infrastructure.Repositories;

public class ModuleRepository : GenericRepository<Module>, IModuleRepository
{
    public ModuleRepository(NeoVortexDbContext context) : base(context)
    {
    }
}