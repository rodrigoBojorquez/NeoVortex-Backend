using ErrorOr;
using MediatR;

namespace NeoVortex.Application.Categories.Commands.Add;

public record AddCategoryCommand(string Name, string? Description = null, string? Color = null) : IRequest<ErrorOr<Created>>;