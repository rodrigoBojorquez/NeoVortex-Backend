using ErrorOr;
using MediatR;

namespace NeoVortex.Application.Books.Command.Update;

public record UpdateBookCommand(
    Guid Id,
    string Title,
    string Author,
    DateOnly PublishDate,
    Guid CategoryId,
    string? Description = null,
    int? PageCount = null,
    int Quantity = 1) : IRequest<ErrorOr<Updated>>;