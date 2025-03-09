using ErrorOr;
using MediatR;
using NeoVortex.Application.Common.Results;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Application.Interfaces.Services;
using NeoVortex.Domain.Errors;


namespace NeoVortex.Application.Books.Command.UploadImage;

public class UploadBookImageCommandHandler : IRequestHandler<UploadBookImageCommand, ErrorOr<AssetResult>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IImageService _imageService;

    public UploadBookImageCommandHandler(IBookRepository bookRepository, IImageService imageService)
    {
        _bookRepository = bookRepository;
        _imageService = imageService;
    }

    public async Task<ErrorOr<AssetResult>> Handle(UploadBookImageCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id);

        if (book is null)
            return Errors.Book.NotFound;

        if (!string.IsNullOrEmpty(book.ImageUrl))
            _imageService.Delete(book.ImageUrl);

        var result = await _imageService.UploadAsync(
            request.Image.FileName,
            request.Image.OpenReadStream(),
            request.Id.ToString(), cancellationToken);

        if (result.IsError)
            return result.Errors;

        var imageUrl = result.Value;
        book.ImageUrl = imageUrl;
        await _bookRepository.UpdateAsync(book);
        
        return new AssetResult(request.Image.FileName, imageUrl);
    }
}