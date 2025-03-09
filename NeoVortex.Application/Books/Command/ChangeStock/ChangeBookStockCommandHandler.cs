using ErrorOr;
using MediatR;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Domain.Errors;

namespace NeoVortex.Application.Books.Command.ChangeStock;

public class ChangeBookStockCommandHandler : IRequestHandler<ChangeBookStockCommand, ErrorOr<Updated>>
{
    private readonly IBookRepository _bookRepository;

    public ChangeBookStockCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<ErrorOr<Updated>> Handle(ChangeBookStockCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId);
        
        if (book is null)
            return Errors.Book.NotFound;
        
        book.Quantity += request.Difference;
        await _bookRepository.UpdateAsync(book);

        return Result.Updated;
    }
}