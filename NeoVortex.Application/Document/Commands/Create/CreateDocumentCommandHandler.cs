using ErrorOr;
using MediatR;
using NeoVortex.Application.Document.Common;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Application.Interfaces.Services;
using NeoVortex.Domain.Errors;

namespace NeoVortex.Application.Document.Commands.Create;

public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, ErrorOr<DocumentResult>>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IAuthUtilities _authUtilities;
    

    public CreateDocumentCommandHandler(IDocumentRepository documentRepository, IAuthUtilities authUtilities)
    {
        _documentRepository = documentRepository;
        _authUtilities = authUtilities;
    }

    public async Task<ErrorOr<DocumentResult>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        if (!CanCreateDocument(request.UserId))
            return Errors.User.Unauthorized;
        
        var (userId, title) = request;

        var document = new Domain.Entities.Document
        {
            FileName = title,
            Path = "",
            UserId = userId
        };

        await _documentRepository.InsertAsync(document);
        
        return new DocumentResult(document.Id, document.FileName, document.Path, document.CreatedAt, document.UpdatedAt);
    }

    private bool CanCreateDocument(Guid requestedId) => 
        _authUtilities.HasSuperAccess() || _authUtilities.GetUserId() == requestedId;
}