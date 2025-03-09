using ErrorOr;
using MediatR;

namespace NeoVortex.Application.Categories.Commands.Update;

public record UpdateCategoryCommand(Guid Id, string Name, string? Description = null, string? Color = null) : IRequest<ErrorOr<Updated>>;