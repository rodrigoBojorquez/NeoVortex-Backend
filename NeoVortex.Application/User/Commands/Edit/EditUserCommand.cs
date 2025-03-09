using ErrorOr;
using MediatR;

namespace NeoVortex.Application.User.Commands.Edit;

public record EditUserCommand(Guid Id, string Name, string Email, string Password, Guid RoleId) : IRequest<ErrorOr<Updated>>;