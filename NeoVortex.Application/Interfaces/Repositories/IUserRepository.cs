namespace NeoVortex.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<Domain.Entities.User>
{
    Task<Domain.Entities.User?> GetByEmailAsync(string email);
}