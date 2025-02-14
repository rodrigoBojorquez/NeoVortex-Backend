using NeoVortex.Domain.Entities;

namespace NeoVortex.Application.Interfaces.Repositories;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<List<Permission>> GetByRoleAsync(Guid roleId);
}