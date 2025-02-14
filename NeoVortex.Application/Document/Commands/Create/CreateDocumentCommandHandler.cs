using ErrorOr;
using MediatR;
using NeoVortex.Application.Document.Common;

namespace NeoVortex.Application.Document.Commands.Create;

public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, ErrorOr<DocumentResult>>
{
    public async Task<ErrorOr<DocumentResult>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}