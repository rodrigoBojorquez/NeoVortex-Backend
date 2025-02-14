using Microsoft.EntityFrameworkCore;
using NeoVortex.Domain.Entities;
using NeoVortex.Infrastructure.Data.Entities;

namespace NeoVortex.Infrastructure.Data.Seeders;

public static partial class Seeder
{
    public static class Administration
    {
        /**
         * Seeder para la gestion de roles de la aplicación
         */
        public static async Task SeedAsync(DbContext context)
        {
            if (await context.Set<Module>().AnyAsync() ||
                await context.Set<Permission>().AnyAsync() ||
                await context.Set<Role>().AnyAsync() ||
                await context.Set<User>().AnyAsync())
            {
                return;
            }

            // Insertar modulos
            var modules = new[]
            {
                new Module { Name = "Usuarios", Description = "Módulo de gestión de usuarios" },
                new Module { Name = "Roles", Description = "Módulo de gestión de roles" },
                new Module { Name = "Permisos", Description = "Módulo de gestión de permisos" },
                new Module { Name = "Documentos", Description = "Módulo de gestión de documentos" },
                new Module { Name = "AsistenteVirtual", Description = "Módulo de asistente virtual" }
            };

            await context.Set<Module>().AddRangeAsync(modules);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var modulesIds = modules.ToDictionary(m => m.Name, m => m.Id);


            // Insertar permisos
            var permissionNames = new[] { "create", "read", "update", "delete" };
            var permissions = modules
                .Where(m => m.Name != "AsistenteVirtual")
                .SelectMany(m => permissionNames.Select(p => new Permission { Name = p, ModuleId = m.Id }))
                .ToList();

            permissions.AddRange(new[]
            {
                new Permission { Name = "chat", ModuleId = modulesIds["AsistenteVirtual"] },
                new Permission { Name = "completions", ModuleId = modulesIds["AsistenteVirtual"] },
                new Permission { Name = "superAdmin" }
            });

            await context.Set<Permission>().AddRangeAsync(permissions);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
            

            var permissionsIds = permissions
                .Where(p => p.ModuleId.HasValue)
                .ToDictionary(p => $"{p.Name}:{modulesIds.FirstOrDefault(m => m.Value == p.ModuleId).Key}", p => p.Id);


            // Insertar roles
            var roles = new[]
            {
                new Role { Name = "SuperAdmin", Description = "Acceso total al sistema" },
                new Role { Name = "Usuario", Description = "Acceso a las funcionalidades no administrativas" }
            };

            await context.Set<Role>().AddRangeAsync(roles);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var rolesIds = roles.ToDictionary(r => r.Name, r => r.Id);


            // Insertar usuarios
            var users = new[]
            {
                new User
                {
                    Name = "Rodrigo", Email = "rbojorquez1620@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("password"), RoleId = rolesIds["SuperAdmin"]
                },
                new User
                {
                    Name = "Fernando", Email = "ffernando.villafanaalfonseca@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("password"), RoleId = rolesIds["Usuario"]
                },
                new User
                {
                    Name = "Alexis", Email = "alexisdanieldr@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("password"), RoleId = rolesIds["Usuario"]
                }
            };

            await context.Set<User>().AddRangeAsync(users);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();


            // Insertar roles-permisos
            var rolesPermissions = roles
                .SelectMany(r => permissions.Select(p => new PermissionRole { RoleId = r.Id, PermissionId = p.Id }))
                .ToList();

            foreach (var rp in rolesPermissions)
            {
                Console.WriteLine($"Role: {rp.RoleId} - Permission: {rp.PermissionId}");
            }

            await context.Set<PermissionRole>().AddRangeAsync(rolesPermissions);
            await context.SaveChangesAsync();
        }
    }
}