namespace NeoVortex.Application.Interfaces.Auth;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
    string GenerateRandomPassword();
}