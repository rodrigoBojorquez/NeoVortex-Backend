using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NeoVortex.Application.Common.Results;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Domain.Entities;
using NeoVortex.Infrastructure.Data;

namespace NeoVortex.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User> , IUserRepository
{
    public UserRepository(NeoVortexDbContext context) : base(context) { }


    public Task<User?> GetByEmailAsync(string email)
    {
        return Context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<ListResult<User>> ListWithRoleAsync(int page = 1, int pageSize = 10, Expression<Func<User, bool>>? filter = null)
    {
        var query = Context.Users
            .Include(u => u.Role)
            .AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }
        
        var totalItems = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        
        return new ListResult<User>(page, pageSize, totalItems, items);
    }

    public async Task<User?> IncludeRoleAsync(Guid id)
    {
        return await Context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}