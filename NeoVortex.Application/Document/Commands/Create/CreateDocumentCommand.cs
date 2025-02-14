using ErrorOr;
using MediatR;
using NeoVortex.Application.Document.Common;

namespace NeoVortex.Application.Document.Commands.Create;

public record CreateDocumentCommand(Guid UserId, string Title) : IRequest<ErrorOr<DocumentResult>>;