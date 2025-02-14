using ErrorOr;
using Microsoft.EntityFrameworkCore;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Domain.Entities;
using NeoVortex.Domain.Errors;
using NeoVortex.Infrastructure.Data;

namespace NeoVortex.Infrastructure.Repositories;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(NeoVortexDbContext context) : base(context)
    {
    }

    public async Task<Role> GetByUserIdAsync(Guid userId)
    {
        return (await Context.Users
            .Where(u => u.Id == userId)
            .Select(u => u.Role)
            .FirstOrDefaultAsync()) ?? null!;
    }

    public async Task<ErrorOr<Created>> AssignPermissionsAsync(Guid roleId, List<Guid> permissionIds)
    {
        var role = await Context.Roles
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id == roleId);

        if (role is null)
            return Errors.Role.NotFound;

        var currentPermissions = role.Permissions.Select(p => p.Id).ToList();
        var permissionsToAdd = permissionIds.Except(currentPermissions).ToList();
        var permissionsToRemove = currentPermissions.Except(permissionIds).ToList();

        var permissionsToAddEntities = await Context.Permissions
            .Where(p => permissionsToAdd.Contains(p.Id))
            .ToListAsync();

        var permissionsToRemoveEntities = role.Permissions
            .Where(p => permissionsToRemove.Contains(p.Id))
            .ToList();

        foreach (var permission in permissionsToRemoveEntities)
        {
            role.Permissions.Remove(permission);
        }

        foreach (var permission in permissionsToAddEntities)
        {
            role.Permissions.Add(permission);
        }

        await Context.SaveChangesAsync();

        return Result.Created;
    }
}