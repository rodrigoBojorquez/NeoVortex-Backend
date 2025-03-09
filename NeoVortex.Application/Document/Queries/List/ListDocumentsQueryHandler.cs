using ErrorOr;
using MediatR;
using NeoVortex.Application.Common.Results;
using NeoVortex.Application.Document.Common;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Application.Interfaces.Services;
using NeoVortex.Domain.Errors;

namespace NeoVortex.Application.Document.Queries.List;

public class ListDocumentsQueryHandler : IRequestHandler<ListDocumentsQuery, ErrorOr<ListResult<DocumentResult>>>
{
    private readonly IAuthUtilities _authUtilities;
    private readonly IDocumentRepository _documentRepository;

    public ListDocumentsQueryHandler(IAuthUtilities authUtilities, IDocumentRepository documentRepository)
    {
        _authUtilities = authUtilities;
        _documentRepository = documentRepository;
    }

    public async Task<ErrorOr<ListResult<DocumentResult>>> Handle(ListDocumentsQuery request,
        CancellationToken cancellationToken)
    {
        if (!_authUtilities.HasSuperAccess() && request.UserId != _authUtilities.GetUserId())
            return Errors.User.Unauthorized;

        var (page, pageSize, userId, fileName) = request;

        var data = userId.HasValue
            ? await _documentRepository.ListAsync(page, pageSize,
                d => d.UserId == userId && (fileName == null || d.FileName.Contains(fileName)))
            : await _documentRepository.ListAsync(page, pageSize,
                d => fileName == null || d.FileName.Contains(fileName));

        return new ListResult<DocumentResult>(data.Page, data.PageSize, data.TotalItems,
            data.Items.Select(d => new DocumentResult(d.Id, d.FileName, d.Path, d.CreatedAt, d.UpdatedAt)).ToList());
    }
}