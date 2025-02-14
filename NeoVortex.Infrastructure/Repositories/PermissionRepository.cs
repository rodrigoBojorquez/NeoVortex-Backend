using Microsoft.EntityFrameworkCore;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Domain.Entities;
using NeoVortex.Infrastructure.Data;

namespace NeoVortex.Infrastructure.Repositories;

public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
{
    public PermissionRepository(NeoVortexDbContext context) : base(context)
    {
    }

    public async Task<List<Permission>> GetByRoleAsync(Guid roleId)
    {
        return await Context.Roles
            .Where(r => r.Id == roleId)
            .SelectMany(r => r.Permissions)
            .Include(p => p.Module)
            .ToListAsync();
    }
}