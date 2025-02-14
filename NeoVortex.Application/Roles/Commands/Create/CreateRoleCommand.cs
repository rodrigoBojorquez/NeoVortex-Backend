using ErrorOr;
using MediatR;
using NeoVortex.Application.Roles.Common;

namespace NeoVortex.Application.Roles.Commands.Create;

public record CreateRoleCommand(string Name, List<Guid> Permissions, string? Description = null) : IRequest<ErrorOr<Created>>;