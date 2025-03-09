using ErrorOr;
using MediatR;

namespace NeoVortex.Application.Document.Commands.Upload;

public record UploadDocumentCommand(Stream DocumentStream, string FolderName, string FileName) : IRequest<ErrorOr<string>>;