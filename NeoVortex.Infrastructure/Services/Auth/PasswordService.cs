using NeoVortex.Application.Interfaces.Auth;

namespace NeoVortex.Infrastructure.Services.Auth;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    public string GenerateRandomPassword()
    {
        return BCrypt.Net.BCrypt.GenerateSalt(12);
    }
}