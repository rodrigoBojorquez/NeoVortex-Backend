using ErrorOr;
using NeoVortex.Domain.Entities;

namespace NeoVortex.Application.Interfaces.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role> GetByUserIdAsync(Guid userId);
    Task<ErrorOr<Created>> AssignPermissionsAsync(Guid roleId, List<Guid> permissionIds);
}