using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeoVortex.Domain.Entities;
using NeoVortex.Infrastructure.Data.Entities;

namespace NeoVortex.Infrastructure.Data.Configurations;

public class PermissionConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasMany<Role>()
            .WithMany(r => r.Permissions)
            .UsingEntity<PermissionRole>();
    }
}