using NeoVortex.Domain.Entities;

namespace NeoVortex.Application.Interfaces.Repositories;

public interface IBookRepository : IRepository<Book>
{
    Task<Book?> IncludeCategoryAsync(Guid id);
}