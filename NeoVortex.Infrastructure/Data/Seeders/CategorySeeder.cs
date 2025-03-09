using Microsoft.EntityFrameworkCore;

namespace NeoVortex.Infrastructure.Data.Seeders;

public static partial class Seeder
{
    public static class Category
    {
        public static async Task SeedAsync(DbContext context)
        {
            if (await context.Set<Domain.Entities.Category>().AnyAsync()) return;
            
            var categories = new[]
            {
                new Domain.Entities.Category { Name = "Fantasía" },
                new Domain.Entities.Category { Name = "Ciencia Ficción" },
                new Domain.Entities.Category { Name = "Terror" },
                new Domain.Entities.Category { Name = "Romance" },
                new Domain.Entities.Category { Name = "Aventura" },
                new Domain.Entities.Category { Name = "Misterio" },
                new Domain.Entities.Category { Name = "Biografía" },
                new Domain.Entities.Category { Name = "Historia" },
                new Domain.Entities.Category { Name = "Política" },
                new Domain.Entities.Category { Name = "Economía" },
                new Domain.Entities.Category { Name = "Autoayuda" },
                new Domain.Entities.Category { Name = "Cocina" },
                new Domain.Entities.Category { Name = "Infantil" },
                new Domain.Entities.Category { Name = "Juvenil" },
                new Domain.Entities.Category { Name = "Adulto" }
            };
        }
    }
}