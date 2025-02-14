using ErrorOr;
using MediatR;
using NeoVortex.Application.User.Common;

namespace NeoVortex.Application.User.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthResult>>;