using ErrorOr;
using MediatR;
using NeoVortex.Application.Interfaces.Services;

namespace NeoVortex.Application.Document.Commands.Upload;

public class UploadDocumentCommandHandler : IRequestHandler<UploadDocumentCommand, ErrorOr<string>>
{
    private readonly IFileService _fileService;

    public UploadDocumentCommandHandler(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<ErrorOr<string>> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
        return await _fileService.UploadFileAsync(request.DocumentStream, request.FolderName, request.FileName);
    }
}