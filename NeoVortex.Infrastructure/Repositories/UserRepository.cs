using Microsoft.EntityFrameworkCore;
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
}