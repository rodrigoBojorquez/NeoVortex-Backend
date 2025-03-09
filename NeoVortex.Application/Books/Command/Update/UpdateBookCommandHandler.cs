using ErrorOr;
using MediatR;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Domain.Errors;

namespace NeoVortex.Application.Books.Command.Update;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, ErrorOr<Updated>>
{
    private readonly IBookRepository _bookRepository;

    public UpdateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id);
        
        if (book is null) return Errors.Book.NotFound;
        
        book.Title = request.Title;
        book.Author = request.Author;
        book.PublishDate = request.PublishDate;
        book.CategoryId = request.CategoryId;
        book.Description = request.Description;
        book.PageCount = request.PageCount;
        book.Quantity = request.Quantity;
        
        await _bookRepository.UpdateAsync(book);
        
        return Result.Updated;
    }
}