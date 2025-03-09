using ErrorOr;
using MediatR;
using NeoVortex.Application.Books.Common;
using NeoVortex.Application.Common.Results;

namespace NeoVortex.Application.Books.Queries.List;

public record ListBooksQuery(int Page, int PageSize, string? Title, Guid? CategoryId)
    : IRequest<ErrorOr<ListResult<BookResult>>>;