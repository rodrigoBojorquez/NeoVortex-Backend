using System.Reflection;

namespace NeoVortex.Domain.Entities;

public class Permission
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string Name { get; set; } = string.Empty;
    public bool IsPublic { get; set; } = true;
    public Guid? ModuleId { get; set; }
    public Module Module { get; set; } = null!;
    public List<Role> Roles { get; set; } = [];
}