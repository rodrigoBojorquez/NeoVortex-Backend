using ErrorOr;
using MediatR;

namespace NeoVortex.Application.User.Commands.Add;

public record AddUserCommand(string Name, string Email, string Password, Guid RoleId) : IRequest<ErrorOr<Created>>;