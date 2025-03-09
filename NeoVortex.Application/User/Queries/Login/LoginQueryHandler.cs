using ErrorOr;
using MediatR;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Application.Interfaces.Services;
using NeoVortex.Application.User.Common;
using NeoVortex.Domain.Errors;

namespace NeoVortex.Application.User.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;

    public LoginQueryHandler(IUserRepository userRepository, IPasswordService passwordService, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }
    
    public async Task<ErrorOr<AuthResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
            return Errors.User.InvalidCredentials;

        // Para las autenticaciones con Google, Facebook, etc.
        if (user.Password is null || user.Email is null)
            return Errors.User.WrongAuthenticationMethod;
        
        bool passwordIsValid = _passwordService.VerifyPassword(request.Password, user.Password);
        
        if (!passwordIsValid)
            return Errors.User.InvalidCredentials;

        var token = await _tokenService.GenerateTokenAsync(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        
        await _tokenService.StoreRefreshTokenAsync(refreshToken, user.Id);
        
        return new AuthResult(token, refreshToken);
    }
}