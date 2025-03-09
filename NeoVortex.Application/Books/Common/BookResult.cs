namespace NeoVortex.Application.Books.Common;

public record BookResult(
    Guid Id,
    string Title,
    string Author,
    string Description,
    string ImageUrl,
    string CategoryName,
    Guid CategoryId,
    DateOnly PublishDate,
    DateTime CreateDate,
    int Quantity,
    int PageCount);