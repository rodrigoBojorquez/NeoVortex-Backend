using ErrorOr;
using MediatR;
using NeoVortex.Application.Categories.Common;
using NeoVortex.Application.Common.Results;

namespace NeoVortex.Application.Categories.Queries.List;

public record ListCategoriesQuery(int Page, int PageSize, string? Name) : IRequest<ErrorOr<ListResult<CategoryResult>>>;