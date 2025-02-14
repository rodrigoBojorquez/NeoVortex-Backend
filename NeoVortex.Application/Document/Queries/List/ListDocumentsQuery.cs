using ErrorOr;
using MediatR;
using NeoVortex.Application.Common.Results;
using NeoVortex.Application.Document.Common;

namespace NeoVortex.Application.Document.Queries.List;

public record ListDocumentsQuery(int Page, int PageSize, Guid? UserId = null, string? FileName = null) : IRequest<ErrorOr<ListResult<DocumentResult>>>;