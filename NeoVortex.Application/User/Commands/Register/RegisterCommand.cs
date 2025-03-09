using ErrorOr;
using MediatR;
using NeoVortex.Application.User.Common;

namespace NeoVortex.Application.User.Commands.Register;

public record RegisterCommand(string Name, string Email, string Password) : IRequest<ErrorOr<AuthResult>>;