using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NeoVortex.Application.Common.Results;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Domain.Entities;
using NeoVortex.Infrastructure.Data;

namespace NeoVortex.Infrastructure.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    public BookRepository(NeoVortexDbContext context) : base(context)
    {
    }

    public async Task<Book?> IncludeCategoryAsync(Guid id)
    {
        return await Context.Books.Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);
    }

    public new async Task<ListResult<Book>> ListAsync(int page = 1, int pageSize = 10,
        Expression<Func<Book, bool>>? filter = null)
    {
        IQueryable<Book> query = Context.Books.Include(b => b.Category);

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        query = query.OrderBy(b => b.PublishDate);

        var total = await query.CountAsync();
        var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new ListResult<Book>(page, pageSize, total, data);
    }
}