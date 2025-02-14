namespace NeoVortex.Application.Interfaces.Auth;

public interface IAuthUtilities
{
    void SetRefreshToken(string token);
    Guid GetUserId();
    bool HasSuperAccess();
}