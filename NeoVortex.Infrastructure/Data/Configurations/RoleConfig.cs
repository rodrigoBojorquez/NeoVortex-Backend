using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeoVortex.Domain.Entities;
using NeoVortex.Infrastructure.Data.Entities;

namespace NeoVortex.Infrastructure.Data.Configurations;

public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasMany<Permission>()
            .WithMany(p => p.Roles)
            .UsingEntity<PermissionRole>();
    }
}