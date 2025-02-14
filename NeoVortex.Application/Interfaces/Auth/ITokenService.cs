using System.Security.Claims;
using ErrorOr;
using NeoVortex.Application.User.Common;

namespace NeoVortex.Application.Interfaces.Auth;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(Domain.Entities.User user);
    string GenerateRefreshToken();
    Task<bool> ValidateRefreshTokenAsync(string refreshToken);
    Task StoreRefreshTokenAsync(string refreshToken, Guid userId);
    Task<ErrorOr<AuthResult>> RefreshToken(string refreshToken);
    Task DeleteRefreshTokenAsync(string refreshToken);
}