using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NeoVortex.Domain.Entities;
using NeoVortex.Infrastructure.Data.Entities;
using Module = NeoVortex.Domain.Entities.Module;

namespace NeoVortex.Infrastructure.Data;

public class NeoVortexDbContext : DbContext
{
    public NeoVortexDbContext(DbContextOptions<NeoVortexDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<PermissionRole> PermissionRoles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}