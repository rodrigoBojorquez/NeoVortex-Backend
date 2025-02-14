using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeoVortex.Application.Common.Auth;
using NeoVortex.Application.Interfaces.Auth;
using NeoVortex.Application.User.Queries.Login;
using NeoVortex.Domain.Errors;
using NeoVortex.Presentation.Common.Controllers;
using NeoVortex.Presentation.Utilities;

namespace NeoVortex.Presentation.Controllers;

[AllowAnonymous]
public class AuthController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;
    private readonly IAuthUtilities _authUtilities;

    public AuthController(IMediator mediator, ITokenService tokenService, IAuthUtilities authUtilities)
    {
        _mediator = mediator;
        _tokenService = tokenService;
        _authUtilities = authUtilities;
    }

    public record LoginRequest(string Email, string Password);

    public record RegisterRequest(string Email, string Password, string Name);
    
    public record LogoutRequest(string RefreshToken);


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _mediator.Send(new LoginQuery(request.Email, request.Password));

        return result.Match(authResult =>
            {
                _authUtilities.SetRefreshToken(authResult.RefreshToken);
                return Ok(new AccessToken(authResult.AccessToken));
            },
            Problem);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        return Ok();
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> Token()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken) || string.IsNullOrEmpty(refreshToken))
            return Problem(Errors.User.MissingRefreshToken);

        if (!await _tokenService.ValidateRefreshTokenAsync(refreshToken))
            return Problem(Errors.User.InvalidRefreshToken);

        var result = await _tokenService.RefreshToken(refreshToken);

        return result.Match(authResult =>
        {
            _authUtilities.SetRefreshToken(authResult.RefreshToken);
            return Ok(new AccessToken(authResult.AccessToken));
        }, Problem);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutRequest request)
    {
        await _tokenService.DeleteRefreshTokenAsync(request.RefreshToken);
        return Ok();
    }
    
    [HttpPost("show-access")]
    public async Task<IActionResult> ShowAccess()
    {
        return Ok();
    }
}