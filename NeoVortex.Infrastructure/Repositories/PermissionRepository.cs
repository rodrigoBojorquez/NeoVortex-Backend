using Microsoft.EntityFrameworkCore;
using NeoVortex.Application.Common.Results;
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
    
    public new async Task<ListResult<Permission>> ListAllAsync()
    {
        var total = await Context.Permissions.CountAsync();
        var data = await Context.Permissions
            .Include(p => p.Module)
            .ToListAsync();
        
        return new ListResult<Permission>(Page: 1, PageSize:total, TotalItems: total, Items: data);
    }
}