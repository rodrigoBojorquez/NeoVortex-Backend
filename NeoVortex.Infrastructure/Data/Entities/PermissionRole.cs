using NeoVortex.Domain.Entities;

namespace NeoVortex.Infrastructure.Data.Entities;

public class PermissionRole
{
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; } = null!;
}